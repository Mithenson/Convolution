using Convolution.Controllers;
using Convolution.Gameplay;
using Maxim.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Convolution
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private InputActionAsset _inputs;
        
        public override void InstallBindings()
        {
            #region To move up

            InputsInstaller.Install(Container, _inputs);

            #endregion
            
            GameplayInputsInstaller.Install(Container);
            InteractionInstaller.Install(Container);
            ControllersInstaller.Install(Container);

            Container.Bind(typeof(GameplayLoop), typeof(ITickable)).To<GameplayLoop>().AsSingle();
        }

        public void Initialize()
        {
            var controllers = FindObjectsOfType<Controller>();
            Container.Resolve<ControllerRepository>().Initialize(controllers);
            
            Container.Resolve<InputsService>().Enable();

            Container.Resolve<GameplayLoop>().Initialize();
        }
    }
}
