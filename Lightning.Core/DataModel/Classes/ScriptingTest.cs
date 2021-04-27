using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ScriptingTest
    /// 
    /// April 24, 2021
    /// 
    /// A test object for calling script method calls. 
    /// </summary>
    public class ScriptingTest : Instance
    {
        internal override string ClassName => "ScriptingTest";

        internal override InstanceTags Attributes => InstanceTags.Instantiable | InstanceTags.Destroyable;
        /// <summary>
        /// Tests scripting
        /// </summary>
        public void ScTest()
        {
            Logging.Log("Hi! I'm a script!", ClassName);
        }

    }
}
