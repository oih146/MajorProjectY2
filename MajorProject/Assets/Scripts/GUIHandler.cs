using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHandler : MonoBehaviour {

    public AbilityArchetype m_textSource;
    public bool m_MouseHover;

    void OnGUI()
    {
        if (m_MouseHover)
        {
            GUIStyle style = new GUIStyle();
            style.richText = true;
            GUI.Label(new Rect(10, 400, 100, 100), m_textSource.GetText(), style);
        }

    }

    public void IsHovering(bool status)
    {
        m_MouseHover = status;
    }
}
