using System;
using System.Threading.Tasks;
using Convolution.DevKit.Common;
using Convolution.DevKit.Controllers;
using Convolution.DevKit.MiniGames;
using UnityEngine;

namespace Modder.Mod
{
    public sealed class ModGame : MiniGame<ModGameConfiguration, MiniGameSpriteMaskDisplay, ModGame.InputChannel>
    {
        #region Nested types

        public enum InputChannel : ushort
        {
            Move
        }

        #endregion

        private GameObject _player;

        public ModGame(ModGameConfiguration configuration, MiniGameSpriteMaskDisplay display, ObjectFactory factory) : base(configuration, display, factory) { }

        public override Task Bootup()
        {
            _player = _factory.Instantiate(_player, _display.transform);
            _player.transform.localPosition = Vector2.zero;
            
            _display.Bootup();
            return Task.CompletedTask;
        }

        protected override void IMP_HandleInput(IControllerInput input, InputChannel channel)
        {
            switch (channel)
            {
                case InputChannel.Move:
                {
                    var moveInput = (SimpleControllerInput<Vector2>)input;
                    
                    var newPlayerPosition = (Vector2)_player.transform.position + moveInput.Value * (_configuration.PlayerSpeed * Time.deltaTime);
                    newPlayerPosition = _display.Wrap(newPlayerPosition, 0.75f);

                    _player.transform.position = newPlayerPosition;
                    break;
                }
            }
        }

        public override MiniGameState Tick() => MiniGameState.Running;
    }
}
