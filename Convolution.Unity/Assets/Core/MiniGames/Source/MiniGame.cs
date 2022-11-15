using System;
using System.Threading.Tasks;
using Convolution.Controllers;

namespace Convolution.MiniGames.Source
{
    public abstract class MiniGame
    {
        public abstract IMiniGameDisplay Display { get; }

        public virtual Task Bootup() => Task.CompletedTask;
        
        public abstract void HandleInput(IControllerInput input, ushort channel);
        public abstract void Tick();
    }
    
    public abstract class MiniGame<TConfiguration, TDisplay, TInputChannel> : MiniGame 
        where TConfiguration : MiniGameConfiguration
        where TDisplay : IMiniGameDisplay
        where TInputChannel : Enum
    {
        protected readonly TConfiguration _configuration;
        protected readonly TDisplay _display;

        protected MiniGame(TConfiguration configuration, TDisplay display)
        {
            _configuration = configuration;
            _display = display;
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
