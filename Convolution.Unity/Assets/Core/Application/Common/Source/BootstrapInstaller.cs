using AssetManagement.Source.Scenes;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VirtCons.Internal.Application.Common.Source.Windows;
using Zenject;

namespace VirtCons.Internal.Application.Common.Source
{
    public class BootstrapInstaller : MonoInstaller, IInitializableInstaller
    {
        [SerializeField]
        private SceneReference _launcherScene;

        public override UniTask InstallBindings()
        {
            #if UNITY_EDITOR

            Container.Bind<IApplicationService>().To<NullApplicationService>().AsSingle();
            Container.Bind<IWindowService>().To<NullWindowService>().AsSingle();
            
            #endif   
            
            return UniTask.CompletedTask;
        }

        public async UniTask Initialize()
        {
            await _launcherScene.LoadSceneAsync(LoadSceneMode.Additive).ToUniTask();
        }
    }
}
