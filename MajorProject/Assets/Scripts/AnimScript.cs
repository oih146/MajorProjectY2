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

    bool m_weaponEffect;
    public bool WeaponEffect { get { return m_weaponEffect; } set { m_weaponEffect = value; } }

    bool m_attackFinished;
    public bool AttackFinished { get { return m_attackFinished; } set { m_attackFinished = value; } }

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

    public void WeapEffectPlay()
    {
        WeaponEffect = true;
    }

    public void WeaponEffectClosed()
    {
        WeaponEffect = false;
    }

    public void AttackDone()
    {
        AttackFinished = true;
    }

    public void AttackReset()
    {
        AttackFinished = false;
    }

    public void ResetVariables()
    {
        AttackFinished = false;
        WeaponEffect = false;
        Attacking = false;
    }
}
