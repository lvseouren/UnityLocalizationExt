using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public static class LocalizationExtension
{
    public static void _SetString(this PgLocalizeStringEvent target, string key, params object[] args)
    {
        string collectionNameCache = target.StringReference.TableReference.TableCollectionName;
        bool isKeyExist = CheckCollectionHasKey(collectionNameCache, key);
        if (!isKeyExist)
        {
            target.StringReference.TableReference = LocalizationSettings.StringDatabase.DefaultTable;
        }
        else
        {
            target.StringReference.Arguments = args;
        }
        target.StringReference.TableEntryReference = key;
        target.RefreshString();
    }

    public static bool CheckCollectionHasKey(string collectionName, string key)
    {
        var table = LocalizationSettings.StringDatabase.GetTable(collectionName);
        return table.GetEntry(key)!=null;
    }
}
