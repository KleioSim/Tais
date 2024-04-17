using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Tais.Commands;
using Tais.Entities;
using Tais.Interfaces;

public partial class CheatCommander : Node
{
    Dictionary<string, ConstructorInfo> dictCommandType;

    public override void _Ready()
    {
        dictCommandType = AppDomain.CurrentDomain.GetAssemblies()
            .Single(x => x.GetName().Name == "Tais.Commands").GetTypes()
            .Where(x => x.IsAssignableTo(typeof(ICommand)) && x.GetCustomAttribute<ExportCheatCommand>() != null)
            .ToDictionary(
                x => x.Name.ToLower(),
                x => x.GetConstructors().Single(x => x.GetCustomAttribute<CheatConstructors>() != null));

        foreach (var item in dictCommandType)
        {
            var paramNames = GetCommandParameterNames(item.Value);

            switch (paramNames.Count())
            {
                case 0:
                    CommandConsole.AddCommand(item.Key, OnCheat_0);
                    break;
                case 1:
                    CommandConsole.AddCommand(item.Key, OnCheat_1, paramNames);
                    break;
                case 2:
                    CommandConsole.AddCommand(item.Key, OnCheat_2, paramNames);
                    break;
                default:
                    throw new Exception();

            }
        }

        //System.Delegate[] @delegate = new System.Delegate[] { Print };

        //CommandConsole.AddCommand("print", @delegate[0]);
        //CommandConsole.AddCommandDescription("print", "Prints the given text in the console.");
        //CommandConsole.AddParameterDescription(CommandName: "print", param: "text", description: "The text to print.");

        //CommandConsole.AddCommand("heloworld", HelloWorld);
        //CommandConsole.AddCommandDescription("heloworld", "Prints 'Hola Mundo!' in the console.");
    }

    private IEnumerable<string> GetCommandParameterNames(ConstructorInfo constructor)
    {
        var parameterNames = constructor.GetParameters().Select(x => x.Name);
        if (constructor.DeclaringType.IsAssignableTo(typeof(ICommandWithTarget)))
        {
            parameterNames = parameterNames.Prepend("target");
        }

        return parameterNames;
    }

    //void Print(string text)
    //{
    //    GD.Print(text);
    //}

    //void HelloWorld()
    //{
    //    GD.PrintErr("Hola Mundo!");
    //}

    //[AddCommand("testing"), AddCommandDescription("[color=red]Prints on GD Console[/color]")]
    //public void testing(string text)
    //{
    //    GD.Print(text);
    //}

    private void OnCheat_1(string p1)
    {
        var commandName = CommandConsole.ConsoleHistory.Last().Split(" ")[0];

        var constructor = dictCommandType[commandName.ToLower()];

        var param = constructor.GetParameters().First();

        var converter = TypeDescriptor.GetConverter(param.ParameterType);
        if (!converter.CanConvertFrom(p1.GetType()))
        {
            throw new Exception();
        }

        var cmd = constructor.Invoke(new[] { converter.ConvertFrom(p1) }) as ICommand;
        CommandSender.Send(cmd);

        PresentBase.SendCommand(new Cmd_UIRefresh());
    }

    private void OnCheat_0()
    {
        var commandName = CommandConsole.ConsoleHistory.Last().Split(" ")[0];

        var constructor = dictCommandType[commandName];

        var cmd = constructor.Invoke(new object[] { }) as ICommand;
        CommandSender.Send(cmd);

        PresentBase.SendCommand(new Cmd_UIRefresh());
    }

    private void OnCheat_2(string p0, string p1)
    {
        var commandName = CommandConsole.ConsoleHistory.Last().Split(" ")[0];

        var constructor = dictCommandType[commandName];

        var param = constructor.GetParameters().First();

        var converter = TypeDescriptor.GetConverter(param.ParameterType);
        if (!converter.CanConvertFrom(p1.GetType()))
        {
            throw new Exception();
        }

        var cmd = constructor.Invoke(new[] { converter.ConvertFrom(p1) }) as ICommandWithTarget;
        cmd.Target = Entity.GetById<IEntity>(p0);
        if (cmd.Target == null)
        {
            throw new Exception($"can not find entity by id p0");
        }

        CommandSender.Send(cmd);

        PresentBase.SendCommand(new Cmd_UIRefresh());
    }
}