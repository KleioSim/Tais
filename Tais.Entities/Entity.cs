using shortid.Configuration;
using shortid;
using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.Entities;

public class Entity<T> : Entity
    where T : IEntityDef
{
    public new T Def
    {
        get => (T)base.Def;
        set => base.Def = value;
    }
}

public class Entity : IEntity
{
    public static Action<IBuffer, object>? OnBufferAdded;
    public static Action<IBuffer, object>? OnBufferRemoved;

    private static Dictionary<string, IEntity> _entities = new Dictionary<string, IEntity>();

    public static IEnumerable<IEntity> GetAll()
    {
        return _entities.Values.ToArray();
    }

    public static T GetById<T>(string Id)
        where T : IEntity
    {
        return (T)_entities[Id];
    }

    public string Id { get; }

    public IEntityDef Def { get; set; }

    public IEnumerable<IBuffer> Buffers => buffers;

    private List<IBuffer> buffers = new List<IBuffer>();

    public Entity()
    {
    re_generate:
        Id = GenerateId().ToLower();
        if (_entities.ContainsKey(Id))
            goto re_generate;

        _entities.Add(Id, this);
    }

    private string GenerateId()
    {
        var options = new GenerationOptions(useNumbers: true, useSpecialCharacters: false, length: 8);
        return ShortId.Generate(options);
    }

    public void RemoveBuffer(IBuffer buff)
    {
        buffers.Remove(buff);

        OnBufferRemoved?.Invoke(buff, this);
    }

    public void AddBuffer(IBuffer buff)
    {
        buffers.Add(buff);

        OnBufferAdded?.Invoke(buff, this);
    }
}