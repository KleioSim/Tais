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
    public string Id { get; }

    public IEntityDef Def { get; set; }

    private static Dictionary<string, IEntity> _entities = new Dictionary<string, IEntity>();

    public static IEnumerable<IEntity> GetAll()
    {
        return _entities.Values;
    }

    public static T GetById<T>(string Id)
        where T : IEntity
    {
        return (T)_entities[Id];
    }

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
}