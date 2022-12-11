namespace VirtCons.Internal.Application.Common.Source.Windows
{
	public sealed class NullWindowService : IWindowService
	{
		private readonly NullWindow _mainWindow;

		public NullWindowService() => _mainWindow = new NullWindow();

		public IWindow MainWindow => _mainWindow;
	}
}