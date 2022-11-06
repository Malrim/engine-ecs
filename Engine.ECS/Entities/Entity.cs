using Engine.ECS.Components;
using Engine.ECS.GameScenes;

namespace Engine.ECS.Entities;

public sealed class Entity
{
    public uint Id { get; internal init; }
    public string Tag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    private bool _isLoaded;
    private readonly IDictionary<Type, Component> _components = new Dictionary<Type, Component>();

    internal void Initialize(GameScene gameScene)
    {
        foreach (var behaviourComponent in _components.Values.OfType<BehaviourComponent>())
        {
            behaviourComponent.Start(gameScene);
        }

        _isLoaded = true;
    }

    public void AttachComponent(Component component)
    {
        if (_isLoaded)
        {
            throw new Exception("Cannot add components at runtime!");
        }
        
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