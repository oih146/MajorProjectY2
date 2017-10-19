using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StatusArchetypeCreator  {

    [MenuItem("Assets/Create/New Status Archetype")]
    public static void CreateAsset(MenuCommand command)
    {
        StatusBase asset = StatusBase.CreateInstance<StatusBase>();

        AssetDatabase.CreateAsset(asset, "Assets/Scripts/StatusEffects/Archetypes/NewStatusArchetype.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
