using System;
using UnityEngine;

namespace SmartFolders.Data
{
    /// <summary>
    /// Represents a folder with its associated color and settings.
    /// </summary>
    [Serializable]
    public class FolderColor
    {
        [Tooltip("Full path to the folder (e.g., Assets/Scripts)")]
        public string Path;

        [Tooltip("Color to apply to this folder. Alpha controls opacity.")]
        [ColorUsage(true, false)]
        public Color Color = new Color(1f, 1f, 1f, 0.2f);

        [Tooltip("Enable pattern matching (e.g., Assets/Scripts/* will match all subfolders)")]
        public bool UsePattern = false;

        /// <summary>
        /// Validates if this folder color configuration is valid.
        /// </summary>
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Path))
                return false;

            if (Color.a <= 0)
                return false;

            if (!Path.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
                return false;

            return true;
        }

        /// <summary>
        /// Checks if this color applies to the given path.
        /// </summary>
        public bool MatchesPath(string targetPath)
        {
            if (UsePattern)
            {
                return PathMatchesPattern(targetPath, Path);
            }
            else
            {
                return string.Equals(targetPath, Path, StringComparison.OrdinalIgnoreCase);
            }
        }

        private bool PathMatchesPattern(string path, string pattern)
        {
            pattern = pattern.Replace("*", ".*").Replace("?", ".");
            return System.Text.RegularExpressions.Regex.IsMatch(path, "^" + pattern + "$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
    }
}