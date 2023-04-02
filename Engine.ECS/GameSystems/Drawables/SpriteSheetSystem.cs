using Engine.ECS.Components.Animations;
using Engine.ECS.Components.Graphics;
using Engine.ECS.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.ECS.GameSystems.Drawables;

public class SpriteSheetSystem : DrawableGameSystem
{
    protected override ComponentTypes ComponentTypes =>
        new ComponentTypes().WithRequiredType<SpriteSheet>().WithRequiredType<Animator>();
    
    protected override void Draw(Entity entity, SpriteBatch spriteBatch, GameTime gameTime)
    {
        var spriteSheet = entity.GetComponent<SpriteSheet>();
        var animator = entity.GetComponent<Animator>();

        var animation = animator.GetPlayableAnimation();
            
        spriteBatch.Draw(spriteSheet.Image, entity.Transform.Position,
            spriteSheet.GetSpriteArea(animation.AnimationType, animation.GetFrame().Id),
            spriteSheet.Color, entity.Transform.Rotation, entity.Transform.Center, entity.Transform.Scale, spriteSheet.SpriteEffects,
            spriteSheet.LayerDepth
        );
    }
}