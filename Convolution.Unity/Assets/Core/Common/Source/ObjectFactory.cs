using UnityEngine;
using Zenject;

namespace Convolution
{
	public sealed class ObjectFactory
	{
		private DiContainer _container;
		
		public ObjectFactory(DiContainer container) => _container = container;
		
		public GameObject Instantiate(GameObject prefab) => _container.InstantiatePrefab(prefab);
		public GameObject Instantiate(GameObject prefab, Transform transform) => _container.InstantiatePrefab(prefab, transform);

		public TComponent Instantiate<TComponent>(TComponent prefab)
			where TComponent : Component
		{
			return _container.InstantiatePrefabForComponent<TComponent>(prefab);
		}
		public TComponent Instantiate<TComponent>(TComponent prefab, Transform transform)
			where TComponent : Component
		{
			return _container.InstantiatePrefabForComponent<TComponent>(prefab, transform);
		}
	}
}