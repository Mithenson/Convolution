using System;
using System.Threading.Tasks;
using Convolution.DevKit.Common;
using Convolution.DevKit.Controllers;
using Convolution.DevKit.MiniGames;
using UnityEngine;

using Object = UnityEngine.Object;
using Random = System.Random;

namespace Convolution.MiniGames.Karting
{
	public sealed class KartingMiniGame : MiniGame<KartingMiniGameConfiguration, MiniGameTopDownCameraDisplay, KartingMiniGame.InputChannel>
	{
		#region Nested types

		public enum InputChannel
		{
			Steering,
			Acceleration
		}

		#endregion
		
		private Kart _kart;
		private float _kartSteering;
		private float _kartSteeringVelocity;
		private float _kartAcceleration;
		private Vector2Int _kartCell;
		
		private Chunk[] _chunks;

		private Vector3 _cameraCatchupVelocity;

		public KartingMiniGame(KartingMiniGameConfiguration configuration, MiniGameTopDownCameraDisplay display, ObjectFactory factory) : base(configuration, display, factory) { }

		public override Task Bootup()
		{
			_display.Camera.transform.position += (Vector3)_configuration.CameraOffset;
			
			_kart = _factory.Instantiate(_configuration.KartPrefab);
			_kart.transform.position = _display.transform.position;

			_chunks = new Chunk[9];
			for (var i = 0; i < _chunks.Length; i++)
			{
				var offsetFromBottomLeft = new Vector2Int(Mathf.FloorToInt(i / 3.0f), i % 3);
				_chunks[i] = SpawnChunk(-Vector2.one + offsetFromBottomLeft);
			}
            
			_display.Bootup();
			return Task.CompletedTask;
		}

		protected override void IMP_HandleInput(IControllerInput input, InputChannel channel)
		{
			switch (channel)
			{
				case InputChannel.Steering:
					var targetSteering = ((SimpleControllerInput<float>)input).Value;
					_kartSteering = Mathf.SmoothDampAngle(_kartSteering, targetSteering, ref _kartSteeringVelocity, _configuration.KartSteeringSmoothing); 
					break;

				case InputChannel.Acceleration:
					_kartAcceleration = ((SimpleControllerInput<float>)input).Value;
					break;
			}
		}
        
		public override MiniGameState Tick()
		{
			MoveKart();
			
			var gridPosition = (Vector2)_display.transform.InverseTransformPoint(_kart.transform.position) + _configuration.CellSize * 0.5f;
			if (_kartCell.y >= _configuration.Goal && gridPosition.y % _configuration.CellSize.y > _configuration.CellSize.y * 0.5f)
				return MiniGameState.Won;
			
			SyncChunks(gridPosition);
			CatchupCamera();
			
			return MiniGameState.Running;
		}

		private void MoveKart()
		{
			var kartTransform = _kart.transform;
			
			kartTransform.rotation = Quaternion.Euler(0.0f, 0.0f, _kartSteering);
			kartTransform.position += kartTransform.up * (_configuration.KartSpeed * _kartAcceleration * Time.deltaTime);
		}

		private void SyncChunks(Vector2 gridPosition)
		{
			var newKartCell = new Vector2Int(Mathf.FloorToInt(gridPosition.x / _configuration.CellSize.x), Mathf.FloorToInt(gridPosition.y / _configuration.CellSize.y));
			var oldKartCell = _kartCell;
			
			if (newKartCell == oldKartCell)
				return;
			
			var oldBottomLeftCell = oldKartCell - Vector2Int.one;
			var newBottomLeftCell = newKartCell - Vector2Int.one;
			
			var copy = new Chunk[9];
			Array.Copy(_chunks, copy, 9);
			
			for (var i = 0; i < _chunks.Length; i++)
			{
				var offsetFromBottomLeft = new Vector2Int(Mathf.FloorToInt(i / 3.0f), i % 3);
				
				var oldCell = oldBottomLeftCell + offsetFromBottomLeft;
				var newCell = newBottomLeftCell + offsetFromBottomLeft;
				
				var oldToNewLocalOffset = oldCell - newKartCell;
				if (Mathf.Abs(oldToNewLocalOffset.x) > 1 || Mathf.Abs(oldToNewLocalOffset.y) > 1)
				{
					Object.Destroy(copy[i].gameObject);
				}
				else
				{
					var movedOffsetFromBottomLeft = oldToNewLocalOffset + Vector2Int.one;
					_chunks[movedOffsetFromBottomLeft.x * 3 + movedOffsetFromBottomLeft.y] = copy[i];
					
					var newToOldLocalOffset = newCell - oldKartCell;
					if (Mathf.Abs(newToOldLocalOffset.x) > 1 || Mathf.Abs(newToOldLocalOffset.y) > 1)
					{
						if (newCell.y >= _configuration.Goal)
							_chunks[i] = SpawnChunk(_configuration.EndChunkPrefab, newCell);
						else
							_chunks[i] = SpawnChunk(newCell);
					}
				}
			}

			_kartCell = newKartCell;
		}
		private Chunk SpawnChunk(Vector2 cell)
		{
			var random = new Random(cell.GetHashCode());
			var chunk = SpawnChunk(_configuration.ChunkPrefabs[random.Next(0, _configuration.ChunkPrefabs.Count)], cell);
			chunk.transform.rotation = Quaternion.Euler(0.0f, 0.0f, random.Next(0, 3) * 90.0f);

			return chunk;
		}
		private Chunk SpawnChunk(Chunk prefab, Vector2 cell)
		{
			var gridCenter = (Vector2)_display.transform.position;
			
			var chunk = _factory.Instantiate(prefab);
			chunk.transform.position = gridCenter + cell * _configuration.CellSize;

			return chunk;
		}

		private void CatchupCamera()
		{
			var kartPosition = _kart.transform.position;
			
			var cameraTransform = _display.Camera.transform;
			var cameraPosition = cameraTransform.position;
			var cameraTargetPosition = new Vector3(kartPosition.x, kartPosition.y, cameraPosition.z) + (Vector3)_configuration.CameraOffset;
			
			cameraTransform.position = Vector3.SmoothDamp(cameraPosition, cameraTargetPosition, ref _cameraCatchupVelocity, _configuration.CameraCatchupSmoothing);
		}
	}
}