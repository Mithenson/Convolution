using System;
using System.Collections.Generic;
using System.Linq;
using Convolution.DevKit.Common;
using Convolution.DevKit.Controllers;
using Convolution.DevKit.MiniGames;
using Convolution.Gameplay;
using Convolution.Interaction;
using Maxim.Common.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Convolution.Orchestration
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        [FoldoutGroup("MiniGame")]
        private MiniGameDisplaySceneRepository _miniGameDisplaySceneRepository;
        
        [SerializeField]
        [FoldoutGroup("MiniGame")]
        private MiniGameRenderer _miniGameRenderer;

        [SerializeField]
        [FoldoutGroup("Controllers")]
        private ControllerGrid _controllerGrid;
        
        [SerializeField]
        [FoldoutGroup("Controllers")]
        private BuiltInControllerPrefabRepository _builtInControllerPrefabRepository;

        private GameArgs _args;
        private DiContainer _miniGameContainer;
        
        public override void InstallBindings()
        {
            _args = Container.ParentContainers.First().Resolve<GameArgs>();
            
            PlacementInstaller.Install(Container);
            ControllersInstaller.Install(Container, _controllerGrid, _builtInControllerPrefabRepository);
            InteractionInstaller.Install(Container);
            MiniGameDisplayInstaller.Install(Container, _miniGameDisplaySceneRepository, _miniGameRenderer);
            GameplayInstaller.Install(Container);

            _miniGameContainer = Container.CreateSubContainer();
            
            _args.GameConfiguration.BindDependencies(_miniGameContainer);
            _miniGameContainer.Bind<MiniGameKernel>().ToSelf().AsSingle();
            _miniGameContainer.Bind(_args.GameConfiguration.GetType()).ToSelf().FromInstance(_args.GameConfiguration).AsSingle();
            _miniGameContainer.Bind<ObjectFactory>().ToSelf().AsSingle();
        }

        public async void Initialize()
        {
            var controllers = new List<Controller>();
            var controllerGrid = Container.Resolve<ControllerGrid>();

            var builtInControllerPrefabRepository = Container.Resolve<BuiltInControllerPrefabRepository>();
            foreach (var controllerPlacement in _args.GameConfiguration.ControllerPlacements)
            {
                var prefab = default(Controller);
                switch (controllerPlacement.ChosenSelectionMode)
                {
                    case ControllerPlacement.SelectionMode.BuiltIn:
                        prefab = builtInControllerPrefabRepository[controllerPlacement.PickedBuiltInType];
                        break;

                    case ControllerPlacement.SelectionMode.Custom:
                        prefab = controllerPlacement.PickedPrefab;
                        break;
                }
                
                var controller = Container.InstantiatePrefabForComponent<Controller>(prefab);
                
                controller.InputChannel = controllerPlacement.InputChannel;
                controller.gameObject.SetLayer(Constants.OwnedLayer);
                
                controllerGrid.Place(controllerPlacement.Position, controller);
                
                controllers.Add(controller);
            }
            
            Container.Resolve<ControllerRepository>().Bootup(controllers);

            var game = (MiniGame)_miniGameContainer.Instantiate(_args.GameConfiguration.GameType);
            _miniGameContainer.BindInterfacesAndSelfTo(game.GetType()).FromInstance(game).AsSingle();
            
            await game.Bootup();
            
            Container.Resolve<GameplayLoop>().Bootup(game, _miniGameContainer.Resolve<MiniGameKernel>());
        }
    }
}
