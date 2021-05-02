using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// StatementToken
    /// 
    /// April 22, 2021
    /// 
    /// Defines a statement. Woo!
    /// </summary>
    /// 
    [TypeConverter(typeof(StatementTokenConverter))]
    public class StatementToken : Token 
    {
        public StatementTokenType Type { get; set; }

        public static StatementToken FromString(string StatementString)
        {
            StatementToken St = new StatementToken();

            switch (StatementString)
            {
                case "while":
                    St.Type = StatementTokenType.While;
                    return St;
                case "for":
                    St.Type = StatementTokenType.For;
                    return St;
                case "if":
                    St.Type = StatementTokenType.If;
                    return St;
                case "function":
                case "funcdec":
                    St.Type = StatementTokenType.FuncDec;
                    return St;
                case "elif":
                case "elseif":
                    St.Type = StatementTokenType.ElseIf;
                    return St;
                case "else":
                    St.Type = StatementTokenType.Else;
                    return St;
                case "break":
                    St.Type = StatementTokenType.Break;
                    return St;
                case "debugbreak":
                    St.Type = StatementTokenType.DebugBreak;
                    return St;
                case "continue":
                    St.Type = StatementTokenType.Continue;
                    return St;
                case "return":
                    St.Type = StatementTokenType.Return;
                    return St;
            }


            return null;
        }
    }
}
