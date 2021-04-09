using Lightning.Core.SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Lightning   SDL_TexturePtr (Texture)
    /// </summary>
    public class Texture : SerialisableObject
    {
        private IntPtr SDLTexturePtr { get; set; }
    }
}
