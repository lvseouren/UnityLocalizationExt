using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class TestGetLocaleString : MonoBehaviour
{
    public Text text;
    bool flag = false;
    public LocalizeStringEvent localizeStringEvent;
    public PgLocalizeStringEvent pgLocalizeStringEvent;
    public void OnClick()
    {
        if (flag)
            text.text = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Home", "home_key1").Result;
        else
            text.text = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Battle", "d3").Result;
        flag = !flag;
    }

    public void TestStringExtMethod()
    {
        if (flag)
            localizeStringEvent.SetString("testCommon", "zhaomu");
        else
            localizeStringEvent.SetString("test_format", "linge");
        flag = !flag;
    }

    public void TestPgStringExtMethod()
    {
        if (flag)
            pgLocalizeStringEvent.SetString("testCommon", "zhaomu");
        else
            pgLocalizeStringEvent.SetString("test_format", "linge");
        flag = !flag;
    }
}
