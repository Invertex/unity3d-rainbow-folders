﻿/*
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 */

using System.IO;
using Borodar.RainbowFolders.Editor.Settings;
using UnityEditor;
using UnityEngine;

namespace Borodar.RainbowFolders.Editor
{
    /* 
    * This script allows you to set custom icons for folders in project browser.
    * Recommended icon sizes - small: 16x16 px, large: 64x64 px;
    */

    [InitializeOnLoad]
    public class RainbowFoldersBrowserIcons
    {
        #region reserved_folder_names
        private const string EDITOR_FOLDER_NAME = "Editor";
        private const string PLUGINS_FOLDER_NAME = "Plugins";
        private const string RESOURCES_FOLDER_NAME = "Resources";
        private const string GIZMOS_FOLDER_NAME = "Gizmos";
        private const string STREAMING_ASSETS_FOLDER_NAME = "StreamingAssets";
        #endregion

        private static RainbowFoldersSettings _settings;

        static RainbowFoldersBrowserIcons()
        {
            EditorApplication.projectWindowItemOnGUI += ReplaceFolderIcon;
        }

        static void ReplaceFolderIcon(string guid, Rect rect)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);

            if (!AssetDatabase.IsValidFolder(path)) return;

            var isSmall = rect.width > rect.height;
            if (isSmall)
            {
                rect.width = rect.height;
            }
            else
            {
                rect.height = rect.width;
            }

            _settings = _settings ?? RainbowFoldersSettings.Load();

            var texture = _settings.GetTextureByFolderName(Path.GetFileName(path), isSmall);
            if (texture != null) GUI.DrawTexture(rect, texture);
        }
    }
}