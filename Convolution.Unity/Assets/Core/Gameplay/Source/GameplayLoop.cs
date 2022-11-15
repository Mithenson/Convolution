using Convolution.Interaction;
using Convolution.MiniGames.Source;
using Zenject;

namespace Convolution.Gameplay
{
    public class GameplayLoop : ITickable
    {
        public bool IsEnabled;
        
        private readonly InteractionService _interactionService;
        private readonly ControllerInputBridgeService _controllerInputBridgeService;

        private MiniGame _miniGame;
        
        public GameplayLoop(InteractionService interactionService, ControllerInputBridgeService controllerInputBridgeService)
        {
            _interactionService = interactionService;
            _controllerInputBridgeService = controllerInputBridgeService;
        }

        public void Bootup(MiniGame miniGame)
        {
            _miniGame = miniGame;
            
            IsEnabled = true;
        }
        
        void ITickable.Tick()
        {
            if (!IsEnabled)
                return;
            
            _interactionService.Tick();
            _controllerInputBridgeService.Tick(_miniGame);
            _miniGame.Tick();
        }
    }
}
