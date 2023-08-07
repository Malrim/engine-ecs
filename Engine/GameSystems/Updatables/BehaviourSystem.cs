using Engine.Components;
using Engine.Entities;
using Microsoft.Xna.Framework;

namespace Engine.GameSystems.Updatables;

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