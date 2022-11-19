using System;
using Convolution.Interaction;
using Convolution.MiniGames.Source;
using UnityEngine;
using Zenject;

namespace Convolution.Gameplay
{
    public class GameplayLoop : IInitializable, ITickable, IFixedTickable, ILateTickable, IDisposable, ILateDisposable
    {
        public bool IsEnabled;

        private readonly GameplayModel _model;
        private readonly InteractionService _interactionService;
        private readonly ControllerInputBridgeService _controllerInputBridgeService;

        private MiniGame _miniGame;
        private MiniGameKernel _miniGameKernel;
        
        public GameplayLoop(GameplayModel model, InteractionService interactionService, ControllerInputBridgeService controllerInputBridgeService)
        {
            _model = model;
            _interactionService = interactionService;
            _controllerInputBridgeService = controllerInputBridgeService;
        }

        public void Bootup(MiniGame miniGame, MiniGameKernel miniGameKernel)
        {
            _miniGame = miniGame;
            _miniGameKernel = miniGameKernel;
            
            IsEnabled = true;
        }

        void IInitializable.Initialize() => _miniGameKernel.Initialize();
        
        void ITickable.Tick()
        {
            if (!IsEnabled || _model.State == GameplayState.Done)
                return;
            
            _interactionService.Tick();
            _controllerInputBridgeService.Tick(_miniGame);

            var miniGameState = _miniGame.Tick();
            _miniGameKernel.Tick();
            
            if (miniGameState == MiniGameState.Won || miniGameState == MiniGameState.Failed)
            {
                Debug.Log($"Game over: {miniGameState}");
                _model.State = GameplayState.Done;
            }
        }
        
        void IFixedTickable.FixedTick() => _miniGameKernel.FixedTick();
        
        void ILateTickable.LateTick() => _miniGameKernel.LateTick();
        
        void IDisposable.Dispose() => _miniGameKernel.Dispose();
        
        void ILateDisposable.LateDispose() => _miniGameKernel.LateDispose();
    }
}
