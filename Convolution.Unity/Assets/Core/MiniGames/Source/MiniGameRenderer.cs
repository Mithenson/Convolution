using UnityEngine;

namespace Convolution.MiniGames.Source
{
	public sealed class MiniGameRenderer : MonoBehaviour
	{
		private static readonly int MiniGameRenderTextureProperty = Shader.PropertyToID("_MainTex");
		
		[SerializeField]
		private MeshRenderer _underlyingImplementation;

		public void Set(RenderTexture texture) => IMP_SetTexture(texture);
		public void Set(Texture texture) => IMP_SetTexture(texture);

		private void IMP_SetTexture(Texture texture) => _underlyingImplementation.material.SetTexture(MiniGameRenderTextureProperty, texture);
	}
}