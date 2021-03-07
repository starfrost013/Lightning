using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{

    /// <summary>
    /// This is a custom error handler that can be used by an error.
    /// 
    /// It currently takes an Error object as its sole parameter and returns nothing (void).
    /// 
    /// THIS WILL OVERRIDE ALL BEHAVIOUR! 
    /// </summary>
    /// <param name="err"></param>
    /// <returns></returns>
    public delegate void CustomErrorHandler(Error err);
    /// <summary>
    /// Lightning
    /// 
    /// Error
    /// 
    /// Non-instanceable object (not part of the DataModel)
    /// </summary>
    public class Error
    {
        public Exception BaseException { get; set; }
        public CustomErrorHandler CustomErrHandler { get; set; }
        public string Description { get; set; }

        public uint Id { get; set; }
        public string Name { get; set; }
        public MessageSeverity Severity { get; set; }


    }
}
