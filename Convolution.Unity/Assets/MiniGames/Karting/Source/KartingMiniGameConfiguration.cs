using System.Collections.Generic;
using Convolution.Core.EmbeddedMiniGames;
using UnityEngine;
using Zenject;

namespace Convolution.MiniGames.Karting
{
    [CreateAssetMenu(menuName = "Convolution/MiniGames/Karting", fileName = nameof(KartingMiniGameConfiguration))]
    public sealed class KartingMiniGameConfiguration : EmbeddedMiniGameConfiguration<KartingMiniGame>
    {
        [SerializeField]
        private Kart _kartPrefab;

        [SerializeField]
        private float _kartSteeringSmoothing;

        [SerializeField]
        private float _kartSpeed;
        
        [SerializeField]
        private Vector2 _cellSize;

        [SerializeField]
        private Chunk[] _chunkPrefabs;

        [SerializeField]
        private Chunk _endChunkPrefab;

        [SerializeField]
        private int _goal;
        
        [SerializeField]
        private float _cameraCatchupSmoothing;

        [SerializeField]
        private Vector2 _cameraOffset;
        
        public Kart KartPrefab => _kartPrefab;
        public float KartSteeringSmoothing => _kartSteeringSmoothing;
        public float KartSpeed => _kartSpeed;
        public Vector2 CellSize => _cellSize;
        public IReadOnlyList<Chunk> ChunkPrefabs => _chunkPrefabs;
        public Chunk EndChunkPrefab => _endChunkPrefab;
        public int Goal => _goal;
        public float CameraCatchupSmoothing => _cameraCatchupSmoothing;
        public Vector2 CameraOffset => _cameraOffset;
        
        public override void BindDependencies(DiContainer container) { }
    }
}
