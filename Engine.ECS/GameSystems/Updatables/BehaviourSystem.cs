using Engine.ECS.Components;
using Engine.ECS.Entities;
using Microsoft.Xna.Framework;

namespace Engine.ECS.GameSystems.Updatables;

public class BehaviourSystem : UpdatableGameSystem
{
    protected override ComponentTypes ComponentTypes => new ComponentTypes().WithRequiredType<BehaviourComponent>();
    
    protected override void Update(Entity entity, GameTime gameTime)
    {
        foreach (var behaviourComponent in entity.GetBehaviourComponents())
        {
            behaviourComponent.Update(gameTime);
        }
    }
}