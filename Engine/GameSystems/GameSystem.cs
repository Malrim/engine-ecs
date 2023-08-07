using Engine.Entities;
using Engine.GameScenes;

namespace Engine.GameSystems;

public abstract class GameSystem
{
    protected abstract ComponentTypes ComponentTypes { get; }
    
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
        if (!ComponentTypes.Verify(GameScene.GetEntity(entityId)))
        {
            return;
        }
        
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