using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Tais.Commands;
using Tais.Entities;
using Tais.Interfaces;
using static CheatCommand;

public partial class CheatCommand : Node
{
    Dictionary<string, ConstructorInfo> dictCommandType;

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

    private void OnCheat_2(string p0, string p1)
    {
        try
        {
            var commandName = ConsoleMono.GetPrevCommand().Split(" ")[0];

            var constructor = dictCommandType[commandName];

            var param = constructor.GetParameters().First();

            var converter = TypeDescriptor.GetConverter(param.ParameterType);
            if (!converter.CanConvertFrom(p1.GetType()))
            {
                throw new Exception();
            }

            var cmd = constructor.Invoke(new[] { converter.ConvertFrom(p1) }) as ICommandWithTarget;
            cmd.Target = Entity.GetById<IEntity>(p0);

            CommandSender.Send(cmd);

            PresentBase.SendCommand(new Cmd_UIRefresh());
        }
        catch (Exception e)
        {
            ConsoleMono.Print(e.ToString());
        }
    }

    private void OnCheat_0()
    {
        try
        {
            var commandName = ConsoleMono.GetPrevCommand().Split(" ")[0];

            var constructor = dictCommandType[commandName];

            var cmd = constructor.Invoke(new object[] { }) as ICommand;
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
            .ToDictionary(
                x => x.Name,
                x => x.GetConstructors().Single(x => x.GetCustomAttribute<CheatConstructors>() != null));

        foreach (var pair in dictCommandType)
        {
            switch (GetCommandParametersCount(pair.Value))
            {
                case 0:
                    ConsoleMono.CreateCommand(pair.Key, OnCheat_0);
                    break;
                case 2:
                    ConsoleMono.CreateCommand(pair.Key, OnCheat_2);
                    break;
                default:
                    throw new Exception();
            }

        }

        ConsoleMono.CreateCommand("foo", Foo); //You can pass method directly as delegate
        ConsoleMono.CreateCommand("foo2", this, MethodName.Foo); // Or you can pass target object and method name
                                                                 //ConsoleMono.CreateCommand("bar", Bar); //Exception: method is static
    }

    private int GetCommandParametersCount(ConstructorInfo constructor)
    {
        if (constructor.DeclaringType.IsAssignableTo(typeof(ICommandWithTarget)))
        {
            return constructor.GetParameters().Length + 1;
        }

        return constructor.GetParameters().Length;
    }

    public class CheatCommandItem
    {
        public Type CommandType { get; init; }
        public ConstructorInfo Constructor { get; init; }
    }
}