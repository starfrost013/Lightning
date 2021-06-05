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
    /// April 16, 2021 (modified May 28, 2021) 
    /// 
    /// Tokenises a LightningScript file - converts it to a sequence of Tokens that can be easily parsed.
    /// </summary>
    public class ScriptTokeniser
    {
        public ScriptScope CurScope { get; set; }

        public ScriptTokeniser()
        {
            CurScope = new ScriptScope();
            ASTPatterns.RegisterPatterns();
        }

        public ASTTreeSectionResult Tokenise(Script Sc)
        {
            ASTTreeSectionResult TLR = new ASTTreeSectionResult();

            GlobalSettings GS = DataModel.GetGlobalSettings();

            if (!GS.UseASTTokeniser)
            {
                // DO BANNED OLD TOKENISER
                ErrorManager.ThrowError("Script Tokeniser", "OldTokeniserRemovedException");
                
                // Fake being successful.
                // Set successful to true and return.
                TLR.Successful = true;
                TLR.TokenList = new TokenCollection();


                return TLR;

            }
            else
            {
                string LogComponent = "AST Tokeniser";

                // Tokeniser 2.0
                Logging.Log("Building syntax tree...", LogComponent);

                TokenCollection TC = new TokenCollection();

                Logging.Log("Phase 1 parsing in progress...", LogComponent);

                ASTTreeSectionResult ASTSR = Tokeniser2_ParsePhase1(Sc);
                
                Logging.Log("Phase 2 parsing in progress...", LogComponent);

                ASTTreeSectionResult ASTSR_Phase2 = Tokeniser2_ParsePhase2(ASTSR.TokenList);

                TC.Subsume(ASTSR_Phase2.TokenList);

                ASTSR.Successful = true;
                return ASTSR; 

            }
            
        }

        /// <summary>
        /// Performs Phase 1 AST tokenisation.
        /// </summary>
        /// <param name="Sc"></param>
        /// <returns></returns>
        private ASTTreeSectionResult Tokeniser2_ParsePhase1(Script Sc)
        {
            ASTTreeSectionResult ASTSR = new ASTTreeSectionResult();
            ASTSR.TokenList.Add(new StartOfFileToken());

            foreach (string ScriptLine in Sc.ScriptContent)
            {
                ASTTreeSectionResult ASTSR2 = Tokeniser2_ParseASTSectionPhase1(ScriptLine);
                ASTSR.TokenList.Subsume(ASTSR2.TokenList);
            }

            return ASTSR;
            
        }

        private ASTTreeSectionResult Tokeniser2_ParseASTSectionPhase1(string Line)
        {
            
            string[] LineComponents = Line.Split(' ');

            List<Token> TempPhase1List = new List<Token>();
            ASTTreeSectionResult ASTSR = new ASTTreeSectionResult();

            foreach (string LineComponent in LineComponents)
            {
                ASTTreeSectionResult ASTSR2 = Tokeniser2_ParseASTSection_ParseComponentPhase1(LineComponent);

                ASTSR.TokenList.Subsume(ASTSR2.TokenList);
            }

            EndOfLineToken EOFT = new EndOfLineToken();

            ASTSR.TokenList.Add(EOFT);

            return ASTSR; 
        }

        private ASTTreeSectionResult Tokeniser2_ParseASTSection_ParseComponentPhase1(string LineComponent)
        {
            ASTTreeSectionResult ASTSR = new ASTTreeSectionResult(); 

            if (LineComponent.ExclusivelyContainsNumeric())
            {
                NumberToken NT = new NumberToken();
                NT.Value = Convert.ToInt32(LineComponent);

                ASTSR.TokenList.Add(NT);
                ASTSR.Successful = true; 
                return ASTSR; 
            }
            else
            {
                OperatorToken OT = OperatorToken.FromString(LineComponent);

                if (OT != null)
                {
                    ASTSR.TokenList.Add(OT);
                    ASTSR.Successful = true;
                    return ASTSR;
                }
                else
                {
                    StatementToken ST = StatementToken.FromString(LineComponent);

                    if (ST != null)
                    {
                        ASTSR.TokenList.Add(ST);
                        ASTSR.Successful = true;
                        return ASTSR;
                    }
                    else
                    {
                        ValueToken VT = new ValueToken();
                        VT.Value = LineComponent;
                        ASTSR.Successful = true;
                        ASTSR.TokenList.Add(VT);
                        return ASTSR; 
                    }
                }
            }

        }

        /// <summary>
        /// Performs Phase 2 AST tokenisation. Transforms a flat list of tokens into an AST tree.
        /// </summary>
        /// <param name="FlatList"></param>
        /// <returns></returns>
        private ASTTreeSectionResult Tokeniser2_ParsePhase2(TokenCollection FlatList)
        {
            // PATTERN RECOGNITION TIME LOL

            string ComponentName = "AST Tokeniser";

            Logging.Log("Pattern identification in progress...", ComponentName);

            ASTTreeSectionResult ASTSR = new ASTTreeSectionResult();

            for (int i = 1; i < FlatList.Count; i++)
            {
                // Basic pattern recognition 
                // Operator token
                foreach (ASTPattern ASTP in ASTPatterns.Patterns)
                {
                    if (ASTP.TokenList.Count < 2)
                    {
                        string ErrorMessage = "An ASTPattern must have at least three elements - cannot access its index 2 or below!";

                        ErrorManager.ThrowError("AST Tokeniser", "AstPatternMustHaveAtLeast3Elements");

                        ASTSR.FailureReason = ErrorMessage;
                        return ASTSR; 
                    }
                    else
                    {

                        string CompName = ASTP.PatternName;

                        Logging.Log($"Identified pattern: {CompName}", ComponentName);

                        foreach (Token ASTPToken in ASTP.TokenList)
                        {
                            if (ASTPToken.IsCentralToken)
                            {
                                int CurFlatListID = i - 1; 

                                if (ASTP.TokenList.Count - CurFlatListID < 0)
                                {

                                }
                                else
                                {
                                    for (int j = CurFlatListID; i < CurFlatListID + ASTP.TokenList.Count; j++)
                                    {

                                        Token ListToken = FlatList[CurFlatListID];
                                        CurFlatListID++;

                                        if (ListToken != ASTPToken)
                                        {
                                            ListToken.Children.Add(ASTPToken);
                                            FlatList.Remove(ASTPToken);
                                        }
                                    }

                                }
                                
                            }
                        }
                    }

                }
            }


            ASTSR.TokenList = FlatList;
            ASTSR.Successful = true;
            return ASTSR;
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