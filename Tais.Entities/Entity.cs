using shortid;
using shortid.Configuration;
using System.Collections;
using Tais.InitialDatas.Interfaces;
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

    public Entity(string id, T def) : base(id, def)
    {
    }
}


public class Entity : IEntity
{
    public static Action<IBuffer, object>? OnBufferAdded;
    public static Action<IBuffer, object>? OnBufferRemoved;

    public string Id { get; init; }

    public IEntityDef Def { get; set; }

    public IEnumerable<IBuffer> Buffers => buffers;

    private List<IBuffer> buffers = new List<IBuffer>();

    public Entity(string id, IEntityDef def)
    {
        Id = id;
        Def = def;
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

internal class EntityManager : IEnumerable<Entity>
{
    private List<Entity> entities = new List<Entity>();

    public IEnumerator<Entity> GetEnumerator()
    {
        return ((IEnumerable<Entity>)entities).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)entities).GetEnumerator();
    }

    public string GenerateId()
    {
        var options = new GenerationOptions(useNumbers: true, useSpecialCharacters: false, length: 8);

        string id = null;
        do
        {
            id = ShortId.Generate(options);
        }
        while (entities.Any(x => x.Id == id));

        return id;
    }

    public void AddEntity(Entity entity)
    {
        entities.Add(entity);
    }
}