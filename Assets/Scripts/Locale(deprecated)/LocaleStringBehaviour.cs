using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

[AddComponentMenu("Locale/Locale String Behaviour")]
[RequireComponent(typeof(TextMeshProUGUI))]
public class LocaleStringBehaviour : MonoBehaviour
{
    private string stringTableCollectionName ;
    private string entryKey;
    private object[] args;

    void OnEnable()
    {
        ///StartCoroutine(LoadStrings());
        LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
    }

    void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
    }

    void OnSelectedLocaleChanged(Locale obj)
    {
        updateString();
    }

    public void UpdateLocaleString(string key, params object[] objs)
    {
        int _index = key.IndexOf("/");
        stringTableCollectionName = key.Substring(0, _index);
        entryKey = key.Substring(_index + 1);
        args = objs;
        updateString();
    }

    void updateString()
    {
        if (!string.IsNullOrEmpty(stringTableCollectionName) && !string.IsNullOrEmpty(entryKey))
        {
            StartCoroutine(GetTableAsync());
        }
        else
        {
            return;
        }
    }

    IEnumerator GetTableAsync()
    {
        var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(stringTableCollectionName);
        yield return loadingOperation;

        if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
        {
            var stringTable = loadingOperation.Result;
            var str = GetLocalizedString(stringTable, entryKey, args);
            var component = GetComponent<TextMeshProUGUI>();
            component.text = str;
        }
        else
        {
             Debug.LogError("Could not load String Table\n" + loadingOperation.OperationException.ToString());
        }
    }

    string GetLocalizedString(StringTable table, string entryName, params object[] objs)
    {
        
        Debug.Log(" ÌõÄ¿Ãû³Æ: " + entryName);
        var entry = table.GetEntry(entryName);
        return entry.GetLocalizedString(objs);
    }

}
    
