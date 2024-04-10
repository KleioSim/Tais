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
            switch (GetCommandParametersCount(item.Value))
            {
                case 0:
                    CommandConsole.AddCommand(item.Key, OnCheat_0);
                    break;
                case 2:
                    CommandConsole.AddCommand(item.Key, OnCheat_2);
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

    private int GetCommandParametersCount(ConstructorInfo constructor)
    {
        if (constructor.DeclaringType.IsAssignableTo(typeof(ICommandWithTarget)))
        {
            return constructor.GetParameters().Length + 1;
        }

        return constructor.GetParameters().Length;
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

        CommandSender.Send(cmd);

        PresentBase.SendCommand(new Cmd_UIRefresh());
    }
}