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
            GUI.Label(new Rect(10, 10, 100, 100), "<color=red><size=30>" + m_textSource.GetText() + "</size></color>", style);
        }

    }

    public void IsHovering(bool status)
    {
        m_MouseHover = status;
    }
}
