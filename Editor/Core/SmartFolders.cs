using UnityEngine;
using UnityEditor;
using SmartFolders.Data;
using System.Collections.Generic;

namespace SmartFolders.Core
{
    [InitializeOnLoad]
    public static class SmartFolders
    {
        private static Dictionary<string, Color> _pathToColor = new Dictionary<string, Color>();
        private static Dictionary<Color, Texture2D> _colorToTexture = new Dictionary<Color, Texture2D>();
        private static bool _isEnabled = true;

        static SmartFolders()
        {
            ReloadSettings();
            EditorApplication.projectWindowItemOnGUI -= OnProjectWindowItemGUI;
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
            Debug.Log("[Smart Folders] Initialized successfully");
        }

        public static void ReloadSettings()
        {
            _pathToColor.Clear();
            
            var guids = AssetDatabase.FindAssets($"t:{nameof(FolderColorCollection)}");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var collection = AssetDatabase.LoadAssetAtPath<FolderColorCollection>(path);
                
                if (collection != null && collection.Data != null)
                {
                    foreach (var folderColor in collection.Data)
                    {
                        if (folderColor.IsValid())
                        {
                            _pathToColor[folderColor.Path] = folderColor.Color;
                        }
                    }
                }
            }
            
            Debug.Log($"[Smart Folders] Loaded {_pathToColor.Count} folder colors");
        }

        private static void OnProjectWindowItemGUI(string guid, Rect rect)
        {
            if (!_isEnabled) return;
            
            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (!AssetDatabase.IsValidFolder(path)) return;
            
            if (_pathToColor.TryGetValue(path, out var color))
            {
                var texture = GetTexture(color);
                if (texture != null)
                {
                    GUI.DrawTexture(rect, texture);
                }
            }
        }

        private static Texture2D GetTexture(Color color)
        {
            if (_colorToTexture.TryGetValue(color, out var texture) && texture != null)
            {
                return texture;
            }
            
            texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            _colorToTexture[color] = texture;
            
            return texture;
        }

        public static void SetEnabled(bool enabled)
        {
            _isEnabled = enabled;
        }

        public static bool IsEnabled => _isEnabled;

        public static string GetStatistics()
        {
            return $"Folders: {_pathToColor.Count} | Textures: {_colorToTexture.Count} | Enabled: {_isEnabled}";
        }
    }
}
