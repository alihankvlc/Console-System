using System;
using System.Collections.Generic;
using System.Globalization;
using Debugging.Runtime.UI;
using UnityEngine;

namespace Debugging.Runtime.Core
{
    public sealed class CommandHandler : CommandMonoBehaviour
    {
        private void OnEnable()
        {
            ConsoleVisualManager.OnCommandSubmitted += OnCommandSubmitted;
        }

        private void OnCommandSubmitted(string input)
        {
            string trimmedInput = input.TrimEnd();
            string[] arguments = trimmedInput.Split(' ');
            string commandName = arguments[0];

            if (commandName.Length > 1)
            {
                if (input.StartsWith("/") && !HasCommand(commandName))
                {
                    Debug.LogWarning($"Command '{commandName}' not found or not authorized for execution.");
                    return;
                }
            }

            object[] args = ParseArguments(arguments);

            CommandBase command = GetCommand(commandName);
            command?.Execute(args);
        }

        private bool IsValidCommand(string commandName)
        {
            return !string.IsNullOrEmpty(commandName) && HasCommand(commandName);
        }

        private object[] ParseArguments(string[] arguments)
        {
            if (arguments.Length <= 1)
            {
                return Array.Empty<object>();
            }

            var result = new List<object>(arguments.Length - 1);

            for (int i = 1; i < arguments.Length; i++)
            {
                string part = arguments[i];

                object parsedValue = part switch
                {
                    _ when int.TryParse(part, out int intValue) => intValue,
                    _ when float.TryParse(part, NumberStyles.Float, CultureInfo.InvariantCulture,
                        out float floatValue) => floatValue,
                    _ => part
                };

                result.Add(parsedValue);
            }

            return result.ToArray();
        }

        private void OnDisable()
        {
            ConsoleVisualManager.OnCommandSubmitted -= OnCommandSubmitted;
        }
    }
}