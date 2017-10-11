using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ArchetypeCreator
{
    [MenuItem("Assets/Create/New Ability Archetype")]
    public static void CreateAsset()
    {
        AbilityArchetype asset = AbilityArchetype.CreateInstance<AbilityArchetype>();

        AssetDatabase.CreateAsset(asset, "Assets/Scripts/AbilityArchetypes/NewAbilityArchetype.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
