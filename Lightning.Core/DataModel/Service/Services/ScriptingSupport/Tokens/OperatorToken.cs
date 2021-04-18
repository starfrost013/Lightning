using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// OperatorToken
    /// 
    /// April 16, 2021 (modified April 17, 2021: Add ToString())
    /// 
    /// Defines an operator token.
    /// An operator token is a token specific to an individual operator.
    /// </summary>
    /// 
    [TypeConverter(typeof(OperatorTokenConverter))]
    public class OperatorToken : Token 
    {
        public OperatorTokenType Type { get; set; }

        public override string ToString()
        {
            switch (Type)
            {
                case OperatorTokenType.Assignment:
                    return "=";
                case OperatorTokenType.Divide:
                    return "/";
                case OperatorTokenType.Equality:
                    return "==";
                case OperatorTokenType.Inequality:
                    return "!=";
                case OperatorTokenType.Minus:
                    return "-";
                case OperatorTokenType.Modulus:
                    return "%";
                case OperatorTokenType.Multiply:
                    return "*";
                case OperatorTokenType.Plus:
                    return "+";
 
            }

            return null; // if this runs something bad hapened
        }

        /// <summary>
        /// Converts an OperatorToken from a string. Used by the TypeConverter for OperatorToken. 
        /// </summary>
        /// <returns></returns>
        public static OperatorToken FromString(string Str)
        {
            OperatorToken OT = new OperatorToken();

            switch (Str)
            {
                case "=":
                    OT.Type = OperatorTokenType.Assignment;
                    return OT;
                case "==":
                    OT.Type = OperatorTokenType.Equality;
                    return OT;
                case "!=":
                    OT.Type = OperatorTokenType.Inequality;
                    return OT;
                case "+":
                    OT.Type = OperatorTokenType.Plus;
                    return OT;
                case "-":
                    OT.Type = OperatorTokenType.Minus;
                    return OT;
                case "*":
                    OT.Type = OperatorTokenType.Multiply;
                    return OT;
                case "/":
                    OT.Type = OperatorTokenType.Divide;
                    return OT;
                case "%":
                    OT.Type = OperatorTokenType.Modulus;
                    return OT;
            }

            return null;
        }
    }
}
