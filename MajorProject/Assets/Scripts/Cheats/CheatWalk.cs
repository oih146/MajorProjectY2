using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatWalk : Cheat {

    private PlayerMovement.EWalk m_walkType;

    protected override void Awakes()
    {
        if(CheatCodes.Length != 3)
        {
            Debug.Log("Error! CheatWalk doesn't have 3 cheat codes");
        }
    }

    public override void SetCheatCode(string acceptedCode)
    {
        if(acceptedCode == CheatCodes[0])
        {
            m_walkType = PlayerMovement.EWalk.Normal;
        } else if (acceptedCode == CheatCodes[1])
        {
            m_walkType = PlayerMovement.EWalk.Chicken;
        } else if (acceptedCode == CheatCodes[2])
        {
            m_walkType = PlayerMovement.EWalk.Crab;
        }
    }

    public override void Execute()
    {
        PlayerMovement.Instance.SwitchWalk(m_walkType);
    }

}
