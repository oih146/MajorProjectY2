using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToolTipDisplay : MonoBehaviour {

    private UnityEngine.UI.Text m_displayText;

    void OnEnable()
    {
        GUIHandler.ToolTipEnableEvent += DisplayToolTip;
        GUIHandler.ToolTipDisableEvent += EraseToolTip;
    }

    void OnDisable()
    {
        GUIHandler.ToolTipEnableEvent -= DisplayToolTip;
        GUIHandler.ToolTipDisableEvent -= EraseToolTip;
    }

	// Use this for initialization
	void Start () {
        m_displayText = GetComponent<UnityEngine.UI.Text>();
        m_displayText.text = string.Empty;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisplayToolTip(string Text)
    {
        m_displayText.text = Text;
    }

    public void EraseToolTip()
    {
        m_displayText.text = string.Empty;
    }

    
}
