using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BtnText1 : MonoBehaviour
{
    public string key = "Login";
    public TextMeshProUGUI text1;

    public void TestBtnText1()
    {
        string txt = LocaleManager.Instance.GetString(key);
        text1.text = txt;
    }
}
