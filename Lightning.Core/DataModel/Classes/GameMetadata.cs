using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Metadata (inherits from SerialisableObject)
    /// 
    /// Contains information about the file. 
    /// </summary>
    public class GameMetadata : SerialisableObject
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string ClassName => "GameMetadata";

        /// <summary>
        /// The author of this Game.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The description of this Game.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The creation date of this Game.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// The date this Game was last modified.
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// The last 
        /// </summary>
        public int RevisionNumber { get; set; }
    }
}
