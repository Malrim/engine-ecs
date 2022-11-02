using Engine.ECS.Components;

namespace Engine.ECS.Entities;

public sealed class Entity
{
    public uint Id { get; internal set; }
    public string Tag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    private readonly IDictionary<Type, Component> _components = new Dictionary<Type, Component>();
}