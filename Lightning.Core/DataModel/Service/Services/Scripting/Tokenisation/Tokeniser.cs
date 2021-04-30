using Lightning.Utilities; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Tokeniser
    /// 
    /// April 16, 2021 (modified April 27, 2021) 
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

                        string[] Tokens_Pre = ScriptLine.Split(' ');

                        if (Tokens_Pre.Length == 0)
                        {
                            continue;
                        }
                        else
                        {
                            // Skip the last token as we store the next token 
                            for (int i = 0; i < Tokens_Pre.Length; i++)
                            {
                                string ThisToken = Tokens_Pre[i];

                                ThisToken = ThisToken.Trim();

                                // comments
                                if (ThisToken == "//") break;

                                if (ThisToken.Length <= 2) // Operator
                                {
                                    if (ThisToken.ContainsNumeric())
                                    {
                                        Logging.Log("Identified NumberToken", "Script Tokeniser");

                                        try
                                        {
                                            NumberToken VT = new NumberToken();

                                            VT.Value = Convert.ToInt32(ThisToken);

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

                                            }) ;
                                        }


                                    }
                                    else
                                    {
                                        switch (ThisToken) 
                                        {
                                            case "{":
                                                Logging.Log("Entering function or statement", "Script Tokeniser");

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

                                                    }) ;

                                                    continue;
                                                }
                                            case "}": // todo: last scope
                                                Logging.Log("Exiting function or statement", "Script Tokeniser");
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

                                }
                                else
                                {

                                    if (ThisToken.Contains("(")) // Function declaration (requires special handling) - checks the last token to see if it matches "function"
                                    {

                                        int Pos = ThisToken.IndexOf("(");

                                        int PosEnd = ThisToken.IndexOf(")");

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


                                        if (Tokens_Pre[i-1].Contains("function"))
                                        {
                                            CallToken CT = new CallToken();

                                            string NameSubstring = ThisToken.Substring(0, ThisToken.Length - Pos);

                                            CT.Name = NameSubstring;

                                            string ParametersSubstring = ThisToken.Substring(Pos + 1, (ThisToken.Length - PosEnd) - 1); // skip the (

                                            // if there are no parameters it will not run. 
                                            string[] CommaArray = ParametersSubstring.Split(',');

                                            foreach (string CA in CommaArray)
                                            {
                                                CT.ParameterValues.Add(CA);
                                            }

                                            Tokens.Add(CT);
                                        }
                                        else
                                        {
                                            FunctionToken FToken = new FunctionToken();

                                            CurScope.Type = ScriptScopeType.Function;

                                            string FunctionNameSubstring = ThisToken.Substring(0, Pos);

                                            Logging.Log($"Name: {FunctionNameSubstring}", "Script Tokeniser");

                                            string FunctionParametersSubstring = ThisToken.Substring(Pos + 1, ThisToken.Length - PosEnd);

                                            // Obtain all function parameters
                                            string[] FunctionParameters = FunctionParametersSubstring.Split(',');

                                            foreach (string FParm in FunctionParameters)
                                            {
                                                // todo: check for method existing...lol
                                                if (FParm.Length == 0)
                                                {

                                                    ScriptErrorManager.ThrowScriptError(new ScriptError
                                                    {
                                                        ScriptName = Sc.Name,
                                                        Line = ScriptLine,
                                                        LineNumber = CurrentLine,
                                                        Id = 1003,
                                                        Severity = MessageSeverity.Error,
                                                        Description = "Closed bracket must be before open bracket in method calls!" // LS1003

                                                    });

                                                }
                                                else
                                                {
                                                    Logging.Log($"Adding function parameter {FParm}", "Script Tokeniser");
                                                    FToken.FunctionParameters.Add(FParm);
                                                }

                                            }

                                            CurScope.Type = ScriptScopeType.Global;
                                            Tokens.Add(FToken);

                                            continue;
                                        }
                                        
                                    }
                                    else // is a statement or a declaration of a variable or value 
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
                                                // try opreator

                                                TypeConverter OTC = TypeDescriptor.GetConverter(typeof(OperatorToken));

                                                OperatorToken OT = (OperatorToken)TC.ConvertFrom(null, null, TokenX);

                                                if (OT == null) // it is a variable
                                                {
                                                    Logging.Log($"Identified value: {TokenX}", "Script Tokeniser");
                                                    ValueToken VT = new ValueToken();

                                                    VT.ValueString = ThisToken;
                                                    Tokens.Add(VT);
                                                }

                                                else
                                                {
                                                    Tokens.Add(OT);
                                                }
                                            }
                                            else
                                            {
                                                Logging.Log($"Identified statement: {ST.Type}", "Script Tokeniser");
                                                Tokens.Add(ST);

                                            }
                                        }

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
    }
}
