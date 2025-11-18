using UnityEditor;
using UnityEngine;
using SmartFolders.Core;
using SmartFolders.Data;

namespace SmartFolders.UI
{
    /// <summary>
    /// Provides context menu items for Smart Folders functionality.
    /// </summary>
    public static class ContextMenus
    {
        [MenuItem("Assets/Smart Folders/Apply Preset/Pastel", false, 2000)]
        private static void ApplyPresetPastel()
        {
            var settings = SmartFoldersSystem.GetOrCreateSettings();

            // Define pastel color palette
            var pastelColors = new[]
            {
                new { Path = "Assets/Scripts", Color = new Color(1f, 0.7f, 0.7f, 0.3f) },      // Pastel Red
                new { Path = "Assets/Prefabs", Color = new Color(0.7f, 0.9f, 1f, 0.3f) },      // Pastel Blue
                new { Path = "Assets/Materials", Color = new Color(1f, 1f, 0.7f, 0.3f) },      // Pastel Yellow
                new { Path = "Assets/Scenes", Color = new Color(0.9f, 0.7f, 1f, 0.3f) },       // Pastel Purple
                new { Path = "Assets/Resources", Color = new Color(0.7f, 1f, 0.9f, 0.3f) },    // Pastel Mint
                new { Path = "Assets/Editor", Color = new Color(1f, 0.85f, 0.7f, 0.3f) }       // Pastel Orange
            };

            settings.Clear();

            foreach (var item in pastelColors)
            {
                settings.SetFolderColor(item.Path, item.Color);
            }

            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();

            SmartFoldersSystem.ClearCaches();
            SmartFoldersSystem.ReloadSettings();

            Debug.Log("Applied Pastel color preset to common folders");
        }

        [MenuItem("Assets/Smart Folders/Apply Preset/Vibrant", false, 2001)]
        private static void ApplyPresetVibrant()
        {
            var settings = SmartFoldersSystem.GetOrCreateSettings();

            // Define vibrant color palette
            var vibrantColors = new[]
            {
                new { Path = "Assets/Scripts", Color = new Color(1f, 0.2f, 0.2f, 0.4f) },      // Red
                new { Path = "Assets/Prefabs", Color = new Color(0.2f, 0.5f, 1f, 0.4f) },      // Blue
                new { Path = "Assets/Materials", Color = new Color(1f, 0.9f, 0.2f, 0.4f) },    // Yellow
                new { Path = "Assets/Scenes", Color = new Color(0.8f, 0.2f, 1f, 0.4f) },       // Purple
                new { Path = "Assets/Resources", Color = new Color(0.2f, 1f, 0.5f, 0.4f) },    // Green
                new { Path = "Assets/Editor", Color = new Color(1f, 0.6f, 0.2f, 0.4f) }        // Orange
            };

            settings.Clear();

            foreach (var item in vibrantColors)
            {
                settings.SetFolderColor(item.Path, item.Color);
            }

            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();

            SmartFoldersSystem.ClearCaches();
            SmartFoldersSystem.ReloadSettings();

            Debug.Log("Applied Vibrant color preset to common folders");
        }

        [MenuItem("Assets/Smart Folders/Reload Colors", false, 2100)]
        private static void ReloadColors()
        {
            SmartFoldersSystem.ClearCaches();
            SmartFoldersSystem.ReloadSettings();
            Debug.Log("Smart Folders: Colors reloaded");
        }

        [MenuItem("Assets/Smart Folders/Clear All Colors", false, 2101)]
        private static void ClearAllColors()
        {
            var settings = SmartFoldersSystem.GetOrCreateSettings();
            settings.Clear();

            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();

            SmartFoldersSystem.ClearCaches();
            SmartFoldersSystem.ReloadSettings();

            Debug.Log("Smart Folders: All colors cleared");
        }

        [MenuItem("Assets/Smart Folders/Open Settings", false, 2102)]
        private static void OpenSettings()
        {
            var settings = SmartFoldersSystem.GetOrCreateSettings();
            Selection.activeObject = settings;
            EditorGUIUtility.PingObject(settings);
        }
    }
}