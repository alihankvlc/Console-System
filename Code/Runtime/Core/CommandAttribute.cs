using System;
using UnityEngine;

namespace Debugging.Runtime.Core
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class CommandAttribute : Attribute
    {
        public string CommandExecuteName { get; private set; }
        public string CommandDescription { get; private set; }

        public CommandAttribute(string commandExecuteName, string commandDescription = "null")
        {
            this.CommandExecuteName = commandExecuteName;
            this.CommandDescription = commandDescription;
        }
    }
}