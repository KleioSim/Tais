using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Tais.Commands;
using Tais.Entities;
using Tais.Interfaces;

public partial class CheatCommand : Node
{
    Dictionary<string, Type> dictCommandType;

    ISession Session
    {
        get
        {
            var global = GetNode<Global>("/root/Global");
            return global.session;
        }
    }

    private void Foo(string a, string b)
    {
        ConsoleMono.Print(a + " " + b);
    }

    private void OnCheat(string p0, string p1)
    {
        try
        {
            var commandName = ConsoleMono.GetPrevCommand().Split(" ")[0];

            var type = dictCommandType[commandName];

            var constructor = type.GetConstructors().First();
            var param = constructor.GetParameters().First();

            var converter = TypeDescriptor.GetConverter(param.ParameterType);
            if (!converter.CanConvertFrom(p1.GetType()))
            {
                throw new Exception();
            }

            var cmd = Activator.CreateInstance(type, new[] { converter.ConvertFrom(p1) }) as ICommand;
            cmd.Target = Entity.GetById<IEntity>(p0);

            CommandSender.Send(cmd);

            PresentBase.SendCommand(new Cmd_UIRefresh());
        }
        catch (Exception e)
        {
            ConsoleMono.Print(e.ToString());
        }
    }

    public override void _Ready()
    {
        base._Ready();

        dictCommandType = AppDomain.CurrentDomain.GetAssemblies()
            .Single(x => x.GetName().Name == "Tais.Commands").GetTypes()
            .Where(x => x.IsAssignableTo(typeof(ICommand)) && x.GetCustomAttribute<ExportCheatCommand>() != null)
            .ToDictionary(x => x.Name, x => x);

        foreach (var pair in dictCommandType)
        {
            ConsoleMono.CreateCommand(pair.Key, OnCheat);
        }

        ConsoleMono.CreateCommand("foo", Foo); //You can pass method directly as delegate
        ConsoleMono.CreateCommand("foo2", this, MethodName.Foo); // Or you can pass target object and method name
                                                                 //ConsoleMono.CreateCommand("bar", Bar); //Exception: method is static
    }
}