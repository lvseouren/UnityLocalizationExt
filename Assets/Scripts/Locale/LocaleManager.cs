using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LocaleManager 
{

    private string stringTableName;
    private string assetTableName;
    private StringTable stringCollction;
    private AssetTable assetCollciton;
    private static LocaleManager instance;

    public static LocaleManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new LocaleManager();
            }
            return instance;
        }
    }

    public string CreateLocalizedString(string collectionName, string entryName, params object [] objs)
    {
        var stringOperation = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(collectionName, entryName);
        //if (stringOperation.IsDone)
        //{
        //    if (stringOperation.Status == AsyncOperationStatus.Failed)
        //        Debug.LogError("Failed to load string");
        //    else
        //        return stringOperation.Result;
        //}
        return "";
    }

    public Sprite CreateLocalizedSprite(string collectionName, string entryName, params object[] objs)
    {
        //var AssetOperation = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<Sprite>(collectionName, entryName);
        //if (AssetOperation.IsDone)
        //{
        //    if (AssetOperation.Status == AsyncOperationStatus.Failed)
        //        Debug.LogError("Failed to load string");
        //    else
        //        return AssetOperation.Result as Sprite;
        //}
        return null;
    }

    public Texture CreateLocalizedTexture(string collectionName, string entryName)
    {
        var AssetOperation = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<Texture>(collectionName, entryName);
        if (AssetOperation.IsDone)
        {
            if (AssetOperation.Status == AsyncOperationStatus.Failed)
                Debug.LogError("Failed to load string");
            else
                return AssetOperation.Result as Texture;
        }
        return null;
    }

    public AudioClip CreateLocalizedAudio(string collectionName, string entryName)
    {
        var AssetOperation = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<AudioClip>(collectionName, entryName);
        if (AssetOperation.IsDone)
        {
            if (AssetOperation.Status == AsyncOperationStatus.Failed)
                Debug.LogError("Failed to load string");
            else
                return AssetOperation.Result as AudioClip;
        }
        return null;
    }

    public GameObject CreateLocalizedPrefab(string collectionName, string entryName)
    {
        var AssetOperation = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<GameObject>(collectionName, entryName);
        if (AssetOperation.IsDone)
        {
            if (AssetOperation.Status == AsyncOperationStatus.Failed)
                Debug.LogError("Failed to load string");
            else
                return AssetOperation.Result as GameObject;
        }
        return null;
    }

    public IEnumerator GetStringTableAsync(string tableName)
    {
        stringTableName = tableName;
        var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(tableName);
        yield return loadingOperation;
        if(loadingOperation.Status == AsyncOperationStatus.Succeeded)
        stringCollction = loadingOperation.Result;
    }

    public IEnumerator GetAssetTableAsync(string tableName)
    {
        assetTableName = tableName;
        var loadingOperation = LocalizationSettings.AssetDatabase.GetTableAsync(tableName);
        yield return loadingOperation;
        if(loadingOperation.Status == AsyncOperationStatus.Succeeded)
        {
            assetCollciton = loadingOperation.Result;
        }
    }

    public string GetString(string key, params object [] objs)
    {
        //if(stringCollction == null)
        //{
        //    if (!string.IsNullOrEmpty(stringTableName))
        //    {
        //        GetStringTableAsync(stringTableName);
        //    }
        //}
        var entry = stringCollction.GetEntry(key);
        return entry.GetLocalizedString(objs);
    }


    public Sprite GetSprite(string key)
    {
        if (assetCollciton == null)
        {
            if (!string.IsNullOrEmpty(assetTableName))
            {
                GetStringTableAsync(assetTableName);
            }
        }
        if(!string.IsNullOrEmpty(assetTableName) && !string.IsNullOrEmpty(key))
        {
            return CreateLocalizedSprite(assetTableName, key);
        }
        else
        {
            return null;
        }
    }

    public Texture GetTextrue(string key)
    {
        if (assetCollciton == null)
        {
            if (!string.IsNullOrEmpty(assetTableName))
            {
                GetStringTableAsync(assetTableName);
            }
        }
        if (!string.IsNullOrEmpty(assetTableName) && !string.IsNullOrEmpty(key))
        {
            return CreateLocalizedTexture(assetTableName, key);
        }
        else
        {
            return null;
        }
    }

    public AudioClip GetAudioClip(string key)
    {
        if (assetCollciton == null)
        {
            if (!string.IsNullOrEmpty(assetTableName))
            {
                GetStringTableAsync(assetTableName);
            }
        }
        if (!string.IsNullOrEmpty(assetTableName) && !string.IsNullOrEmpty(key))
        {
            return CreateLocalizedAudio(assetTableName, key);
        }
        else
        {
            return null;
        }
    }

    public GameObject GetGameObject(string key)
    {
        if (assetCollciton == null)
        {
            if (!string.IsNullOrEmpty(assetTableName))
            {
                GetStringTableAsync(assetTableName);
            }
        }
        if (!string.IsNullOrEmpty(assetTableName) && !string.IsNullOrEmpty(key))
        {
            return CreateLocalizedPrefab(assetTableName, key);
        }
        else
        {
            return null;
        }
    }
}
