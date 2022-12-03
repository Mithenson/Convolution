using System;
using System.Collections.Generic;
using System.Linq;
using Convolution.DevKit.Common;
using Convolution.DevKit.Controllers;
using Convolution.DevKit.MiniGames;
using Convolution.Gameplay;
using Convolution.Interaction;
using Cysharp.Threading.Tasks;
using Maxim.Common.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Convolution.Orchestration
{
    public class GameInstaller : MonoInstaller, IInitializableInstaller
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

        private GameContext _context;
        private DiContainer _miniGameContainer;
        
        public override async UniTask InstallBindings()
        {
            _context = Container.ParentContainers.First().Resolve<GameContext>();
            
            await PlacementInstaller.Install(Container);
            await ControllersInstaller.Install(Container, _controllerGrid, _builtInControllerPrefabRepository);
            await InteractionInstaller.Install(Container);
            await MiniGameDisplayInstaller.Install(Container, _miniGameDisplaySceneRepository, _miniGameRenderer);
            await GameplayInstaller.Install(Container);

            _miniGameContainer = Container.CreateSubContainer();
            
            _context.Configuration.BindDependencies(_miniGameContainer);
            _miniGameContainer.Bind<MiniGameKernel>().ToSelf().AsSingle();
            _miniGameContainer.Bind(_context.Configuration.GetType()).ToSelf().FromInstance(_context.Configuration).AsSingle();
            _miniGameContainer.Bind<ObjectFactory>().ToSelf().AsSingle();
        }

        public async UniTask Initialize()
        {
            var controllers = new List<Controller>();
            var controllerGrid = Container.Resolve<ControllerGrid>();

            var builtInControllerPrefabRepository = Container.Resolve<BuiltInControllerPrefabRepository>();
            foreach (var controllerPlacement in _context.Configuration.ControllerPlacements)
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

            var game = (MiniGame)_miniGameContainer.Instantiate(_context.Configuration.GameType);
            _miniGameContainer.BindInterfacesAndSelfTo(game.GetType()).FromInstance(game).AsSingle();
            
            await game.Bootup();
            game.Display.Show();
            
            Container.Resolve<GameplayLoop>().Bootup(game, _miniGameContainer.Resolve<MiniGameKernel>());
        }
    }
}
