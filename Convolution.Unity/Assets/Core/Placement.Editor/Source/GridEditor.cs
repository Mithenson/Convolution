using Maxim.Common.Extensions;
using UnityEditor;
using UnityEngine;

namespace Convolution.Placement.Editor
{
    [CustomEditor(typeof(Grid), true)]
    public sealed class GridEditor : UnityEditor.Editor
    {
        private const string DirectoryForConfigurationSearch = "Assets/Core/Placement/Assets";

        private static GridConfiguration _configuration;
        private static GridConfiguration LazyConfiguration
        {
            get
            {
                if (_configuration != null)
                    return _configuration;
                
                var guids = AssetDatabase.FindAssets($"t:{nameof(GridConfiguration)}", new string[] { DirectoryForConfigurationSearch });
                
                _configuration = AssetDatabase.LoadAssetAtPath<GridConfiguration>(AssetDatabase.GUIDToAssetPath(guids[0]));
                return _configuration;
            }
        }
        
        private void OnSceneGUI()
        {
            var grid = (Grid)target;
            
            var color = Color.white.SetAlpha(0.25f);
            var vertices = new Vector3[4];
            var halfCellSize = LazyConfiguration.CellSize * 0.5f;
            
            foreach (var cell in grid.Cells)
            {
                var position = (Vector2)grid.transform.position + (Vector2)cell * (LazyConfiguration.CellSize + LazyConfiguration.Spacing);
                
                vertices[0] = position + new Vector2(-halfCellSize, -halfCellSize);
                vertices[1] = position + new Vector2(-halfCellSize, halfCellSize);
                vertices[2] = position + new Vector2(halfCellSize, halfCellSize);
                vertices[3] = position + new Vector2(halfCellSize, -halfCellSize);
                
                Handles.DrawSolidRectangleWithOutline(vertices, color, Color.black);
            }
        }
    }
}
