using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

public class PgLocalizeStringEvent : LocalizeStringEvent
{
    string originCollection;
    private void Awake()
    {
        originCollection = StringReference.TableReference;
    }

    public void SetString(string key, params object[] args)
    {
        bool isKeyExist = CheckCollectionHasKey(key);
        StringReference.Arguments = args;
        if (isKeyExist)
        {
            StringReference.SetReference(originCollection, key);
        }
        else
        {
            StringReference.SetReference(LocalizationSettings.StringDatabase.DefaultTable, key);
        }
    }

    public bool CheckCollectionHasKey(string key)
    {
        var table = LocalizationSettings.StringDatabase.GetTable(originCollection);
        return table.GetEntry(key) != null;
    }
}
