using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHandler : MonoBehaviour {

    public delegate void OnToolTipActive(string text);
    public static OnToolTipActive ToolTipEnableEvent;

    public delegate void OnToolTipDisable();
    public static OnToolTipDisable ToolTipDisableEvent;

    public AbilityArchetype m_textSource;
    //public bool m_MouseHover;
    //public Font m_font;


    //void OnGUI()
    //{
    //    if (m_MouseHover)
    //    {
    //        GUIStyle style = new GUIStyle();
    //        style.richText = true;
    //        style.font = m_font;
    //        GUI.Label(new Rect(10, 10, 100, 100), "<color=red><size=30>" + m_textSource.GetText() + "</size></color>", style);
    //    }

    //}

    public void IsHovering(bool status)
    {
        if (TurnBasedScript.Instance.BattleActive == true)
        {
            if (status)
                ToolTipEnableEvent(m_textSource.GetText());
            else
                ToolTipDisableEvent();
            //m_MouseHover = status;
        }
        else
        {
            ToolTipDisableEvent();
            //m_MouseHover = false;
        }
    }
}
