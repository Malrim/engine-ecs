using Engine.ECS.Entities;
using Microsoft.Xna.Framework;

namespace Engine.ECS.GameSystems;

public abstract class UpdatableGameSystem : GameSystem
{
    protected abstract void Update(Entity entity, GameTime gameTime);

    internal void Update(GameTime gameTime)
    {
        foreach (var entityId in Entities)
        {
            Update(GameScene.GetEntity(entityId), gameTime);
        }
    }
}