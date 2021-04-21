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
    /// April 16, 2021 (modified April 18, 2021)
    /// 
    /// Tokenises a LightningScript file - converts it to a sequence of Tokens that can be easily parsed.
    /// </summary>
    public class Tokeniser
    {
        public TokenListResult Tokenise(Script Sc)
        {
            try
            {
                TokenListResult TLR = new TokenListResult();

                List<Token> Tokens = new List<Token>();

                if (Sc.Name == null
                    || Sc.Name.Length == 0)
                {
                    ErrorManager.ThrowError("Script Tokenizer", "CannotParseNonLSScriptFileException");
                    TLR.FailureReason = "CannotParseNonLSScriptFileException";
                    return TLR;
                }
                else
                {
                    Tokens.Add(new StartOfFileToken { ScriptName = Sc.Name });


                    foreach (string ScriptLine in Sc.ScriptContent)
                    {
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

                                if (ThisToken.Length == 1)
                                {
                                    TypeConverter LCConv = TypeDescriptor.GetConverter(typeof(OperatorToken));

                                    OperatorToken OT = (OperatorToken)LCConv.ConvertFrom(ThisToken);

                                    Tokens.Add(OT);
                                }
                                else
                                {
                                    if (ThisToken.Contains("(")) // Function 
                                    {
                                        int Pos = ThisToken.IndexOf("(");
                                        
                                        if (!ThisToken.Contains(")"))
                                        {
                                            ScriptErrorManager.ThrowScriptError(new ScriptError
                                            {
                                                Name = ""
                                            });
                                        }
                                        else
                                        {
                                            int PosEnd = ThisToken.IndexOf(")");

                                            if (Pos > PosEnd)
                                            {
                                                ScriptError
                                            }

                                            FunctionToken FToken = new FunctionToken();

                                            string FunctionParametersSubstring = ThisToken.Substring(Pos, PosEnd - Pos);
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
            
        }
    }
}
