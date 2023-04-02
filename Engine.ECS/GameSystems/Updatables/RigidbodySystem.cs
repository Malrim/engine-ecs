using Engine.ECS.Components.Physics;
using Engine.ECS.Entities;
using Microsoft.Xna.Framework;

namespace Engine.ECS.GameSystems.Updatables;

public class RigidbodySystem : UpdatableGameSystem
{
    protected override ComponentTypes ComponentTypes => new ComponentTypes().WithRequiredType<Rigidbody>();
    
    protected override void Update(Entity entity, GameTime gameTime)
    {
        var rigidbody = entity.GetComponent<Rigidbody>();
        
        entity.Transform.Position = rigidbody.Velocity;
        rigidbody.Velocity = Vector2.Zero;
    }
}