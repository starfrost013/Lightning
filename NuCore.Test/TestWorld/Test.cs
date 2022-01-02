using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Test
{
    /// <summary>
    /// Test
    /// 
    /// Defines a test as a part of the testworld.
    /// </summary>
    public class Test
    {
        /// <summary>
        /// The name of this test.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The current state of this test instance - see <see cref="TestState"/>.
        /// </summary>
        public TestState State { get; set; }
        
        /// <summary>
        /// Delegate for the method that will be invoked when this test is run - see <see cref="TestState"/>
        /// </summary>
        public TestMethod Method { get; set; }
    }
}
