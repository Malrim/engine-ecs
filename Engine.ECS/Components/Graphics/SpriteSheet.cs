using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.ECS.Components.Graphics;

public class SpriteSheet : Sprite
{
    public int SpriteWidth => Image.Width / _columns;
    public int SpriteHeight => Image.Height / _rows;

    private readonly int _rows;
    private readonly int _columns;
    
    public SpriteSheet(Texture2D image, int rows, int columns) : base(image)
    {
        _rows = rows;
        _columns = columns;
    }

    public Rectangle GetSpriteArea(int row, int column) =>
        new(SpriteWidth * column, SpriteHeight * row, SpriteWidth, SpriteHeight);
}