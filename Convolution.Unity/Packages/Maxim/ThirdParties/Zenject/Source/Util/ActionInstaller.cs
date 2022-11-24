using System;
using Cysharp.Threading.Tasks;

namespace Zenject
{
    public class ActionInstaller : Installer<ActionInstaller>
    {
        readonly Install _installMethod;

        public ActionInstaller(Install installMethod)
        {
            _installMethod = installMethod;
        }

        public override async UniTask InstallBindings()
        {
            await _installMethod(Container);
        }
    }
}
