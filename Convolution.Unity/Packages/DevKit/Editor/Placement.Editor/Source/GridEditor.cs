using System.Linq;
using Maxim.Common.Extensions;
using UnityEditor;
using UnityEngine;

namespace Convolution.DevKit.Placement.Editor
{
    [CustomEditor(typeof(Grid), true)]
    public sealed class GridEditor : UnityEditor.Editor
    {
        private const string ConfigurationPath = "Packages/com.convolution.devkit/Runtime/Placement/Resources/GridConfiguration.asset";
        
        private static GridConfiguration _configuration;
        private static GridConfiguration LazyConfiguration
        {
            get
            {
                if (_configuration != null)
                    return _configuration;
                
                _configuration = AssetDatabase.LoadAssetAtPath<GridConfiguration>(ConfigurationPath);
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
