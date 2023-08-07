using Engine.Entities;
using Microsoft.Xna.Framework;

namespace Engine.GameSystems;

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