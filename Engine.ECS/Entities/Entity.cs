using Engine.ECS.Components;

namespace Engine.ECS.Entities;

public sealed class Entity
{
    public uint Id { get; internal set; }
    public string Tag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    private readonly IDictionary<Type, Component> _components = new Dictionary<Type, Component>();

    public void AttachComponent(Component component)
    {
        if (component == null)
        {
            throw new NullReferenceException("Component is null!");
        }

        if (_components.ContainsKey(component.GetType()))
        {
            throw new Exception($"Entity already contains component of type '{component.GetType()}'!");
        }

        component.Parent = this;
        _components.Add(component.GetType(), component);
    }

    public bool TryGetComponent<T>(out T component) where T : Component
    {
        if (!_components.TryGetValue(typeof(T), out var item))
        {
            component = null;
            return false;
        }

        component = item as T;
        return true;
    }

    public T GetComponent<T>() where T : Component
    {
        if (!TryGetComponent<T>(out var component))
        {
            throw new Exception($"Component type '{typeof(T)}' does not exist!");
        }

        return component;
    }
}