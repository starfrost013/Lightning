using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// GuiElement
    /// 
    /// June 30, 2021
    /// 
    /// Defines an individual GUI element. 
    /// </summary>
    public class GuiElement : GuiRoot
    {
        internal override string ClassName => "GuiElement";

        internal override InstanceTags Attributes => base.Attributes | InstanceTags.ParentCanBeNull | InstanceTags.UsesCustomRenderPath; // THESE twomust be set for all guielements
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
        /// Gets or sets the background colour of this GuiElement.
        /// </summary>
        public Color4 BackgroundColour { get; set; }


        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="SDL_Renderer"></param>
        /// <param name="Tx"></param>
        public override void OnClick(object Sender, ClickEventArgs EventArgs)
        {
            return; 
        }
    }
}
