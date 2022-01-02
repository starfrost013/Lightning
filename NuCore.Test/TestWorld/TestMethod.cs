using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Test
{
    /// <summary>
    /// TestMethod
    /// 
    /// November 11, 2021
    /// 
    /// A test method (takes a Scene as a parameter)
    /// </summary>
    public delegate TestResult TestMethod
    (
        Scene Sc
    ); 
}
