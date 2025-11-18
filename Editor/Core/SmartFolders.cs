using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using SmartFolders.Data;

namespace SmartFolders.Core
{
    /// <summary>
    /// Main system that handles folder coloring in the Unity Project Window.
    /// </summary>
    [InitializeOnLoad]
    public static class SmartFoldersSystem
    {
        private static FolderColorCollection settings;
        private static Dictionary<string, Color> pathColorCache = new Dictionary<string, Color>();
        private static Dictionary<Color, Texture2D> textureCache = new Dictionary<Color, Texture2D>();

        static SmartFoldersSystem()
        {
            EditorApplication.projectWindowItemOnGUI -= OnProjectWindowItemGUI;
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
            ReloadSettings();
        }

        /// <summary>
        /// Reloads the folder color settings from the settings file.
        /// </summary>
        public static void ReloadSettings()
        {
            pathColorCache.Clear();

            // Try to find the settings file
            string[] guids = AssetDatabase.FindAssets("t:FolderColorCollection");
            if (guids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                settings = AssetDatabase.LoadAssetAtPath<FolderColorCollection>(path);
            }
            else
            {
                settings = null;
            }
        }

        /// <summary>
        /// Gets the current settings instance, creating one if necessary.
        /// </summary>
        public static FolderColorCollection GetOrCreateSettings()
        {
            if (settings == null)
            {
                ReloadSettings();
            }

            if (settings == null)
            {
                // Create default settings in Assets folder
                string settingsPath = "Assets/SmartFoldersSettings.asset";
                settings = ScriptableObject.CreateInstance<FolderColorCollection>();
                AssetDatabase.CreateAsset(settings, settingsPath);
                AssetDatabase.SaveAssets();
                Debug.Log($"Created Smart Folders settings at {settingsPath}");
            }

            return settings;
        }

        private static void OnProjectWindowItemGUI(string guid, Rect selectionRect)
        {
            if (settings == null)
                return;

            string path = AssetDatabase.GUIDToAssetPath(guid);

            // Only process directories
            if (!AssetDatabase.IsValidFolder(path))
                return;

            // Get color from cache or calculate it
            Color? color = GetColorForPath(path);
            if (!color.HasValue)
                return;

            // Get or create texture for this color
            Texture2D colorTexture = GetOrCreateTexture(color.Value);

            // Draw the colored rectangle
            Rect bgRect = selectionRect;
            bgRect.x = 0;
            bgRect.width = selectionRect.x + selectionRect.width + 16;

            GUI.DrawTexture(bgRect, colorTexture);
        }

        private static Color? GetColorForPath(string path)
        {
            if (pathColorCache.TryGetValue(path, out Color cachedColor))
            {
                return cachedColor;
            }

            Color? color = settings.GetColorForPath(path);
            if (color.HasValue)
            {
                pathColorCache[path] = color.Value;
            }

            return color;
        }

        private static Texture2D GetOrCreateTexture(Color color)
        {
            if (textureCache.TryGetValue(color, out Texture2D texture))
            {
                return texture;
            }

            texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            textureCache[color] = texture;

            return texture;
        }

        /// <summary>
        /// Clears all caches and forces a refresh.
        /// </summary>
        public static void ClearCaches()
        {
            pathColorCache.Clear();

            // Clean up textures
            foreach (var texture in textureCache.Values)
            {
                if (texture != null)
                    Object.DestroyImmediate(texture);
            }
            textureCache.Clear();

            EditorApplication.RepaintProjectWindow();
        }
    }
}
