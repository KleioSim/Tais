using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Tais.Commands;
using Tais.Entities;
using Tais.Interfaces;

public partial class CheatCommander : Node
{
    public override void _Ready()
    {
        var cmdConstructors = AppDomain.CurrentDomain.GetAssemblies()
            .Single(x => x.GetName().Name == "Tais.Commands").GetTypes()
            .Where(x => x.IsAssignableTo(typeof(ICommand)) && x.GetCustomAttribute<ExportCheatCommand>() != null)
            .Select(x => x.GetConstructors().Single(x => x.GetCustomAttribute<CheatConstructors>() != null))
            .ToList();

        foreach (var constructor in cmdConstructors)
        {
            AddCommand(constructor);
        }

        //System.Delegate[] @delegate = new System.Delegate[] { Print };

        //CommandConsole.AddCommand("print", @delegate[0]);
        //CommandConsole.AddCommandDescription("print", "Prints the given text in the console.");
        //CommandConsole.AddParameterDescription(CommandName: "print", param: "text", description: "The text to print.");

        //CommandConsole.AddCommand("heloworld", HelloWorld);
        //CommandConsole.AddCommandDescription("heloworld", "Prints 'Hola Mundo!' in the console.");
    }

    private void AddCommand(ConstructorInfo constructor)
    {
        Action<string[]> action = (parameters) =>
        {
            IEntity target = null;
            if (constructor.DeclaringType.IsAssignableTo(typeof(ICommandWithTarget)))
            {
                var global = GetNode<Global>("/root/Global");
                target = global.session.Entities.SingleOrDefault(x => x.Id == parameters[0]);
                if (target == null)
                {
                    throw new Exception($"can not find entity by id {parameters[0]}");
                }

                parameters = parameters.Skip(1).ToArray();
            }


            var converters = constructor.GetParameters().Select(x => TypeDescriptor.GetConverter(x.ParameterType)).ToArray();
            var convertedParams = new List<object>();
            for (int i = 0; i < converters.Length; i++)
            {
                convertedParams.Add(converters[i].ConvertFrom(parameters[i]));
            }

            var cmd = constructor.Invoke(convertedParams.ToArray()) as ICommand;
            if (cmd is ICommandWithTarget cmdExt)
            {
                cmdExt.Target = target;
            }

            CommandSender.Send(cmd);

            PresentBase.SendCommand(new Cmd_UIRefresh());
        };

        CommandConsole.AddCommand(constructor.DeclaringType.Name.ToLower(), action, GetCommandParameterNames(constructor));
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
}