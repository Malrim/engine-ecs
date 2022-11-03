namespace Engine.ECS.Entities;

internal sealed class EntityController
{
    private uint _nextEntityId;
    private readonly Queue<Entity> _addedEntities = new();
    private readonly Queue<uint> _removedEntities = new();
    private readonly IDictionary<uint, Entity> _entities = new Dictionary<uint, Entity>();

    public Entity CreateEntity()
    {
        var entity = new Entity { Id = _nextEntityId++ };
        _addedEntities.Enqueue(entity);

        return entity;
    }
}