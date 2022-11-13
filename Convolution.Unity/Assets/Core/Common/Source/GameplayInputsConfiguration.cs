using Maxim.AssetManagement.Configurations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Convolution
{
    [CreateAssetMenu(menuName = "Convolution/Configurations/Gameplay inputs", fileName = nameof(GameplayInputsConfiguration))]
    public sealed class GameplayInputsConfiguration : Configuration
    {
        [SerializeField]
        private InputActionReference _cursorPositionInputReference;
        
        [SerializeField]
        private InputActionReference _cursorClickInputReference;

        public InputActionReference CursorPositionInputReference => _cursorPositionInputReference;
        public InputActionReference CursorClickInputReference => _cursorClickInputReference;
    }
}
