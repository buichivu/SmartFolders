using System.Collections.Generic;
using UnityEngine;

namespace SmartFolders.Data
{
    [CreateAssetMenu(menuName = "Smart Folders/Folder Color Collection", fileName = "FolderColorCollection")]
    public class FolderColorCollection : ScriptableObject
    {
        [Tooltip("List of folder color configurations")]
        public List<FolderColor> Data = new List<FolderColor>();

        [Header("Settings")]
        [Tooltip("Auto-reload when this asset is modified")]
        public bool AutoReload = true;

        [Tooltip("Enable debug logging")]
        public bool DebugMode = false;

        private void OnValidate()
        {
            if (AutoReload)
            {
                Core.SmartFolders.ReloadSettings();

                if (DebugMode)
                {
                    Debug.Log($"[Smart Folders] Reloaded {Data.Count} folder colors from {name}");
                }
            }
        }

        public Color? GetColorForPath(string path)
        {
            for (int i = Data.Count - 1; i >= 0; i--)
            {
                var folderColor = Data[i];
                if (folderColor.IsValid() && folderColor.MatchesPath(path))
                {
                    return folderColor.Color;
                }
            }

            return null;
        }

        public void AddFolder(string path, Color color)
        {
            Data.Add(new FolderColor
            {
                Path = path,
                Color = color
            });
        }

        public bool RemoveFolder(string path)
        {
            var item = Data.Find(f => f.Path == path);
            if (item != null)
            {
                Data.Remove(item);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            Data.Clear();
        }
    }
}
