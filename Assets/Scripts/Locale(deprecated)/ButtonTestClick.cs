using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTestClick : MonoBehaviour
{
    string str = "LoginStringCollection/Login";
    public LocaleStringBehaviour ll;
    public void TestClick()
    {
        ll.UpdateLocaleString(str);
    }
}
