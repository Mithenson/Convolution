using System.Threading.Tasks;
using Convolution.Controllers;
using Convolution.MiniGames.Source;
using UnityEngine;

using Object = UnityEngine.Object;

namespace Sample
{
    public sealed class SampleMiniGame : MiniGame<SampleMiniGameConfiguration, SpriteMaskMiniGameDisplay, SampleMiniGame.InputChannel>
    {
        #region Nested types

        public enum InputChannel : ushort
        {
            Movement = 1,
            Direction = 2
        }

        #endregion

        private GameObject _player;
    
        public SampleMiniGame(SampleMiniGameConfiguration configuration, SpriteMaskMiniGameDisplay display) : base(configuration, display) { }

        public override Task Bootup()
        {
            _player = Object.Instantiate(_configuration.PlayerPrefab, _display.transform);
            _player.transform.localPosition = Vector2.zero;
            
            return Task.CompletedTask;
        }

        protected override void IMP_HandleInput(IControllerInput input, InputChannel channel)
        {
            var vector2Input = (SimpleControllerInput<Vector2>)input;
            switch (channel)
            {
                case InputChannel.Movement:
                    _player.transform.position += (Vector3)(vector2Input.Value * (Time.deltaTime * _configuration.PlayerSpeed));
                    break;

                case InputChannel.Direction:
                    _player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(-vector2Input.Value.x, vector2Input.Value.y) * Mathf.Rad2Deg);
                    break;
            }
        }

        public override void Tick() { }
    }
}
