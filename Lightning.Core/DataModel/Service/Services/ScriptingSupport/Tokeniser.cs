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
                        for (int i = 1; i < Tokens_Pre.Length - 1; i++)
                        {
                            string PreviousToken = Tokens_Pre[i - 1];
                            string ThisToken = Tokens_Pre[i];
                            string NextToken = Tokens_Pre[i + 1];

                            // Is an assignment to a variable.
                            if (!PreviousToken.ContainsNumeric()
                                && PreviousToken.ContainsAlpha()) 
                            {

                                Tokens.Add(new VariableToken { Name = PreviousToken.ToString() }) ;



                                TypeConverter TC0 = TypeDescriptor.GetConverter(typeof(OperatorToken));

                                ValueToken VT = new ValueToken();

                                TC0.ConvertTo(ThisToken, typeof(TypeConverter));
                            }
                            else if (!PreviousToken.ContainsAlpha()
                                && PreviousToken.ContainsNumeric()) // previous token is a number...lol...
                            {

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
