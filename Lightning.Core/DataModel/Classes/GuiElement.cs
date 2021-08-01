﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// GuiElement
    /// 
    /// June 30, 2021 (modified July 16, 2021)
    /// 
    /// Defines an individual GUI element. 
    /// </summary>
    public class GuiElement : GuiRoot
    {
        internal override string ClassName => "GuiElement";

        internal override InstanceTags Attributes => base.Attributes | InstanceTags.ParentCanBeNull | InstanceTags.UsesCustomRenderPath; // THESE twomust be set for all guielements

        /// <summary>
        /// The horizontal alignment of this element. 
        /// </summary>
        public Alignment HorizontalAlignment { get; set; }

        /// <summary>
        /// The vertical alignment of this element.
        /// </summary>
        public Alignment VerticalAlignment { get; set; }

        /// <summary>
        /// This is completely fucking bullshit but is required for screengui and it makes me sad
        /// </summary>
        internal bool ForceToScreen { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="SDL_Renderer"></param>
        /// <param name="Tx"></param>
        public override void Render(Renderer SDL_Renderer, Texture Tx)
        {
            base.Render(SDL_Renderer, Tx);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="SDL_Renderer"></param>
        /// <param name="Tx"></param>
        public override void OnClick(object Sender, MouseEventArgs EventArgs)
        {
            return; 
        }
    }
}
