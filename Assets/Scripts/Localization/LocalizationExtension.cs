using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

public static class LocalizationExtension
{
    public static void SetString(this LocalizeStringEvent target, string key, params object[] args)
    {
        target.StringReference.TableEntryReference = key;

    }
}
