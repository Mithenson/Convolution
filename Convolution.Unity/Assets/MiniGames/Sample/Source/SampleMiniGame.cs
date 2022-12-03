using System.Collections.Generic;
using System.Threading.Tasks;
using Convolution.DevKit.Common;
using Convolution.DevKit.Controllers;
using Convolution.DevKit.MiniGames;
using Maxim.Common.Extensions;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Convolution.MiniGames.Sample
{
    public sealed class SampleMiniGame : MiniGame<SampleMiniGameConfiguration, MiniGameTopDownCameraDisplay, SampleMiniGame.InputChannel>
    {
        #region Nested types

        public enum InputChannel : ushort
        {
            Movement = 1,
            Direction = 2,
            Temp = 3
        }

        #endregion

        private readonly PlayerModel _playerModel;
        private readonly TimerModel _timerModel;

        private Player _player;
        private List<Bullet> _bullets;
        private float _bulletSpawnCountdown;

        public SampleMiniGame(
            SampleMiniGameConfiguration configuration, 
            MiniGameTopDownCameraDisplay display,
            ObjectFactory factory,
            PlayerModel playerModel,
            TimerModel timerModel) 
            : base(configuration, display, factory)
        {
            _playerModel = playerModel;
            _timerModel = timerModel;

            _bullets = new List<Bullet>();
        }

        public SampleMiniGameConfiguration Configuration => _configuration;
        public MiniGameTopDownCameraDisplay CameraDisplay => _display;

        public override Task Bootup()
        {
            _factory.Instantiate(_configuration.UIPrefab, _display.Canvas.transform);

            _player = _factory.Instantiate(_configuration.PlayerPrefab, _display.transform);
            _player.transform.localPosition = Vector2.zero;

            _bulletSpawnCountdown = _configuration.BulletSpawnInterval;

            _timerModel.GameStartTimestamp = Time.time;
            return Task.CompletedTask;
        }

        protected override void IMP_HandleInput(IControllerInput input, InputChannel channel)
        {
            if (!(input is SimpleControllerInput<Vector2> vector2Input))
                return;
            
            switch (channel)
            {
                case InputChannel.Movement:
                    _player.Move(vector2Input.Value);
                    break;

                case InputChannel.Direction:
                    _player.Rotate(vector2Input.Value);
                    break;
            }
        }

        public override MiniGameState Tick()
        {
            if (_timerModel.ElapsedTime >= _configuration.NeededSurvivalDuration)
                return MiniGameState.Won;
            
            _bulletSpawnCountdown -= Time.deltaTime;
            if (_bulletSpawnCountdown <= 0.0f)
            {
                var halfInflatedBounds = _display.Bounds.Inflate(_configuration.WrapPadding * 0.5f);
                var randomPoint = new Vector2(Random.Range(_display.Bounds.xMin, _display.Bounds.xMax), Random.Range(_display.Bounds.yMin, _display.Bounds.yMax));

                var diffs = new Vector2[]
                {
                    new Vector2(halfInflatedBounds.xMax - randomPoint.x, 0.0f),
                    new Vector2(halfInflatedBounds.xMin - randomPoint.x, 0.0f),
                    new Vector2(0.0f, halfInflatedBounds.yMax - randomPoint.y),
                    new Vector2(0.0f, halfInflatedBounds.yMin - randomPoint.y)
                };
                
                var minSquaredDiff = float.MaxValue;
                var minDiff = default(Vector2);

                for (var i = 0; i < diffs.Length; i++)
                {
                    var diff = diffs[i];
                    var squaredDiff = diff.sqrMagnitude;
                    
                    if (squaredDiff > minSquaredDiff)
                        continue;

                    minSquaredDiff = squaredDiff;
                    minDiff = diff;
                }

                randomPoint += minDiff;

                var bullet = _factory.Instantiate(_configuration.BulletPrefab, _display.transform);
                bullet.transform.position = randomPoint;
                bullet.Direction = ((Vector2)_player.transform.position - randomPoint).normalized;
                
                _bullets.Add(bullet);
                
                _bulletSpawnCountdown = _configuration.BulletSpawnInterval + _bulletSpawnCountdown;
            }

            for (var i = _bullets.Count - 1; i > -1; i--)
            {
                _bullets[i].Tick(_player, out var isDestroyed);
                
                if (isDestroyed)
                    _bullets.RemoveAt(i);
            }
            
            if (_playerModel.Health <= 0)
                return MiniGameState.Failed;

            return MiniGameState.Running;
        }
    }
}
