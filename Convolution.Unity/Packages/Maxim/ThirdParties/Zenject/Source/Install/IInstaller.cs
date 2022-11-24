using Cysharp.Threading.Tasks;

namespace Zenject
{
    // We extract the interface so that monobehaviours can be installers
    public interface IInstaller
    {
        UniTask InstallBindings();

        bool IsEnabled
        {
            get;
        }
    }

}
