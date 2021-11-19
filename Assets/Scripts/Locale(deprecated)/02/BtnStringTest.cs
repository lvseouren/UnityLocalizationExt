using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BtnStringTest : MonoBehaviour
{
    // Start is called before the first frame update
    public string str = "Login";
    public TextMeshProUGUI textPro;
    public void Test()
    {
        string test = LocaleStringCollectionManager.Instance.GetLocaleString(str);
        textPro.text = test;
    }
}
