using Cysharp.Threading.Tasks;
using ModestTree;
using UnityEngine;

#if !NOT_UNITY3D

namespace Zenject
{
    public abstract class RunnableContext : Context
    {
        [Tooltip("When false, wait until run method is explicitly called. Otherwise run on initialize")]
        [SerializeField]
        bool _autoRun = true;

        static bool _staticAutoRun = true;

        public bool Initialized { get; private set; }

        protected async UniTask Initialize()
        {
            if (_staticAutoRun && _autoRun)
            {
                await Run();
            }
            else
            {
                // True should always be default
                _staticAutoRun = true;
            }
        }

        public async UniTask Run()
        {
            Assert.That(!Initialized,
                "The context already has been initialized!");

            await RunInternal();

            Initialized = true;
        }

        protected abstract UniTask RunInternal();

        public static T CreateComponent<T>(GameObject gameObject) where T : RunnableContext
        {
            _staticAutoRun = false;

            var result = gameObject.AddComponent<T>();
            Assert.That(_staticAutoRun); // Should be reset
            return result;
        }
    }
}

#endif
