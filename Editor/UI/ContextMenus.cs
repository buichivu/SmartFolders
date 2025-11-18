using UnityEngine;
using UnityEditor;
using SmartFolders.Core;
using SmartFolders.Data;

namespace SmartFolders.UI
{
    public static class ContextMenus
    {
        [MenuItem("Assets/Smart Folders/Set Folder Color", false, 2000)]
        public static void SetFolderColor()
        {
            EditorUtility.DisplayDialog("Smart Folders", 
                "Select a folder and choose a color.\n\nFor now, create a FolderColorCollection manually:\nRight-click → Create → Smart Folders → Folder Color Collection", 
                "OK");
        }

        [MenuItem("Assets/Smart Folders/Apply Preset/Pastel", false, 2040)]
        public static void ApplyPresetPastel() => ApplyPreset("Pastel");

        [MenuItem("Assets/Smart Folders/Reload All Colors", false, 2060)]
        public static void ReloadColors()
        {
            SmartFolders.ReloadSettings();
            Debug.Log("[Smart Folders] Reloaded all folder colors");
        }

        private static void ApplyPreset(string presetName)
        {
            var collection = ScriptableObject.CreateInstance<FolderColorCollection>();
            
            // Pastel preset
            collection.AddFolder("Assets/Scripts", new Color(0.7f, 0.85f, 1f, 0.25f));
            collection.AddFolder("Assets/Prefabs", new Color(1f, 0.9f, 0.7f, 0.25f));
            collection.AddFolder("Assets/Resources", new Color(1f, 1f, 0.7f, 0.25f));
            collection.AddFolder("Assets/Materials", new Color(0.9f, 0.7f, 1f, 0.25f));
            collection.AddFolder("Assets/Scenes", new Color(0.7f, 1f, 0.85f, 0.25f));
            
            var path = $"Assets/FolderColors_{presetName}.asset";
            path = AssetDatabase.GenerateUniqueAssetPath(path);
            
            AssetDatabase.CreateAsset(collection, path);
            AssetDatabase.SaveAssets();
            
            SmartFolders.ReloadSettings();
            
            Selection.activeObject = collection;
            EditorGUIUtility.PingObject(collection);
            
            Debug.Log($"[Smart Folders] Applied preset '{presetName}' - Created at: {path}");
        }
    }
}
