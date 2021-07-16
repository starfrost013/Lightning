using Lightning.Core.NativeInterop;
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
        public static void ScTest()
        {
            MessageBox.Show("I AM A SCRIPT PLEASE LISTEN TO ME");
        }

    }
}
