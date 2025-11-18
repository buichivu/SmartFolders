# 🎨 Smart Folders

**Advanced folder color and organization system for Unity Project Window**

[![Unity Version](https://img.shields.io/badge/Unity-2020.3%2B-blue.svg)](https://unity.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

Transform your Unity project organization with beautiful folder colors, pattern matching, and instant presets!

## ✨ Features

- 🎯 **Folder Coloring** - Add colors to any folder
- 🔍 **Pattern Matching** - Use wildcards like `Assets/Scripts/*`
- 🎨 **6 Built-in Presets** - Professional themes ready to use
- 📝 **Context Menus** - Right-click integration
- 🪟 **Editor Window** - Beautiful UI for managing colors
- ⚡ **High Performance** - Optimized texture caching

## 📦 Installation

### Via Package Manager

1. Open Unity
2. Go to `Window → Package Manager`
3. Click `+` → `Add package from git URL...`
4. Paste: `https://github.com/buichivu/SmartFolders.git`
5. Click `Add`

### Via manifest.json

Add to your `Packages/manifest.json`:

\`\`\`json
{
  "dependencies": {
    "com.buichivu.smartfolders": "https://github.com/buichivu/SmartFolders.git"
  }
}
\`\`\`

## 🚀 Quick Start

### Fastest Way - Apply a Preset

1. Right-click in Project Window
2. Select `Smart Folders → Apply Preset → Pastel`
3. Done! ✨

### Custom Colors

1. Open `Window → Smart Folders`
2. Select "Quick Color" tab
3. Click "Use Selected Folder in Project"
4. Choose a color
5. Click "Apply Color"

## 🎨 Built-in Presets

- **Pastel** - Soft, easy on the eyes
- **Vibrant** - Bold, high-contrast colors
- **Monochrome** - Grayscale minimal
- **Nature** - Earth tones
- **Neon** - Bright colors for dark theme
- **Corporate** - Professional business colors

## 📖 Usage

### Setting Folder Colors

**Via Context Menu:**
\`\`\`
Right-click folder → Smart Folders → Set Folder Color...
\`\`\`

**Via Window:**
\`\`\`
Window → Smart Folders → Quick Color tab
\`\`\`

### Pattern Matching

Color multiple folders at once:

\`\`\`
Path: Assets/Scripts/*
Enable "Use Pattern Matching"
\`\`\`

This will color all subfolders under `Assets/Scripts/`

## 🛠️ API

\`\`\`csharp
using SmartFolders.Core;

// Reload all colors
SmartFolders.ReloadSettings();

// Get statistics
Debug.Log(SmartFolders.GetStatistics());

// Check if folder has color
bool hasColor = SmartFolders.ColorManager.HasColor("Assets/Scripts");
\`\`\`

## 📝 License

MIT License - see [LICENSE](LICENSE) file

## 👤 Author

**Bui Chi Vu**
- Email: buivuisme@gmail.com
- GitHub: [@buichivu](https://github.com/buichivu)

## 🌟 Support

If you find this useful:
- ⭐ Star the repository
- 🐛 Report bugs
- 💡 Suggest features

---

**Made with ❤️ for the Unity Community**
