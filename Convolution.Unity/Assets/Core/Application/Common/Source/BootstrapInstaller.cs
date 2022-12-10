using AssetManagement.Source.Scenes;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using VirtCons.Internal.Common.Source;
using Zenject;

namespace VirtCons.Internal.Application.Common.Source
{
    public class BootstrapInstaller : MonoInstaller, IInitializableInstaller
    {
        [SerializeField]
        private SceneReference _launcherScene;

        [SerializeField]
        private EventSystem _eventSystem;
        
        public override UniTask InstallBindings()
        {
            Container.Bind<EventSystem>().ToSelf().FromInstance(_eventSystem).AsSingle();
            Container.BindInterfacesAndSelfTo<TransparentBackgroundService>().AsSingle().NonLazy();
            
            return UniTask.CompletedTask;
        }

        public async UniTask Initialize()
        {
            await _launcherScene.LoadSceneAsync(LoadSceneMode.Additive).ToUniTask();
        }
    }
}
