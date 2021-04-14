using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// CameraType
    /// 
    /// April 14, 2021
    /// 
    /// Defines types of Camera.
    /// </summary>
    public enum CameraType
    {
        /// <summary>
        /// The camera can be fully controlled. 
        /// </summary>
        Free = 0,

        /// <summary>
        /// The camera tracks an object.
        /// </summary>
        FollowObject = 1,

        /// <summary>
        /// The camera chases an object.
        /// </summary>
        ChaseObject = 2,

        /// <summary>
        /// The camera does not move.
        /// </summary>
        Fixed = 3,

    }}
