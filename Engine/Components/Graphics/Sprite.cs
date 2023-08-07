using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Components.Graphics;

public class Sprite : Component
{
    public Texture2D Image { get; set; }
    public Color Color { get; set; } = Color.White;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; }

    public Sprite(Texture2D image)
    {
        Image = image;
    }
}