using Engine.Components.Physics;
using Engine.Entities;
using Microsoft.Xna.Framework;

namespace Engine.GameSystems.Updatables;

public class RigidbodySystem : UpdatableGameSystem
{
    protected override ComponentTypes ComponentTypes => new ComponentTypes().WithRequiredType<Rigidbody>();
    
    protected override void Update(Entity entity, GameTime gameTime)
    {
        var rigidbody = entity.GetComponent<Rigidbody>();
        
        entity.Transform.Position += rigidbody.Velocity;
        rigidbody.Velocity = Vector2.Zero;
    }
}