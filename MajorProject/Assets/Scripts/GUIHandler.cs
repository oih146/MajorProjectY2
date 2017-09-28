using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHandler : MonoBehaviour {

    public AbilityArchetype m_textSource;
    public bool m_MouseHover;
    public Font m_font;

    void OnGUI()
    {
        if (m_MouseHover)
        {
            GUIStyle style = new GUIStyle();
            style.richText = true;
            style.font = m_font;
            GUI.Label(new Rect(10, 10, 100, 100), "<color=red><size=30>" + m_textSource.GetText() + "</size></color>", style);
        }

    }

    public void IsHovering(bool status)
    {
        if (TurnBasedScript.Instance.BattleActive == true)
            m_MouseHover = status;
        else
            m_MouseHover = false;
    }
}
