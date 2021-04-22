using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// StatementType
    /// 
    /// April 22, 2021
    /// 
    /// Defines a type of statement to be used. A statement can define control flow, end a loop, restart a loop, etc...
    /// </summary>
    public enum StatementTokenType
    {
        /// <summary>
        /// An if statement - runs the statements within its scoop
        /// </summary>
        If = 0,

        /// <summary>
        /// YandereDev getting hard right about here
        /// </summary>
        ElseIf = 1,

        /// <summary>
        /// A while statement
        /// </summary>
        While = 2,

        /// <summary>
        /// A for statement
        /// </summary>
        For = 3,

        /// <summary>
        /// A return statement
        /// </summary>
        Return = 4,

        /// <summary>
        /// A function declaration statement
        /// </summary>
        FuncDec = 5,

        /// <summary>
        /// A break statement
        /// </summary>
        Break = 6,

        /// <summary>
        /// A continue statement
        /// </summary>
        Continue = 7,

        /// <summary>
        /// A debug break statement (only available in Debug builds)
        /// </summary>
        DebugBreak = 8

    }
}
