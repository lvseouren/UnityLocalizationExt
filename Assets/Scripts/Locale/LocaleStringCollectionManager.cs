using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LocaleStringCollectionManager
{
    private static LocaleStringCollectionManager instance;

    private static Dictionary<string, StringTableEntry> localStringDic = new Dictionary<string, StringTableEntry>();

    public List<StringTable> sts;

    public event Action OnSelectedLocaleChanged;

    public event Action SelectedLocaleChanged
    {
        add => Instance.OnSelectedLocaleChanged += value;
        remove => Instance.OnSelectedLocaleChanged -= value;
    }

    public static LocaleStringCollectionManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new LocaleStringCollectionManager();
            }
            return instance;
        }
    }

    public Dictionary<string, StringTableEntry> LocalString
    {
        get => localStringDic;
        set => localStringDic = value;
    }

    public void LoadAllStringCollection()
    {
        Debug.Log("-----begin Load locale-----");
        var locale = LocalizationSettings.SelectedLocale;
        LocaleIdentifier identifier = locale.Identifier;
        string code = identifier.Code;
        string label = "Locale-" + code;
        Debug.Log("label is ===" + label);
        var op = Addressables.LoadAssetsAsync<StringTable>(label, ParseStringTable);
        var val = op.WaitForCompletion();
        if (op.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Successful");
            //sts = (List<StringTable>)op.Result;
            OnSelectedLocaleChanged?.Invoke();
        }
        else
        {
            Debug.LogError("Load Local Fail");
        }
    }

    private void ParseStringTable(StringTable st)
    {
        foreach(var entry in st)
        {
            var a = entry.Value.Key;
            var b = entry.Value.KeyId;
            localStringDic[a] = entry.Value;
        }
        Debug.Log("------------log-----------");
    }

    public string GetLocaleString(string key, params object[] objs)
    {
        StringTableEntry value;
        if (localStringDic.TryGetValue(key, out value))
        {
            var str = value.GetLocalizedString(objs);
            return str;
        }
        return "";
    }

    public string GetLocalizationString(string key)
    {
        for (int i = 0; i < sts.Count; ++i)
        {
            var entry = sts[i][key];
            if (entry != null)
            {
                return entry.ToString();
            }
        }
        return string.Empty;
    }
}
