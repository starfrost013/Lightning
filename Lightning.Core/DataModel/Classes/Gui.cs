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

        public override void OnClick(object Sender, MouseEventArgs EventArgs)
        {
            foreach (Instance Ins in Children)
            {
                Type TypeOfChild = Ins.GetType();

                if (TypeOfChild == typeof(GuiElement)
                    || TypeOfChild.IsSubclassOf(typeof(GuiElement)))
                {
                    GuiElement GE = (GuiElement)Ins;

                    GE.OnClick(Ins, EventArgs);
                }
            }
        }
    }
}
