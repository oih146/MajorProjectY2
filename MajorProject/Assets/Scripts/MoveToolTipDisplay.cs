using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToolTipDisplay : MonoBehaviour {

    public delegate void OnToolTipActive(string text);
    public static OnToolTipActive ToolTipEnableEvent;

    public delegate void OnToolTipDisable();
    public static OnToolTipDisable ToolTipDisableEvent;


    public UnityEngine.UI.Image m_toolTipDisplay;

    private UnityEngine.UI.Text m_displayText;
    private string m_nameToShow;
    private string m_stringToShow;
    public float m_timeTillShow;
    private float m_timer;
    private bool m_timing = false;

    void OnEnable()
    {
        ToolTipEnableEvent += DisplayToolTip;
        ToolTipDisableEvent += EraseToolTip;
    }

    void OnDisable()
    {
        ToolTipEnableEvent -= DisplayToolTip;
        ToolTipDisableEvent -= EraseToolTip;
    }

	// Use this for initialization
	void Start () {
        m_displayText = GetComponent<UnityEngine.UI.Text>();
        m_displayText.text = string.Empty;
        m_toolTipDisplay.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_timing)
        {
            m_timer -= Time.deltaTime;
            if(m_timer <= 0)
            {
                m_toolTipDisplay.enabled = true;
                m_displayText.text = m_stringToShow;
                m_timing = false;
            }
        }
	}

    public void DisplayToolTip(string Text)
    {
        m_timer = m_timeTillShow;
        m_timing = true;
        m_stringToShow = Text;
    }

    public void EraseToolTip()
    {
        m_timing = false;

        m_displayText.text = string.Empty;
        m_toolTipDisplay.enabled = false;
    }

    
}
