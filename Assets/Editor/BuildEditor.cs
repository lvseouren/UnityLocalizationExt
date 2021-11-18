using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets;
using System;
using System.Linq;
using UnityEditor.Build.Reporting;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization.Tables;

namespace Demo.Editor
{
    public class BuildEditor : MonoBehaviour
    {
        private static string localOutputPath = null;
        [MenuItem("Build Tools/BuildPlayer", false)]
        static void AddressablesBuild()
        {
            localOutputPath = GetApplicationBuildPath();
            if (string.IsNullOrEmpty(localOutputPath))
                return;
            var settings = AddressableAssetSettingsDefaultObject.GetSettings(true);
            var context = new AddressablesDataBuilderInput(settings, "0.0.0.1");
            var result = settings.ActivePlayerDataBuilder.BuildData<AddressablesPlayerBuildResult>(context);
            if (string.IsNullOrEmpty(result.Error))
                BuildInternal();
            else
                Debug.LogErrorFormat("build failed: {0}", result.Error);
        }

        private static string GetApplicationBuildPath()
        {
            string outputPath;
            const string title = "Choose Location of the Built Game";
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
                outputPath = EditorUtility.SaveFilePanel(title, Environment.CurrentDirectory, Application.productName,
                    "apk");
            else
                outputPath =
                    EditorUtility.SaveFolderPanel(title, Environment.CurrentDirectory, Application.productName);
            return outputPath;
        }

        private static void BuildInternal()
        {
            var options = new BuildPlayerOptions
            {
                locationPathName = localOutputPath,
                scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(scene => scene.path).ToArray(),
                options = BuildOptions.Development,
                target = EditorUserBuildSettings.activeBuildTarget
            };
            var report = BuildPipeline.BuildPlayer(options);
            if (report.summary.result == BuildResult.Failed)
            {
                var errSteps = report.steps.Where(step =>
                        step.messages.Any(message =>
                            message.type == LogType.Error || message.type == LogType.Exception))
                    .ToList();
                throw new Exception("Build Player error: \n");
            }
        }

        //private  IEnumerator tsetfunc()
        //{
        //    var op = Addressables.LoadAssetAsync<StringTable>("key_value");
        //    yield return op.IsDone;
        //    var stringTable = op.Result;
        //}
    }
}
