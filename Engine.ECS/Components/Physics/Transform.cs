using Microsoft.Xna.Framework;

namespace Engine.ECS.Components.Physics;

public class Transform : Component
{
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public Vector2 Center { get; set; }
    public Vector2 Scale { get; set; }
}