using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour {

    [SerializeField]
    private string[] m_cheatCodes;

    public string[] CheatCodes { get { return m_cheatCodes; } }

    private void Awake()
    {
        Awakes();
    }

    protected virtual void Awakes()
    {

    }



    public virtual void SetCheatCode(string acceptedCode)
    {

    }

    public virtual void Execute()
    {

    }
}
