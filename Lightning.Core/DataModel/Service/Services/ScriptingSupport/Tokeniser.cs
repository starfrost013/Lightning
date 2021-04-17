using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Tokeniser
    /// 
    /// April 16, 2021
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
            }
            else
            {
                Tokens.Add(new StartOfFileToken { ScriptName = Sc.Name });

                Tokens.Add(new EndOfFileToken());
            }

            // Set successful to true and return.
            TLR.Successful = true; 
            TLR.TokenList = Tokens;
            return TLR;
        }
    }
}
