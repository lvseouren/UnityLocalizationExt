using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocaleAssetBehaviour : MonoBehaviour
{
    private string collectionName;

    private string key;

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
    }

    private void OnDisable()
    {
        
    }

    private void OnSelectedLocaleChanged(Locale locale)
    {

    }


    void UpdateAsset()
    {

    }



    public void UpdateLocaleString(string collection, string key)
    {
        collectionName = collection;
        this.key = key;
    }

    

}
