using Engine.ECS.Entities;
using Engine.ECS.GameScenes;

namespace Engine.ECS.GameSystems;

public abstract class GameSystem
{
    protected GameScene GameScene { get; private set; }
    protected readonly IList<uint> Entities = new List<uint>();
    
    internal void Initialize(GameScene parent, EntityController entityController)
    {
        GameScene = parent;
        entityController.EntityAdded += OnEntityAddedCallback;
        entityController.EntityRemoved += OnEntityRemovedCallback;
    }
    
    protected virtual void OnEntityAdded(uint entityId) { }
    
    private void OnEntityAddedCallback(uint entityId)
    {
        Entities.Add(entityId);
        OnEntityAdded(entityId);
    }
    
    protected virtual void OnEntityRemoved(uint entityId) { }
        
    private void OnEntityRemovedCallback(uint entityId)
    {
        Entities.Remove(entityId);
        OnEntityRemoved(entityId);
    }
}