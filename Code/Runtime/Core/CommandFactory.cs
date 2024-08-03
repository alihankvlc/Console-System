using System;
using System.Linq;
using System.Reflection;

namespace Debugging.Runtime.Core
{
    public interface ICommandFactory
    {
        CommandBase CreateCommand(CommandMonoBehaviour behaviour, MethodInfo method, CommandAttribute attribute,
            Type[] parameterTypes);
    }

    public sealed class CommandFactory : ICommandFactory
    {
        public CommandBase CreateCommand(CommandMonoBehaviour behaviour, MethodInfo method, CommandAttribute attribute,
            Type[] parameterTypes)
        {
            int parametersLength = parameterTypes.Length;
            Delegate del = CreateDelegate(behaviour, method, parameterTypes);

            if (del == null)
                return null;

            return CreateCommandInstance(attribute.CommandExecuteName, attribute.CommandDescription, del,
                parameterTypes);
        }

        private Delegate CreateDelegate(CommandMonoBehaviour behaviour, MethodInfo method, Type[] parameterTypes)
        {
            Type delegateType = parameterTypes.Length switch
            {
                0 => typeof(Action),
                1 => typeof(Action<>).MakeGenericType(parameterTypes),
                2 => typeof(Action<,>).MakeGenericType(parameterTypes),
                3 => typeof(Action<,,>).MakeGenericType(parameterTypes),
                _ => null
            };

            return Delegate.CreateDelegate(delegateType, behaviour, method);
        }

        private CommandBase CreateCommandInstance(string commandName, string description, Delegate del,
            Type[] parameterTypes)
        {
            Type commandType = parameterTypes.Length switch
            {
                0 => typeof(Command),
                1 => typeof(Command<>).MakeGenericType(parameterTypes),
                2 => typeof(Command<,>).MakeGenericType(parameterTypes),
                3 => typeof(Command<,,>).MakeGenericType(parameterTypes),
                _ => null
            };

            return (CommandBase)Activator.CreateInstance(commandType, commandName, description, del);
        }
    }
}