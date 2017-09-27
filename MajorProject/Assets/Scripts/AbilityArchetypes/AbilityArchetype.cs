using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityArchetype : ScriptableObject {

    [TextArea(3, 10)]
    [SerializeField]
    string m_text;


    public string GetText()
    {
        return m_text;
    }
}
