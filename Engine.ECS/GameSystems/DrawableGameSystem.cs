using Engine.ECS.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.ECS.GameSystems;

public abstract class DrawableGameSystem : GameSystem
{
    protected abstract void Draw(Entity entity, SpriteBatch spriteBatch, GameTime gameTime);

    internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        foreach (var entityId in Entities)
        {
            Draw(GameScene.GetEntity(entityId), spriteBatch, gameTime);
        }
    }
}