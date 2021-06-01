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

        public void Add(object Obj, Token Parent = null)
        {
            Type ObjType = Obj.GetType();

            if (ObjType != typeof(Token))
            {
                ErrorManager.ThrowError("AST Tokeniser", "CannotAddNonTokenToTokenCollectionException");
                return; 
            }
            else
            {
                if (Parent != null)
                {
                    if (!Add_SearchForParent(Parent, (Token)Obj))
                    {
                        ErrorManager.ThrowError("AST Tokeniser", "CannotAddTokenWithParentThatIsNotInTokenCollectionException");
                        return; // Do not add
                    }
                }

                Add_PerformAdd(Obj, Parent);
            }
        }

        private void Add_PerformAdd(object Obj, Token Parent = null)
        {
            if (Parent != null)
            {
                Parent.Children.Add((Token)Obj);
            }
            else
            {
                Tokens.Add((Token)Obj);
            }
        }

        
        private bool Add_SearchForParent(Token ParentObj, Token CurObj)
        {
            foreach (Token Token in Tokens)
            {
                if (Token == ParentObj)
                {
                    return true;
                }
                else
                {
                    if (Token.Children.Count > 0)
                    {
                        Add_SearchForParent(ParentObj, Token);
                    }
                    else
                    {
                        continue; 
                    }
                }
                
            }

            return false; 
        }

        public TokenCollectionEnumerator GetEnumerator() => new TokenCollectionEnumerator(Tokens);

        /// <summary>
        /// Subsumes another list of tokens into this TokenCollection, with an optional parent.
        /// </summary>
        /// <param name="TokenList"></param>
        /// <param name="Parent"></param>
        public void Subsume(TokenCollection TokenList, Token Parent = null)
        {
            foreach (Token Token in TokenList)
            {
                if (Parent != null)
                {
                    Add(Token, Parent);
                }
                else
                {
                    Add(Token);
                }

                
            }
        }

        public int Count => Count; 

        public Token this[int index] => Tokens[index];
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
