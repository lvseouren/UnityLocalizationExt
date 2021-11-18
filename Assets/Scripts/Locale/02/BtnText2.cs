using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BtnText2 : MonoBehaviour
{
    public string key = "key1";
    public TextMeshProUGUI text1;

    public void TestBtnText1()
    {
        string txt = LocaleManager.Instance.GetString(key);
        text1.text = txt;
    }
}
