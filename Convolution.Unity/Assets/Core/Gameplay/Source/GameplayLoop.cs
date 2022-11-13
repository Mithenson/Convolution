using Convolution.Interaction;
using Zenject;

namespace Convolution.Gameplay
{
    public class GameplayLoop : IInitializable, ITickable
    {
        public bool IsEnabled;
        
        private readonly InteractionService _interactionService;
        
        public GameplayLoop(InteractionService interactionService) => _interactionService = interactionService;

        public void Initialize() => IsEnabled = true;
        
        void ITickable.Tick()
        {
            if (!IsEnabled)
                return;
            
            _interactionService.Tick();
        }
    }
}
