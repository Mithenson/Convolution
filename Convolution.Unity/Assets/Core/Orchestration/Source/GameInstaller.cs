using System.Collections.Generic;
using System.Linq;
using Convolution.Controllers;
using Convolution.Gameplay;
using Convolution.MiniGames.Source;
using UnityEngine;
using Zenject;

namespace Convolution.Orchestration
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private MiniGameDisplayRepository _displayRepository;

        [SerializeField]
        private ControllerGrid _controllerGrid;

        private GameArgs _args;
        
        public override void InstallBindings()
        {
            _args = Container.ParentContainers.First().Resolve<GameArgs>();

            Container.Bind(_args.GameConfiguration.GetType()).ToSelf().FromInstance(_args.GameConfiguration).AsSingle();
            
            PlacementInstaller.Install(Container);
            GameplayInputsInstaller.Install(Container);
            InteractionInstaller.Install(Container);
            ControllersInstaller.Install(Container);

            var displays = _displayRepository.Displays;
            foreach (var display in displays)
            {
                var displayType = display.GetType();
                Container.Bind(displayType).To(displayType).FromInstance(display).AsSingle();
            }

            Container.Bind<ControllerGrid>().ToSelf().FromInstance(_controllerGrid).AsSingle();

            Container.Bind(typeof(GameplayLoop), typeof(ITickable)).To<GameplayLoop>().AsSingle();
        }

        public async void Initialize()
        {
            var controllers = new List<Controller>();
            var controllerGrid = Container.Resolve<ControllerGrid>();
            
            foreach (var controllerPlacement in _args.GameConfiguration.ControllerPlacements)
            {
                var controller = Container.InstantiatePrefabForComponent<Controller>(controllerPlacement.Prefab);
                
                controller.InputChannel = controllerPlacement.InputChannel;
                controllerGrid.Place(controllerPlacement.Position, controller);
                
                controllers.Add(controller);
            }
            
            Container.Resolve<ControllerRepository>().Bootup(controllers);

            var game = (MiniGame)Container.Instantiate(_args.GameConfiguration.GameType);

            await game.Bootup();
            game.Display.Show();
            
            Container.Resolve<GameplayLoop>().Bootup(game);
        }

        private void OnDestroy() => _args.Reset();
    }
}
