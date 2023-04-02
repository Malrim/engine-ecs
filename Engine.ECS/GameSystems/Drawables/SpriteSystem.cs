using Engine.ECS.Components.Graphics;
using Engine.ECS.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.ECS.GameSystems.Drawables;

public class SpriteSystem : DrawableGameSystem
{
    protected override ComponentTypes ComponentTypes => new ComponentTypes().WithRequiredType<Sprite>();
    
    protected override void Draw(Entity entity, SpriteBatch spriteBatch, GameTime gameTime)
    {
        var sprite = entity.GetComponent<Sprite>();

        spriteBatch.Draw(sprite.Image, entity.Transform.Position, null, sprite.Color, entity.Transform.Rotation,
            entity.Transform.Center, entity.Transform.Scale, sprite.SpriteEffects, sprite.LayerDepth
        );
    }
}