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
        /// Backing field for CurrentlyExecutingLine
        /// </summary>
        private int _currentlyexecutingline { get; set; }

        public override void OnCreate()
        {
            ScriptContent = new List<string>(); 
        }

        /// <summary>
        /// The currently executing line.
        /// </summary>
        internal int CurrentlyExecutingLine { get
            {
                return _currentlyexecutingline;
            }
            set
            {
                if (_currentlyexecutingline < 0
                    || _currentlyexecutingline > ScriptContent.Count)
                {
                    if (Name != null)
                    {
                        ErrorManager.ThrowError(ClassName, "ErrorAcquiredInvalidLineException", $"Attempted to acquire invalid line {value} for the script with name {Name}!");
                    }
                    else
                    {
                        ErrorManager.ThrowError(ClassName, "ErrorAcquiredInvalidLineException", $"Attempted to acquire invalid line {value} for a script!");
                    }
                    
                }
                else
                {
                    _currentlyexecutingline = value; 
                }
            }

        }

        /// <summary>
        /// A list of the script's lines.
        /// </summary>
        internal List<string> ScriptContent { get; set; }
        
        /// <summary>
        /// The tokenised script content if this script.
        /// </summary>
        internal List<Token> TokenisedScriptContent { get; set; }

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
        internal TokenCollection Tokens { get; set; }

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
