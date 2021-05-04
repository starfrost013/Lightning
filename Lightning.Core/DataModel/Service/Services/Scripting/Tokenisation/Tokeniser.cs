using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq; 
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Tokeniser
    /// 
    /// April 16, 2021 (modified May 2, 2021) 
    /// 
    /// Tokenises a LightningScript file - converts it to a sequence of Tokens that can be easily parsed.
    /// </summary>
    public class ScriptTokeniser
    {
        public ScriptScope CurScope { get; set; }

        public ScriptTokeniser()
        {
            CurScope = new ScriptScope(); 
        }

        public TokenListResult Tokenise(Script Sc)
        {
            TokenListResult TLR = new TokenListResult();

            try
            {
                List<Token> Tokens = new List<Token>();

                if (Sc.Name == null
                    || Sc.Name.Length == 0)
                {
                    ErrorManager.ThrowError("Script Tokeniser", "CannotParseNonLSScriptFileException");
                    TLR.FailureReason = "CannotParseNonLSScriptFileException";
                    return TLR;
                }
                else
                {
                    Logging.Log("Tokenising...", "Script Tokeniser");

                    Tokens.Add(new StartOfFileToken { ScriptName = Sc.Name });

                    // set cur line
                    int CurrentLine = 0;

                    for (int j = 0; j < Sc.ScriptContent.Count; j++)
                    {
                        string ScriptLine = Sc.ScriptContent[j];

                        CurrentLine++;

                        List<string> Tokens_Pre = ScriptLine.Split(' ').ToList();

                        Tokens_Pre = Tokeniser_PreprocessTokenList(Tokens_Pre);

                        if (Tokens_Pre.Count == 0)
                        {
                            continue;
                        }
                        else
                        {
                            // Skip the last token as we store the next token 
                            for (int i = 0; i < Tokens_Pre.Count; i++)
                            {
                                string ThisToken = Tokens_Pre[i];

                                // comments
                                if (ThisToken == "//")
                                {
                                    Logging.Log($"Skipping commented line", "Script Tokeniser");
                                    break;
                                }
                                

                                if (ThisToken.ContainsNumeric()
                                    && !ThisToken.ContainsAlpha())
                                {
                                    Logging.Log("Identified NumberToken", "Script Tokeniser");

                                    try
                                    {
                                        NumberToken VT = new NumberToken();

                                        VT.Value = Convert.ToInt32(ThisToken);

                                        Logging.Log($"Value: {VT.Value}");
                                        Tokens.Add(VT);
                                        continue;
                                    }
                                    catch (FormatException err)
                                    {
                                        ScriptErrorManager.ThrowScriptError(new ScriptError
                                        {
                                            ScriptName = Sc.Name,
                                            Line = ScriptLine,
                                            LineNumber = CurrentLine,
                                            Id = 1007,
                                            Severity = MessageSeverity.Error,
                                            Description = "Invalid numerical token!", // shouldn't happen but w/e
#if DEBUG
                                            BaseException = err
#endif

                                        });
                                    }


                                }
                                else
                                {
                                    switch (ThisToken)
                                    {
                                        case "{":

                                            if (CurScope.Type == ScriptScopeType.Statement
                                                || CurScope.Type == ScriptScopeType.Function)
                                            {
                                                continue;
                                            }
                                            else
                                            {

                                                ScriptErrorManager.ThrowScriptError(new ScriptError
                                                {
                                                    ScriptName = Sc.Name,
                                                    Line = ScriptLine,
                                                    LineNumber = CurrentLine,
                                                    Id = 1005,
                                                    Severity = MessageSeverity.Error,
                                                    Description = "Open curved parentheses - { - must be in statement or function context!" // LS1005

                                                });

                                                continue;
                                            }
                                        case "}": // todo: last scope

                                            if (CurScope.Type != ScriptScopeType.Global)
                                            {
                                                CurScope.Type = ScriptScopeType.Global;
                                                continue;
                                            }
                                            else
                                            {
                                                ScriptErrorManager.ThrowScriptError(new ScriptError
                                                {
                                                    ScriptName = Sc.Name,
                                                    Line = ScriptLine,
                                                    LineNumber = CurrentLine,
                                                    Id = 1006,
                                                    Severity = MessageSeverity.Error,
                                                    Description = "Closed curved parentheses - } - must be in statement or function context!" // LS1005

                                                });

                                                continue;
                                            }

                                    }
                                }

                                if (!ThisToken.Contains("(")) 
                                {
                                    // Allow any capitalisation

                                    CurScope.Type = ScriptScopeType.Statement;

                                    if (ThisToken == null
                                        || ThisToken == "")
                                    {
                                        ScriptErrorManager.ThrowScriptError(new ScriptError
                                        {
                                            ScriptName = Sc.Name,
                                            Line = ScriptLine,
                                            LineNumber = CurrentLine,
                                            Id = 1004,
                                            Severity = MessageSeverity.Error,
                                            Description = "Invalid statement or variable declaration!" // LS1003

                                        });
                                    }
                                    else
                                    {
                                        string TokenX = ThisToken.ToLower();

                                        TypeConverter TC = TypeDescriptor.GetConverter(typeof(StatementToken));
                                        StatementToken ST = (StatementToken)TC.ConvertFrom(null, null, TokenX);

                                        if (ST == null) // assume variable declaration
                                        {
                                            // try operators

                                            TypeConverter OTC = TypeDescriptor.GetConverter(typeof(OperatorToken));

                                            OperatorToken OT = (OperatorToken)OTC.ConvertFrom(null, null, TokenX);

                                            if (OT == null) // it is a variable
                                            {
                                                Logging.Log($"Identified value: {TokenX}", "Script Tokeniser");
                                                ValueToken VT = new ValueToken();

                                                VT.ValueString = ThisToken;
                                                Tokens.Add(VT);
                                                continue;
                                            }
                                            else
                                            {
                                                Logging.Log($"Identified operator: {TokenX}", "Script Tokeniser"); 
                                                Tokens.Add(OT);
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            CurScope.Type = ScriptScopeType.Statement;
                                            Logging.Log($"Identified statement: {ST.Type}", "Script Tokeniser");
                                            Tokens.Add(ST);
                                            continue;
                                        }
                                    }

                                }
                                else // is a statement or a declaration of a variable or value 
                                {
                                    StringBuilder SB = new StringBuilder();
                                    SB.Append(ThisToken);

                                    int Pos = ThisToken.IndexOf("(");

                                    int PosEnd = ThisToken.IndexOf(")");

                                    if (PosEnd == -1)
                                    {

                                        PosEnd = ThisToken.Length;

                                        // search for the end of the method...
                                        for (int k = i + 1; k < Tokens_Pre.Count; k++)
                                        {
                                            string NextToken = Tokens_Pre[k];

                                            int NewPosEnd = NextToken.IndexOf(')');

                                            i++; // skip the tokens as they have been handled

                                            SB.Append($" {NextToken}"); // append any missing spaces betwen tokens - this will be stripped and trimmed anyway

                                            if (NewPosEnd != -1)
                                            {
                                                PosEnd = PosEnd + NewPosEnd;
                                                break;
                                            }
                                            else
                                            {
                                                PosEnd = PosEnd + NextToken.Length;
                                                continue;
                                            }

                                        }
                                    }

                                    if (Pos > PosEnd
                                        || Pos == -1
                                        || PosEnd == -1)
                                    {
                                        ScriptErrorManager.ThrowScriptError(new ScriptError
                                        {
                                            ScriptName = Sc.Name,
                                            Line = ScriptLine,
                                            LineNumber = CurrentLine,
                                            Id = 1002,
                                            Severity = MessageSeverity.Error,
                                            Description = "Closed bracket must be before open bracket in method declarations!"

                                        });
                                    }

                                    string FinalMethodString = SB.ToString();

                                    if (Tokens_Pre.Count > 1 &&
                                        !Tokens_Pre[i - 1].Contains("function"))
                                    {
                                        Logging.Log("Identified function call", "Script Tokeniser");
                                        CallToken CT = new CallToken();

                                        string NameSubstring = FinalMethodString.Substring(0, Pos);

                                        CT.Name = NameSubstring;

                                        string ParametersSubstring = FinalMethodString.Substring(Pos + 1, PosEnd - (Pos + 1)); // skip the ( & ) 

                                        // if there are no parameters it will not run. 
                                        string[] CommaArray = ParametersSubstring.Split(',');

                                        foreach (string CA in CommaArray)
                                        {
                                            Logging.Log($"Adding parameter {CA} to function", "Script Tokeniser");
                                            CT.ParameterValues.Add(CA);
                                        }

                                        Tokens.Add(CT);
                                        continue;
                                    }
                                    else
                                    {
                                        Logging.Log("Identified function declaration", "Script Tokeniser");
                                        FunctionToken FToken = new FunctionToken();

                                        CurScope.Type = ScriptScopeType.Function;

                                        string FunctionNameSubstring = FinalMethodString.Substring(0, Pos);

                                        Logging.Log($"Name: {FunctionNameSubstring}", "Script Tokeniser");

                                        string FunctionParametersSubstring = FinalMethodString.Substring(Pos + 1, PosEnd - (Pos + 1));

                                        // Obtain all function parameters
                                        string[] FunctionParameters = FunctionParametersSubstring.Split(',');

                                        foreach (string FParm in FunctionParameters)
                                        {
                                            string FParameterNoSpaces = FParm.Replace(" ", "");

                                            // todo: check for method existing...lol
                                            if (FParm.Length == 0
                                                || FParameterNoSpaces.Length == 0)
                                            {

                                                continue; 
                                            }
                                            else
                                            {

                                                Logging.Log($"Adding function parameter {FParm}", "Script Tokeniser");
                                                FToken.FunctionParameters.Add(FParm);
                                            }

                                        }

                                        //CurScope.Type = ScriptScopeType.Global;
                                        Tokens.Add(FToken);

                                        continue;
                                    }
                                }
                            }
                        }

                        Tokens.Add(new EndOfLineToken());
                    }

                    Tokens.Add(new EndOfFileToken());
                }

                // Set successful to true and return.
                TLR.Successful = true;
                TLR.TokenList = Tokens;

                
                return TLR;
            }
            catch (NotSupportedException err)
            {
                // is fatal.
                ErrorManager.ThrowError("Script Tokeniser", $"TokenisationCannotConvertTypeInternalException", err);
                return TLR;
            }
        }

        private List<string> Tokeniser_PreprocessTokenList(List<string> TokensPre)
        {
            for (int i = 0; i < TokensPre.Count; i++)
            {
                // get around foreach iteration variable issues
                string TokenPre = TokensPre[i];

                string TokenPre_NoSpaces = TokenPre.Replace(" ", "");

                // get rid of stuff 
                TokenPre = TokenPre.Trim();
                TokenPre = TokenPre.Replace("\r", "");
                TokenPre = TokenPre.Replace("\n", "");

                // adds useless fucking values unless we do this stupid fucking crap
                TokenPre = TokenPre.Replace(@"\r", "");
                TokenPre = TokenPre.Replace(@"\n", "");

                // hack
                TokensPre[i] = TokenPre; 

                if (TokenPre.Length == 0
                    || TokenPre_NoSpaces.Length == 0)
                {
                    TokensPre.Remove(TokenPre);
                    i--;

                }
            }

            return TokensPre; 
        }
    }
}