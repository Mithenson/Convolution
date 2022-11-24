using Cysharp.Threading.Tasks;
using Zenject;

namespace Convolution.Orchestration
{
	public class MenuInstaller : MonoInstaller
	{
		public override UniTask InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<MiniGameChoiceViewModel>().AsSingle();
			return UniTask.CompletedTask;
		}
	}
}