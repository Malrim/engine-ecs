using Microsoft.Xna.Framework;

namespace Engine.ECS.Components.Physics;

public class Rigidbody : Component
{
    public Vector2 Velocity { get; set; }
}