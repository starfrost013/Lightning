using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ASTPatterns
    /// 
    /// June 1, 2021
    /// 
    /// Defines basic syntactical patterns that are used for Phase 2 tokenisation. 
    /// </summary>
    public static class ASTPatterns
    {
        public static List<ASTPattern> Patterns { get; set; }

        /// <summary>
        /// Registers valid AST patterns.
        /// </summary>
        public static void RegisterPatterns()
        {
            Patterns.Add(new ASTPattern { PatternName = "NUMBER OPERATOR NUMBER", TokenList = new List<Token> { new NumberToken(), new OperatorToken { IsCentralToken = true }, new NumberToken() } }) ;
            Patterns.Add(new ASTPattern { PatternName = "VALUE OPERATOR NUMBER", TokenList = new List<Token> { new ValueToken(), new OperatorToken { IsCentralToken = true }, new NumberToken() } });
            Patterns.Add(new ASTPattern { PatternName = "NUMBER OPERATOR VALUE", TokenList = new List<Token> { new NumberToken(), new OperatorToken { IsCentralToken = true }, new ValueToken() } });
            Patterns.Add(new ASTPattern { PatternName = "VALUE OPERATOR VALUE", TokenList = new List<Token> { new ValueToken(), new OperatorToken { IsCentralToken = true }, new ValueToken() } });
            Patterns.Add(new ASTPattern { PatternName = "FUNCTION DECLARATION", TokenList = new List<Token> { new StatementToken { Type = StatementTokenType.FuncDec }, new OperatorToken(), new OperatorToken() } });
        }
    }
}
