using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Localization.UI;
using UnityEngine.UIElements;
using UnityEditor.Localization;
using UnityEngine.Localization.Settings;
using System.IO;
using System.Text;

public class CreateLocalizationTables
{
    [MenuItem("Assets/CreateLocalizationTables", false, 1)]
    public static void CreateTable()
    {
        string directory = Application.dataPath + "/LocaleCollection";
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        StringBuilder str = new StringBuilder(directory);
        var name = Selection.activeObject.name;
        //Debug.Log("选中物体的名字" + name);
        str.Append("/");
        var parentPath = str.Append(name).ToString();
        var assetPath = "/Assets";
        var stringPath = "/String";
        
        var createAssetPath = string.Concat(parentPath, assetPath);
        var createStringPath = string.Concat(parentPath, stringPath);

        if (!Directory.Exists(createAssetPath))
        {
            Directory.CreateDirectory(createAssetPath);
        }

        if (!Directory.Exists(createStringPath))
        {
            Directory.CreateDirectory(createStringPath);
        }

        //var fileStringName = string.Concat(name, "StringCollection");
        //var fileAssetName = string.Concat(name, "AssetsCollection");

        if (!File.Exists(createStringPath + "/" + name + ".asset"))
        {
            LocalizationEditorSettings.CreateStringTableCollection(name, createStringPath);
        }

        if (!File.Exists(createAssetPath + "/" + name + ".asset"))
        {
            LocalizationEditorSettings.CreateAssetTableCollection(name, createAssetPath);
        }
        

        //var assetDirectory = EditorUtility.SaveFolderPanel("Create Table Collection", "Assets/", "");
        //if (string.IsNullOrEmpty(assetDirectory))
        //    return;

        //LocalizationTableCollection createdCollection = null;
        //if (m_CollectionTypePopup.value == typeof(StringTableCollection))
        //{
        //    createdCollection = LocalizationEditorSettings.CreateStringTableCollection("StringTableCollection", assetDirectory, GetSelectedLocales());
        //}
        //if (m_CollectionTypePopup.value == typeof(AssetTableCollection))
        //{
        //    createdCollection = LocalizationEditorSettings.CreateAssetTableCollection(m_TableCollectionName.value, assetDirectory, GetSelectedLocales());
        //}
        //var created = CreateLocalizationAsset();
        //if (created != null)
        //{
        //    LocalizationEditorSettings.ActiveLocalizationSettings = created;
        //}
    }

}
