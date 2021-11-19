using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class PgLocalizeStringEvent : LocalizeStringEvent
{
    LocalizedString originStringRef;
    private void Awake()
    {
        originStringRef = StringReference;
    }
    protected virtual void UpdateString(string value)
    {
        base.UpdateString(value);
        StringReference = originStringRef;
    }
}
