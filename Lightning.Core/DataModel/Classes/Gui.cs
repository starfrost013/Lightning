using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// Gui
    /// 
    /// July 7, 2021
    /// 
    /// Defines a root gui class shared across all gui types 
    /// </summary>
    public class Gui : GuiRoot
    {
        internal override string ClassName => "Gui";

        internal override InstanceTags Attributes => InstanceTags.UsesCustomRenderPath; //DO NOT MAKE INSTANTIABLE

        public override void OnCreate()
        {
            Click += OnClick; 
        }

        public override void OnClick(object Sender, MouseEventArgs EventArgs) // probably needs to be refactored
        {
            foreach (Instance Ins in Children)
            {
                Type TypeOfChild = Ins.GetType();

                if (TypeOfChild == typeof(GuiElement)
                    || TypeOfChild.IsSubclassOf(typeof(GuiElement)))
                {
                    GuiElement GE = (GuiElement)Ins;

                    if (GE.Click != null)
                    {
                        if (GE.Position == null || GE.Size == null)
                        {
                            GE.Click(this, EventArgs);
                        }
                        else
                        {
                            if (EventArgs.RelativePosition.X > GE.AABB.Position.X
                                && EventArgs.RelativePosition.X < GE.AABB.Maximum.X
                                && EventArgs.RelativePosition.Y > GE.AABB.Position.Y
                                && EventArgs.RelativePosition.Y < GE.AABB.Maximum.Y)
                            {
                                GE.Click(this, EventArgs);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        
                    }
                    else
                    {
                        continue; 
                    }
                   
                }
            }
        }
    }
}
