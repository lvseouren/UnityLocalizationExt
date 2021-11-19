using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public static class LocalizationExtension
{
    public static void SetString(this LocalizeStringEvent target, string key, params object[] args)
    {
        string collectionNameCache = target.StringReference.TableReference.TableCollectionName;
        bool isKeyExist = CheckCollectionHasKey(collectionNameCache, key);
        if (!isKeyExist)
        {
            //target.StringReference.TableReference = "Common";
            var txt = target.gameObject.GetComponent<Text>();
            if(txt)
            {
                txt.text = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Common", key, args).Result;
            }
        }
        else
        {
            target.StringReference.Arguments = args;
            target.StringReference.TableEntryReference = key;
            target.RefreshString();
        }
    }

    public static bool CheckCollectionHasKey(string collectionName, string key)
    {
        var table = LocalizationSettings.StringDatabase.GetTable(collectionName);
        return table.GetEntry(key)!=null;
    }
}
