using System.Linq;
using System.Threading.Tasks;
using Convolution.Controllers;
using Convolution.Gameplay;
using Convolution.MiniGames.Source;
using Maxim.AssetManagement.Configurations;
using UnityEngine;
using Zenject;

namespace Convolution.Orchestration
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private MiniGameDisplayRepository _displayRepository;

        private GameArgs _args;
        
        public override void InstallBindings()
        {
            _args = Container.ParentContainers.First().Resolve<GameArgs>();

            Container.Bind(_args.GameConfiguration.GetType()).ToSelf().FromInstance(_args.GameConfiguration).AsSingle();
            
            GameplayInputsInstaller.Install(Container);
            InteractionInstaller.Install(Container);
            ControllersInstaller.Install(Container);

            var displays = _displayRepository.Displays;
            foreach (var display in displays)
            {
                var displayType = display.GetType();
                Container.Bind(displayType).To(displayType).FromInstance(display).AsSingle();
            }

            Container.Bind(typeof(GameplayLoop), typeof(ITickable)).To<GameplayLoop>().AsSingle();
        }

        public async void Initialize()
        {
            var controllers = FindObjectsOfType<Controller>();
            Container.Resolve<ControllerRepository>().Bootup(controllers);

            var game = (MiniGame)Container.Instantiate(_args.GameConfiguration.GameType);

            await game.Bootup();
            game.Display.Show();
            
            Container.Resolve<GameplayLoop>().Bootup(game);
        }

        private void OnDestroy() => _args.Reset();
    }
}
