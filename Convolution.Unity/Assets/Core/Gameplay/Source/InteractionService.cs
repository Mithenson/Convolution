using System;
using Convolution.DevKit.Controllers;
using Convolution.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

using Cursor = Convolution.DevKit.Interaction.Cursor;

namespace Convolution.Interaction
{
    public sealed class InteractionService : IDisposable
    {
        private readonly Cursor _cursor;
        private readonly ControllerRepository _controllerRepository;
        private readonly GameplayInputsRepository _gameplayInputsRepository;

        private Controller _closetHoveredController;
        private bool _isInteracting;

        public InteractionService(
            Cursor cursor, 
            ControllerRepository controllerRepository, 
            GameplayInputsRepository gameplayInputsRepository)
        {
            _cursor = cursor;
            _controllerRepository = controllerRepository;
            _gameplayInputsRepository = gameplayInputsRepository;
            
            _gameplayInputsRepository.CursorClickAction.started += OnCursorDown;
            _gameplayInputsRepository.CursorClickAction.canceled += OnCursorUp;
        }

        public void Tick()
        {
            var cursorPosition = _gameplayInputsRepository.CursorPositionAction.ReadValue<Vector2>();
            _cursor.Position = Camera.main.ScreenToWorldPoint(cursorPosition);

            if (_isInteracting)
            {
                _closetHoveredController.Interact(_cursor);
                if (_closetHoveredController.State != ControllerState.BeingInteractedWith)
                    _isInteracting = false;
                
                return;
            }

            var closestSeparation = float.MaxValue;
            _closetHoveredController = default;
            
            foreach (var controller in _controllerRepository.Controllers)
            {
                if (controller.State != ControllerState.AtRest
                    || !controller.Handle.IsHovered(_cursor, out var separation) 
                    || separation >= closestSeparation)
                    continue;

                _closetHoveredController = controller;
            }
        }

        private void OnCursorDown(InputAction.CallbackContext ctxt)
        {
            if (_closetHoveredController == null)
                return;

            _isInteracting = _closetHoveredController.TryStartInteraction(_cursor);
        }
        private void OnCursorUp(InputAction.CallbackContext ctxt)
        {
            if (!_isInteracting)
                return;
            
            _closetHoveredController.EndInteraction(_cursor);
            _isInteracting = false;
        }
        
        void IDisposable.Dispose()
        {
            _gameplayInputsRepository.CursorClickAction.started -= OnCursorDown;
            _gameplayInputsRepository.CursorClickAction.canceled -= OnCursorUp;
        }
    }
}
