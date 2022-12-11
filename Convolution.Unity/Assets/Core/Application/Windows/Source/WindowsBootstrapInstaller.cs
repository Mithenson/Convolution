using Cysharp.Threading.Tasks;
using VirtCons.Internal.Application.Common.Source;
using VirtCons.Internal.Application.Common.Source.Windows;
using Zenject;

namespace VirtCons.Internal.Core.Application.Windows.Source
{
	public sealed class WindowsBootstrapInstaller : MonoInstaller
	{
		public override UniTask InstallBindings()
		{
			#if !UNITY_EDITOR
			
			Container.Bind<IWindowService>().To<WindowsWindowService>().AsSingle().NonLazy();
			Container.Bind<IApplicationService>().To<WindowsApplicationService>().AsSingle().NonLazy();
			
			#endif
			
			return UniTask.CompletedTask;
		}
	}
}