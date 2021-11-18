using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using TMPro;

public static class LocaleExtension
{
    public static void CreateLocaleTextString(this LocalizedStringDatabase localizedStringDatabase, Text txt, string key, string collection)
    {
        var collectionName = collection;
        if(collection == null)
        {
            collectionName = DefaultStringCollection();
        }
        txt.text = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(collectionName, key).Result;
    }

    public static void CreateLocaleTextProString(this LocalizedStringDatabase localizedStringDatabase, TextMeshProUGUI txt, string key, string collection)
    {
        var collectionName = collection;
        if (collection == null)
        {
            collectionName = DefaultStringCollection();
        }
        txt.text = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(collectionName, key).Result;
    }

    public static void CreateLocaleTextProString(this LocalizedStringDatabase localizedStringDatabase, TextMeshPro txt, string key, string collection)
    {
        var collectionName = collection;
        if (collection == null)
        {
            collectionName = DefaultStringCollection();
        }
        txt.text = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(collectionName, key).Result;
    }


    public static void CreateLocalSprite(this LocalizedAssetDatabase localizedAssetDatabase, Sprite sp, string key, string collection)
    {
        var collectionName = collection;
        if (collection == null)
        {
            collectionName = DefaultAssetCollection();
        }
        sp = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<Sprite>(collectionName, key).Result;
    }

    public static void CreateLocalRawImage(this LocalizedAssetDatabase localizedAssetDatabase, Texture image, string key, string collection)
    {
        var collectionName = collection;
        if (collection == null)
        {
            collectionName = DefaultAssetCollection();
        }
        image = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<Texture>(collectionName, key).Result;
    }


    public static void CreateLocaleAudio(this LocalizedAssetDatabase localizedAssetDatabase, AudioClip clip, string key, string collection)
    {
        var collectionName = collection;
        if (collection == null)
        {
            collectionName = DefaultAssetCollection();
        }
        clip = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<AudioClip>(collectionName, key).Result;
    }


    public static void CreateLocalePrefabs(this LocalizedAssetDatabase localizedAssetDatabase, GameObject prefabs, string key, string collection)
    {
        var collectionName = collection;
        if (collection == null)
        {
            collectionName = DefaultAssetCollection();
        }
        prefabs = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<GameObject>(collectionName, key).Result;
    }


    public static string DefaultStringCollection()
    {
        return "common";
    }

    public static string DefaultAssetCollection()
    {
        return "common";
    }
}
