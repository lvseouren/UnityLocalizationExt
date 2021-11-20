using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

[InitializeOnLoad]
public static class PgLocalizeComponent_UGUI
{
    [MenuItem("CONTEXT/Text/PgLocalize")]
    static void LocalizeUIText(MenuCommand command)
    {
        var target = command.context as Text;
        SetupForLocalization(target);
    }

    public static MonoBehaviour SetupForLocalization(Text target)
    {
        var comp = Undo.AddComponent(target.gameObject, typeof(PgLocalizeStringEvent)) as PgLocalizeStringEvent;
        var setStringMethod = target.GetType().GetProperty("text").GetSetMethod();
        var methodDelegate = System.Delegate.CreateDelegate(typeof(UnityAction<string>), target, setStringMethod) as UnityAction<string>;
        UnityEditor.Events.UnityEventTools.AddPersistentListener(comp.OnUpdateString, methodDelegate);
        comp.OnUpdateString.SetPersistentListenerState(0, UnityEventCallState.EditorAndRuntime);

        const int kMatchThreshold = 5;
        var foundKey = LocalizationEditorSettings.FindSimilarKey(target.text);
        if (foundKey.collection != null && foundKey.matchDistance < kMatchThreshold)
        {
            comp.StringReference.TableEntryReference = foundKey.entry.Id;
            comp.StringReference.TableReference = foundKey.collection.TableCollectionNameReference;
        }

        return comp;
    }
}
