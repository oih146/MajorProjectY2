using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Button))]
public class NewBehaviourScript : Editor {

    public override void OnInspectorGUI()
    {
        Button myButton = (Button)target;

        EditorGUILayout.LabelField("Current Menu:", SceneManager.GetActiveScene().name);
        myButton.buttonType = (Buttons)EditorGUILayout.EnumPopup("Button Type", myButton.buttonType);
        myButton.transType = (Transitions)EditorGUILayout.EnumPopup("Transition Type", myButton.transType);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Transition Options");
        switch (myButton.transType)
        {
            case Transitions.Pulse:
                myButton.speed = EditorGUILayout.FloatField("Pulse Speed" ,myButton.speed);
                myButton.difference = EditorGUILayout.Slider("Size of Pulse", myButton.difference, 0.1f, 0.5f);
                break;
            case Transitions.ColorTransition:
                break;
        }
    }
}
 