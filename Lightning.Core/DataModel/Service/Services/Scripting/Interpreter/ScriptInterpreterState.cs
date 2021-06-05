using System;
using System.Collections.Generic;
using System.Diagnostics; 
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ScriptInterpreterState
    /// 
    /// May 3, 2021 (modified June 5, 2021: Lua)
    /// </summary>
    public class ScriptInterpreterState
    {
        /// <summary>
        /// A list of variables
        /// </summary>
        public List<Variable> Variables { get; set; }

        /// <summary>
        /// An FIFO stack.
        /// </summary>
        public List<Token> Stack { get; set; }

       
        public ScriptInterpreterState()
        {
            Stack = new List<Token>();
            Variables = new List<Variable>();
        }

        /// <summary>
        /// TokenCollection?
        /// 
        /// Pops a token to the stack.
        /// </summary>
        /// <returns></returns>
        public Token Pop() => Stack[Stack.Count - 1];

        /// <summary>
        /// Pushes a token to the stack.
        /// </summary>
        /// <param name="TokenToPush"></param>

        public void Push(Token TokenToPush) => Stack.Add(TokenToPush); 
    }
}
