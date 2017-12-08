using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour {

    [SerializeField]
    private UnityEngine.UI.InputField m_inputField;

    [SerializeField]
    private Cheat[] m_cheats;

    private string m_activationMessage = "Cheat Activated!";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            m_inputField.gameObject.SetActive(!m_inputField.gameObject.activeInHierarchy);
            Time.timeScale = (Time.timeScale == 0 ? 1 : 0);
            if(!m_inputField.gameObject.activeInHierarchy)
            {
                CheckCheats();
            }
            else
            {
                m_inputField.text = "";
            }
        }
	}

    protected virtual void ChangeActivationMessage(string newMessage)
    {
        m_activationMessage = newMessage;
    }

    public void CheckCheats()
    {
        foreach(Cheat cheatItem in m_cheats)
        {
            foreach (string cheatCode in cheatItem.CheatCodes)
            {
                if (cheatCode == m_inputField.text)
                {
                    cheatItem.SetCheatCode(m_inputField.text);
                    cheatItem.Execute();
                    CheatTextFlash.Instance.StartLerp(m_activationMessage);
                }
            }
        }
    }
}
