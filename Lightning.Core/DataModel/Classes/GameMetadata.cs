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
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int RevisionNumber { get; set; }
    }
}
