using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScript : MonoBehaviour {

    bool m_attacking;                    //Is this character attacking
    public bool Attacking
    {
        get
        {
            return m_attacking;
        }

        set
        {
            m_attacking = value;
        }
    }

    //For animation events
    //Syncs animation with damage
    public void HitPointOpen()
    {
        Attacking = true;
    }

    //For animation events
    //Syncs animation with damage
    public void HitPointClosed()
    {
        Attacking = false;
    }
}
