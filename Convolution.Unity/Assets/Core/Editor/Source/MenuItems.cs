using System;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace VirtCons.Internal.Editor.Source
{
    // Defaults to build for windows as this is the only supported target for now
    public static class MenuItems
    {
        private const string RelativeBuildDirectory = "Build";
        private const string BootstrapScenePath = "Assets/Core/Application/Windows/Scenes/Bootstrap@Windows.unity";
        
        [MenuItem("VirtCons/Build")]
        public static void Build()
        {
            CleanupBuildFolder(RelativeBuildDirectory);

            PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
            PlayerSettings.defaultScreenHeight = 960;
            PlayerSettings.defaultScreenWidth = 540;
            PlayerSettings.runInBackground = true;
            PlayerSettings.resizableWindow = true;
            PlayerSettings.forceSingleInstance = true;
            PlayerSettings.useFlipModelSwapchain = false;
            
            BuildAddressables();

            var applicationPath = Path.Combine(RelativeBuildDirectory, $"{PlayerSettings.productName}.exe");
            var options = new BuildPlayerOptions()
            {
                options = BuildOptions.Development,
                target = BuildTarget.StandaloneWindows,
                targetGroup = BuildTargetGroup.Standalone,
                locationPathName = applicationPath,
                scenes = new string[] { BootstrapScenePath }
            };

            var report = BuildPipeline.BuildPlayer(options);
            switch (report.summary.result )
            {
                case BuildResult.Succeeded:
                    Debug.Log($"Build succeeded, `Size={report.summary.totalSize} bytes, Time={report.summary.totalTime.Seconds} seconds`.");
                    EditorUtility.RevealInFinder(applicationPath);
                    break;

                case BuildResult.Failed:
                    Debug.LogError("Build failed.");
                    break;

                case BuildResult.Cancelled:
                    break;

                default:
                    Debug.LogWarning($"Build ended with an unknown `{nameof(BuildResult)}={report.summary.result}`.");
                    break;
            }
        }

        private static void CleanupBuildFolder(string buildDirectory)
        {
            if (!Directory.Exists(buildDirectory))
                return;
            
            Directory.Delete(buildDirectory, true);
        }
        private static void BuildAddressables()
        {
            AddressableAssetSettings.BuildPlayerContent(out var result);

            if (!string.IsNullOrEmpty(result.Error))
                throw new InvalidOperationException($"Could not build addressables, `Error={result.Error}`");
        }
    }
}
