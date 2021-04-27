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
        /// An else statement
        /// </summary>
        Else = 2,
        /// <summary>
        /// A while statement
        /// </summary>
        While = 3,

        /// <summary>
        /// A for statement
        /// </summary>
        For = 4,

        /// <summary>
        /// A return statement
        /// </summary>
        Return = 5,

        /// <summary>
        /// A function declaration statement
        /// </summary>
        FuncDec = 6,

        /// <summary>
        /// A break statement
        /// </summary>
        Break = 7,

        /// <summary>
        /// A continue statement
        /// </summary>
        Continue = 8,

        /// <summary>
        /// A debug break statement (only available in Debug builds)
        /// </summary>
        DebugBreak = 9

    }
}
