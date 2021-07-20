using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    public class DefaultPhysicsController : PhysicsController
    {
        internal override string ClassName => "DefaultPhysicsController";

        public override void OnInit()
        {
            return; 
        }

        public override void OnTick()
        {
            return;
        }
        public override void OnCollisionStart()
        {
            return;
        }

        public override void OnCollisionEnd()
        {
            return;
        }
    }
}
