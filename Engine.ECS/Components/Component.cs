using Engine.ECS.Entities;

namespace Engine.ECS.Components;

public abstract class Component
{
    public Entity Parent { get; internal set; }
}