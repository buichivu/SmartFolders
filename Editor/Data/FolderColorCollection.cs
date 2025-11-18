using System.Collections.Generic;
using UnityEngine;

namespace SmartFolders.Data
{
    /// <summary>
    /// ScriptableObject that stores a collection of folder colors.
    /// </summary>
    [CreateAssetMenu(fileName = "FolderColorCollection", menuName = "Smart Folders/Folder Color Collection")]
    public class FolderColorCollection : ScriptableObject
    {
        [Tooltip("List of folders and their associated colors")]
        public List<FolderColor> FolderColors = new List<FolderColor>();

        /// <summary>
        /// Gets the color for a specific path, or null if no color is defined.
        /// </summary>
        public Color? GetColorForPath(string path)
        {
            // Check in reverse order so later entries override earlier ones
            for (int i = FolderColors.Count - 1; i >= 0; i--)
            {
                var folderColor = FolderColors[i];
                if (folderColor.IsValid() && folderColor.MatchesPath(path))
                {
                    return folderColor.Color;
                }
            }
            return null;
        }

        /// <summary>
        /// Adds or updates a folder color entry.
        /// </summary>
        public void SetFolderColor(string path, Color color, bool usePattern = false)
        {
            // Check if entry already exists
            var existing = FolderColors.Find(fc => fc.Path == path);
            if (existing != null)
            {
                existing.Color = color;
                existing.UsePattern = usePattern;
            }
            else
            {
                FolderColors.Add(new FolderColor
                {
                    Path = path,
                    Color = color,
                    UsePattern = usePattern
                });
            }
        }

        /// <summary>
        /// Removes a folder color entry.
        /// </summary>
        public void RemoveFolderColor(string path)
        {
            FolderColors.RemoveAll(fc => fc.Path == path);
        }

        /// <summary>
        /// Clears all folder colors.
        /// </summary>
        public void Clear()
        {
            FolderColors.Clear();
        }
    }
}