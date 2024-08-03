using Debugging.Runtime.Core;
using UnityEngine;
using Zenject;

namespace Debugging.Runtime.Binding
{
    public sealed class CommandZenjectBinding : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ICommandFactory>().To<CommandFactory>().AsSingle();
            Container.Bind<CommandRepository>().FromComponentInHierarchy().AsSingle();
        }
    }
}