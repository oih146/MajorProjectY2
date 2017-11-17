using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour {

    [SerializeField]
    private string[] cheatCode;

    private string m_activationMessage = "Cheat Activated!";

    private int index;

    void Start()
    {
        index = 0;
        foreach(string st in cheatCode)
        {
            st.ToLower();
        }
    }

    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown)
        {
            // Check if the next key in the code is pressed
            if (Input.GetKeyDown(cheatCode[index]))
            {
                // Add 1 to index to check the next key in the code
                index++;
                // If index reaches the length of the cheatCode string, 
                // the entire code was correctly entered
                if (index == cheatCode.Length)
                {
                    // Cheat code successfully inputted!
                    Execute();
                    index = 0;
                }
            }
            // Wrong key entered, we reset code typing
            else
            {
                index = 0;
            }
        }
    }

    protected virtual void ChangeActivationMessage(string newMessage)
    {
        m_activationMessage = newMessage;
    }

    public virtual void Execute()
    {
        CheatTextFlash.Instance.StartLerp(m_activationMessage);
    }
}
