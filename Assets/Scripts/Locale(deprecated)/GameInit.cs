using System.Collections;
using System.Collections.Generic;
using UnityEngine.Localization.Settings;
using UnityEngine;
using UnityEngine.Localization;

public class GameInit : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        int index = PlayerPrefs.GetInt("Language", 0);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }

  
    // Update is called once per frame
    void Update()
    {
        
    }
}
