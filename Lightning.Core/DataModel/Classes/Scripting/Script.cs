using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Script
    /// 
    /// April 16, 2021 (modified April 28, 2021) 
    /// 
    /// Defines a LightningScript script. 
    /// </summary>
    public class Script : SerialisableObject
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        internal override string ClassName => "Script";

        /// <summary>
        /// A list of the script's lines.
        /// </summary>
        internal List<string> ScriptContent { get; set; }
        
        private string _content { get; set; }
        /// <summary>
        /// Used for seamless DataModel serialisation. 
        /// </summary>
        public string Content
        {
            get
            {
                return _content;
            }

            set 
            {
                string[] SplitX = value.Split('\n');

                if (SplitX.Length != 0)
                {
                    ScriptContent = SplitX.ToList();
                }

                _content = value; 
            }
        }
        
        /// <summary>
        /// The list of tokens that make up the script.
        /// </summary>
        internal List<Token> Tokens { get; set; }

        /// <summary>
        /// Backing field for <see cref="CurToken"/>.
        /// </summary>
        private int _curtoken { get; set; }

        /// <summary>
        /// The index of the current token. 
        /// </summary>
        internal int CurToken { get
            {
                return _curtoken; 
            }

            set
            {
                if (value < 0 ||
                    value > Tokens.Count)
                {
                    ScriptErrorManager.ThrowScriptError(new ScriptError
                    {
                        ScriptName = Name,
                        Id = 1008,
                        Severity = MessageSeverity.Error,
                        Description = "Script does not have a valid end of file token or CurToken somehow ended up below 0 -- internal bug!!" // LS1008

                    });

                }
                else
                {
                    _curtoken = value;
                }
            }
        }

        internal void Update()
        {
            Token CToken = Tokens[CurToken];
        }

    }
}
