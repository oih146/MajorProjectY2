using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseConsequences : MonoBehaviour {

    public enum ConsequenceApplication
    {
        OnKill,
        AfterUse
    }

    [Tooltip("Depending on whether the variable is a negative or a positive, it will take away, or give to, the alignment")]
    public Consequence[] m_consequences;

    [System.Serializable]
    public struct Consequence
    {
        public Aliginment m_alignmnent;
        public int m_VariableToApply;
        public ConsequenceApplication m_application;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ApplyConsequences(PlayerStat player, ConsequenceApplication application)
    {
        int addToLight = 0;
        int addToLaw = 0;
        foreach(Consequence conseq in m_consequences)
        {
            if(conseq.m_application == application)
            {
                switch (conseq.m_alignmnent)
                {
                    case Aliginment.Law:
                        addToLaw += conseq.m_VariableToApply;
                        break;
                    case Aliginment.Light:
                        addToLight += conseq.m_VariableToApply;
                        break;
                    default:
                        break;
                }
            }
        }

        if(addToLight != 0)
        {
            player.Light += addToLight;
        }
        if(addToLaw != 0)
        {
            player.Law += addToLaw;
        }
    }

}
