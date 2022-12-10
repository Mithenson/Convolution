using ModestTree;
using UnityEngine;

namespace Convolution.DevKit.MiniGames
{
	public sealed class MiniGameRenderer : MonoBehaviour
	{
		private static readonly int MiniGameRenderTextureProperty = Shader.PropertyToID("_MainTex");
		
		[SerializeField]
		private MeshRenderer _underlyingImplementation;

		[SerializeField]
		private Material _materialReference;

		private int _runtimeMaterialIndex;
		
		private void Awake() => _runtimeMaterialIndex = _underlyingImplementation.sharedMaterials.IndexOf(_materialReference);

		public void Set(RenderTexture texture) => IMP_SetTexture(texture);
		public void Set(Texture texture) => IMP_SetTexture(texture);

		private void IMP_SetTexture(Texture texture) => _underlyingImplementation.materials[_runtimeMaterialIndex].SetTexture(MiniGameRenderTextureProperty, texture);
	}
}