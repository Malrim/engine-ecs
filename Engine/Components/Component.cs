using Engine.Entities;

namespace Engine.Components;

public abstract class Component
{
    public Entity Parent { get; internal set; }
}