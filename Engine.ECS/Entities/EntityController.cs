using Engine.ECS.GameScenes;

namespace Engine.ECS.Entities;

internal sealed class EntityController
{
    public delegate void EntityEventHandler(uint entityId);
    public event EntityEventHandler EntityAdded;
    public event EntityEventHandler EntityRemoved;
    
    private uint _nextEntityId;
    private readonly GameScene _gameScene;
    private readonly Queue<Entity> _addedEntities = new();
    private readonly Queue<uint> _removedEntities = new();
    private readonly IDictionary<uint, Entity> _entities = new Dictionary<uint, Entity>();

    public EntityController(GameScene gameScene)
    {
        _gameScene = gameScene;
    }

    public Entity CreateEntity()
    {
        var entity = new Entity { Id = _nextEntityId++ };
        
        _addedEntities.Enqueue(entity);
        return entity;
    }

    public void RemoveEntity(uint entityId)
    {
        if (!_entities.ContainsKey(entityId))
        {
            throw new Exception($"Entity with id '{entityId}' does not exist!");
        }

        if (_removedEntities.Contains(entityId))
        {
            throw new Exception($"Entity with id '{entityId}' is already in the queue for deletion!");
        }
        
        _removedEntities.Enqueue(entityId);
    }

    public Entity GetEntity(uint entityId) => _entities.TryGetValue(entityId, out var entity) ? entity : null;

    public void Update()
    {
        AddEntities();
        RemoveEntities();
    }
    
    private void AddEntities()
    {
        while (_addedEntities.Count > 0)
        {
            var entity = _addedEntities.Dequeue();
            
            entity.Initialize(_gameScene);
            
            _entities.Add(entity.Id, entity);
            EntityAdded?.Invoke(entity.Id);
        }
    }
    
    private void RemoveEntities()
    {
        while (_removedEntities.Count > 0)
        {
            var entityId = _removedEntities.Dequeue();

            _entities.Remove(entityId);
            EntityRemoved?.Invoke(entityId);
        }
    }
}