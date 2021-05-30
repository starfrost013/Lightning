using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// TokenCollection
    /// 
    /// May 29, 2021
    /// 
    /// Specialised tokencollection for the AST Tokeniser.
    /// </summary>
    public class TokenCollection : IEnumerable
    {
        public List<Token> Tokens { get; set; }

        public TokenCollection()
        {
            Tokens = new List<Token>(); 
        }

        public TokenCollection(List<Token> TokenList)
        {
            foreach (Token Token in TokenList)
            {
                Tokens.Add(Token);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator(); 
        }

        public TokenCollectionEnumerator GetEnumerator() => new TokenCollectionEnumerator(TokenList);
    }

    public class TokenCollectionEnumerator : IEnumerator
    {

        public TokenCollectionEnumerator(List<Token> TokenList)
        {
            foreach (Token Token in TokenList)
            {
                Tokens.Add(Token);
            }
        }

        public List<Token> Tokens { get; set; }
        public Token Current
        {
            get
            {
                try
                {
                    return Tokens[Position];
                }
                catch (IndexOutOfRangeException err)
                {
                    ErrorManager.ThrowError("AST Tokeniser", "TokenCollectionIndexOutOfRangeException", err.Message, err);
                    return null; // never runs
                }
            }
        }

        public int Position;

        public bool MoveNext()
        {
            Position++;
            return (Position < Tokens.Count);
        }

        public void Reset() => Position = -1;

        object IEnumerator.Current
        {
            get
            {
                return(object)Current;
            }
           
        }
    }
}
