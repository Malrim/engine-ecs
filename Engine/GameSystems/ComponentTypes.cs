using Engine.Components;
using Engine.Entities;

namespace Engine.GameSystems;

public sealed class ComponentTypes
{
    private List<Type> _requiredTypes;
    private List<Type> _anyTypes;
    private List<Type> _excludeTypes;

    public ComponentTypes WithRequiredType<T>() where T : Component
    {
        var type = typeof(T);
        
        EnsureUniquenessTypes(type, _anyTypes, "any");
        EnsureUniquenessTypes(type, _excludeTypes, "exclude");
        
        _requiredTypes ??= new List<Type>();
        AddType(type, _requiredTypes, "required");
        return this;
    }
    
    public ComponentTypes WithAnyType<T>() where T : Component
    {
        var type = typeof(T);
        
        EnsureUniquenessTypes(type, _requiredTypes, "required");
        EnsureUniquenessTypes(type, _excludeTypes, "exclude");
        
        _anyTypes ??= new List<Type>();
        AddType(typeof(T), _anyTypes, "any");
        return this;
    }

    public ComponentTypes WithExcludeType<T>() where T : Component
    {
        var type = typeof(T);
        
        EnsureUniquenessTypes(type, _requiredTypes, "required");
        EnsureUniquenessTypes(type, _anyTypes, "any");
        
        _excludeTypes ??= new List<Type>();
        AddType(typeof(T), _excludeTypes, "exclude");
        return this;
    }
    
    internal bool Verify(Entity entity)
    {
        if (_requiredTypes == null && _anyTypes == null && _excludeTypes == null)
        {
            return false;
        }
        
        if (_anyTypes is {Count: 1})
        {
            throw new Exception("There must be more than one type for 'any'!");
        }

        if (_requiredTypes != null && !_requiredTypes.TrueForAll(entity.ContainsComponent))
        {
            return false;
        }

        if (_anyTypes != null && !_anyTypes.Exists(entity.ContainsComponent))
        {
            return false;
        }

        if (_excludeTypes != null && _excludeTypes.Exists(entity.ContainsComponent))
        {
            return false;
        }
        
        return true;
    }

    private static void AddType(Type type, ICollection<Type> types, string typeOfTypes)
    {
        if (types.Contains(type))
        {
            throw new Exception($"Type '{type}' has already been added to '{typeOfTypes}'!");
        }
        
        types.Add(type);
    }

    private static void EnsureUniquenessTypes(Type type, ICollection<Type> types, string typeOfTypes)
    {
        if (types != null && types.Contains(type))
        {
            throw new Exception($"The '{type}' type was added as '{typeOfTypes}'!");
        }
    }
}