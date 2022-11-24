using System;
using System.Threading.Tasks;
using Convolution.DevKit.Common;
using Convolution.DevKit.Controllers;

namespace Convolution.DevKit.MiniGames
{
    public abstract class MiniGame
    {
        public abstract IMiniGameDisplay Display { get; }
        
        public virtual Task Bootup() => Task.CompletedTask;
        
        public abstract void HandleInput(IControllerInput input, ushort channel);
        public abstract MiniGameState Tick();
    }
    
    public abstract class MiniGame<TConfiguration, TDisplay, TInputChannel> : MiniGame 
        where TConfiguration : IMiniGameConfiguration
        where TDisplay : IMiniGameDisplay
        where TInputChannel : Enum
    {
        protected readonly TConfiguration _configuration;
        protected readonly TDisplay _display;
        protected readonly ObjectFactory _factory;

        protected MiniGame(TConfiguration configuration, TDisplay display, ObjectFactory factory)
        {
            _configuration = configuration;
            _display = display;
            _factory = factory;
        }

        public override IMiniGameDisplay Display => _display;

        public override void HandleInput(IControllerInput input, ushort channel)
        {
            var castedChannel = (TInputChannel)Enum.ToObject(typeof(TInputChannel), channel);
            IMP_HandleInput(input, castedChannel);
        }
        protected abstract void IMP_HandleInput(IControllerInput input, TInputChannel channel);
    }
}