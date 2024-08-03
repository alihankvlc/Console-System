using System;
using UnityEngine;

namespace Debugging.Runtime.Core
{
    public interface ICommand
    {
        string CommandName { get; }
        string Description { get; }
    }

    public abstract class CommandBase : ICommand
    {
        public string CommandName { get; }
        public string Description { get; }

        public abstract void Execute(params object[] args);

        protected CommandBase(string commandName, string description)
        {
            this.CommandName = commandName;
            this.Description = description;
        }

        protected bool ValidateArgs(object[] args, int expectedLength)
        {
            if (args.Length != expectedLength)
            {
                Debug.LogWarning($"Command '{CommandName}' expects {expectedLength} arguments, but got {args.Length}.");
                return false;
            }

            return true;
        }

        protected bool ValidateArgs(object[] args, int expectedLength, Type[] expectedTypes)
        {
            if (!ValidateArgs(args, expectedLength))
                return false;

            for (int i = 0; i < expectedLength; i++)
                if (args[i] == null || args[i].GetType() != expectedTypes[i])
                {
                    Debug.LogWarning(
                        $"Command '{CommandName}' expects argument {i} of type {expectedTypes[i].Name}, but got {args[i]?.GetType().Name ?? "null"}.");
                    return false;
                }

            return true;
        }
    }
}