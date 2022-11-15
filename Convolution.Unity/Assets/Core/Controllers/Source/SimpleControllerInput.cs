namespace Convolution.Controllers
{
	public readonly struct SimpleControllerInput<TValue> : IControllerInput
		where TValue : struct
	{
		public readonly TValue Value;
		
		public SimpleControllerInput(TValue value) => Value = value;
	}
}