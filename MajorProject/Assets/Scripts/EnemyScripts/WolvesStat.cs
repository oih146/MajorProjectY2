using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolvesStat : EnemyBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public override void DecideTarget(CharacterStatSheet[] possibleTargets)
    {
        base.DecideTarget(possibleTargets);
    }

    public override void DecideAttack()
    {
        int m_chanceForHowl = 0;
        if(m_playerToAttack.m_decidedAttack && m_playerToAttack.m_decidedTarget)
        {
            m_chanceForHowl = 30;
        }

        if (Random.Range(0, 100) <= m_chanceForHowl)
        {
            m_ActiveWeapon = m_abilities[0];
        }
        else
        {
            int attackStrength;
            //Deciding attack charge time and damage
            int attackDamage = 0;
            attackStrength = Random.Range(0, 3);
            switch (attackStrength)
            {
                case 0:
                    attackStrength = 3;
                    attackDamage = 15;
                    break;
                case 1:
                    attackStrength = 5;
                    attackDamage = 25;
                    break;
                case 2:
                    attackStrength = 8;
                    attackDamage = 35;
                    break;
                default:
                    attackStrength = 3;
                    attackDamage = 15;
                    break;
            }
            //using melee
            m_weapon.m_damageSet = AttackDamage.Custom;
            m_weapon.m_chargeTime = (ChargeTime)attackStrength;
            m_weapon.SetAttackDamage = attackDamage;
            m_ActiveWeapon = m_weapon;
        }
        //decide enemy attackStrength
        GetCombatBar().SlowDown((float)m_ActiveWeapon.m_chargeTime);
        m_attackCharge = m_ActiveWeapon.m_chargeTime;
        switch (m_ActiveWeapon.m_chargeTime)
        {
            case ChargeTime.Light:
                GetCombatBar().SetPortraitBackgroundColor(TurnBasedScript.Instance.m_attackColors[(int)eAttackColors.Light]);
                break;
            case ChargeTime.Normal:
                GetCombatBar().SetPortraitBackgroundColor(TurnBasedScript.Instance.m_attackColors[(int)eAttackColors.Medium]);
                break;
            case ChargeTime.Heavy:
                GetCombatBar().SetPortraitBackgroundColor(TurnBasedScript.Instance.m_attackColors[(int)eAttackColors.Heavy]);
                break;
            case ChargeTime.Magic:
                GetCombatBar().SetPortraitBackgroundColor(TurnBasedScript.Instance.m_attackColors[(int)eAttackColors.Magic]);
                break;
            default:
                GetCombatBar().SetPortraitBackgroundColor(TurnBasedScript.Instance.m_attackColors[(int)eAttackColors.Light]);
                break;
        }
        m_decidedAttack = true;
    }
}
