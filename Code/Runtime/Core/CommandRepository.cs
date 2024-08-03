using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Debugging.Runtime.UI;
using UnityEngine;
using Zenject;

namespace Debugging.Runtime.Core
{
    public interface ICommandRepository
    {
        public CommandBase GetCommand(string commandName);
    }

    [RequireComponent(typeof(ConsoleVisualManager))]
    public sealed class CommandRepository : MonoBehaviour
    {
        private readonly Dictionary<string, CommandBase> _cache = new();
        private ICommandFactory _factory;

        private List<CommandBase> _commandList;

        public List<CommandBase> CommandBases => _commandList;

        [Inject]
        private void Constructor(ICommandFactory factory)
        {
            this._factory = factory;
        }

        private void Awake()
        {
            Type baseType = typeof(CommandMonoBehaviour);
            Assembly assembly = baseType.Assembly;

            Type[] allTypes = assembly.GetTypes();
            Type[] derivedTypes = allTypes.Where(r => r.IsSubclassOf(baseType) && !r.IsAbstract).ToArray();

            foreach (Type derivedType in derivedTypes)
            {
                CommandMonoBehaviour instance = (CommandMonoBehaviour)FindObjectOfType(derivedType);

                MethodInfo[] methods =
                    derivedType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                foreach (MethodInfo method in methods)
                {
                    CommandAttribute attribute = method.GetCustomAttribute<CommandAttribute>();
                    if (attribute == null) continue;

                    ParameterInfo[] parameters = method.GetParameters();
                    Type[] parameterTypes = parameters.Select(r => r.ParameterType).ToArray();
                    CommandBase commandBase = _factory.CreateCommand(instance, method, attribute, parameterTypes);

                    if (commandBase != null)
                        _cache[attribute.CommandExecuteName] = commandBase;
                }
            }
        }

        private void Start()
        {
            _commandList = _cache.Values.ToList();
        }

        public CommandBase GetCommand(string commandName)
        {
            return _cache[commandName];
        }

        public bool TryGetCommand(string commandName, out CommandBase command)
        {
            if (_cache.TryGetValue(commandName, out command))
                return true;

            return false;
        }
    }
}