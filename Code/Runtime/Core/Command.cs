using System;
using UnityEngine;

namespace Debugging.Runtime.Core
{
    public sealed class Command : CommandBase
    {
        private Action _action;


        public Command(string commandName, string description, Action action) :
            base(commandName, description)
        {
            this._action = action;
        }

        public override void Execute(params object[] args)
        {
            if (!ValidateArgs(args, 0))
                return;

            _action?.Invoke();
        }
    }

    public class Command<T> : CommandBase
    {
        private Action<T> _action;

        public Command(string commandName, string description, Action<T> action) : base(commandName, description)
        {
            this._action = action;
        }

        public override void Execute(params object[] args)
        {
            if (!ValidateArgs(args, 1, new[] { typeof(T) }))
                return;

            _action?.Invoke((T)args[0]);
        }
    }

    public class Command<T1, T2> : CommandBase
    {
        private Action<T1, T2> _action;

        public Command(string commandName, string description, Action<T1, T2> action) : base(commandName, description)
        {
            this._action = action;
        }

        public override void Execute(params object[] args)
        {
            if (!ValidateArgs(args, 2, new[] { typeof(T1), typeof(T2) }))
                return;

            _action((T1)args[0], (T2)args[1]);
        }
    }

    public class Command<T1, T2, T3> : CommandBase
    {
        private Action<T1, T2, T3> _action;

        public Command(string commandName, string description, Action<T1, T2, T3> action) : base(commandName,
            description)
        {
            this._action = action;
        }

        public override void Execute(params object[] args)
        {
            if (!ValidateArgs(args, 3, new[] { typeof(T1), typeof(T2), typeof(T3) }))
                return;

            _action((T1)args[0], (T2)args[1], (T3)args[2]);
        }
    }
}