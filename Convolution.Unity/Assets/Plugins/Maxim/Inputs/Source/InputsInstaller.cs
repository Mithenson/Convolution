using UnityEngine.InputSystem;
using Zenject;

using Object = UnityEngine.Object;

namespace Maxim.Inputs
{
    public class InputsInstaller : Installer<InputActionAsset, InputsInstaller>
    {
        private readonly InputActionAsset _asset;

        public InputsInstaller(InputActionAsset asset) => _asset = asset;

        public override void InstallBindings()
        {
            var instance = Object.Instantiate(_asset);
            Container.Bind<InputsService>().ToSelf().AsSingle().WithArguments(instance);
        }
    }
}
