using System;
using System.Collections.Generic;
using System.Globalization;
using Debugging.Runtime.UI;
using UnityEngine;
using Zenject;

namespace Debugging.Runtime.Core
{
    public abstract class CommandMonoBehaviour : MonoBehaviour
    {
        private CommandRepository _repository;

        [Inject]
        private void Consturctor(CommandRepository repository)
        {
            this._repository = repository;
        }

        protected void ExecuteCommand(string commandName)
        {
            bool success = _repository.TryGetCommand(commandName, out CommandBase command);

            if (success)
                command.Execute();
        }

        protected void ExecuteCommand(string commandName, params object[] args)
        {
            bool success = _repository.TryGetCommand(commandName, out CommandBase command);

            if (success)
                command?.Execute(args);
        }

        protected bool HasCommand(string commandName)
        {
            return _repository.TryGetCommand(commandName, out _);
        }

        protected CommandBase GetCommand(string commandName)
        {
            _repository.TryGetCommand(commandName, out CommandBase command);
            return command;
        }

        public List<CommandBase> GetCommands()
        {
            return _repository.CommandBases;
        }
    }
}