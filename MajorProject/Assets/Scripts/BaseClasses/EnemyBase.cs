using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterStatSheet {

    //Public Variables
    //----------------------------------
    public bool isCrippled;
    public bool m_vulnerableToFire;         // Mostly for wolves, may move
    public float m_incapacitationPoints;    //How many incap point enemy has
    public int m_baseCapIP;                 //Base IP Limit, without willpower added. Added to m_maxIP with willpower for total IP limit

    //Static Variables
    //----------------------------------
    public static float IPonHit;

    //Private Variables
    //----------------------------------
    private int m_maxIP = 50;

    public float IncapacitationPoints
    {
        get
        {
            return m_incapacitationPoints;
        }

        set
        {
            m_incapacitationPoints = value;
            if (m_incapacitationPoints > m_maxIP && !m_surrender)
            {
                Debug.Log(gameObject.name + " Surrendered");
                //Vector3 temp = gameObject.transform.position;
                //temp.y += 4;
                //temp.x += 2;
                //GameObject obj = Instantiate(m_notificationBox, Camera.main.WorldToScreenPoint(temp), Quaternion.identity, GetPersonalCanvas().transform);
                //obj.GetComponent<UnityEngine.UI.Text>().text = "I Surrender!";
                Surrendered = true;
                GetCombatBar().m_combatActive = false;
                GetCombatBar().enabled = false;
                GetCombatBar().m_combatSlider.value = 0;
                TurnBasedScript.CallOnPlayerSurrender(this);
            } else if(m_incapacitationPoints < m_maxIP && m_surrender)
            {
                Surrendered = false;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        Starts();

        //fadeDeathLerping.StartLerp(2, m_renderer.color.a, 0);
    }

    public override void Starts()
    {
        base.Starts();
        m_maxIP += GetStatistics().GetWillPowerIPIncrease();
        m_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Updates();
        if (FadeDeath)
            FadingDeath();
    }

    //Override Functions
    //-------------------------------------------------------------------------
    //Applies IP when Enemies take damage
    public override float TakeDamage(float damageToTake, CombatStats attackerCombatStats, float bonusInterupt, ChargeTime attackStrength, bool interrupt = true)
    {
        //Regardless of how much damage is done, IP is gained
        Debug.Log("IP Gained");
        IncapacitationPoints += IPonHit + (GetEffectArray()[(int)eEffects.BonusIncapacitationPoints].IsActive ? GetEffectArray()[(int)eEffects.BonusIncapacitationPoints].Strength : 0);

        return base.TakeDamage(damageToTake, attackerCombatStats, bonusInterupt, attackStrength, interrupt);
    }






    //Virtual Functions
    //------------------------------------------------------------------------------
    public virtual void DecideTarget(CharacterStatSheet[] possibleTargets)
    {
        int playerToAttack = Random.Range(0, possibleTargets.Length - 1);
        while (possibleTargets[playerToAttack] == null)
        {
            playerToAttack = Random.Range(0, possibleTargets.Length - 1);
        }
        m_playerToAttack = possibleTargets[playerToAttack];

        m_decidedTarget = true;
    }

    public virtual void DecideAttack()
    {
        int attackStrength;
        //decided whether to use magic or not
        if (Random.Range(0, 1) >= 0.5f && m_knowMagic == true)
        {
            //using magic
            int spellIndex = Random.Range(0, m_spells.Length);
            m_ActiveWeapon = m_spells[spellIndex];
            attackStrength = (int)m_ActiveWeapon.m_chargeTime;
        }
        else
        {
            //not using magic
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
        GetCombatBar().SlowDown(attackStrength);
        m_attackCharge = (ChargeTime)attackStrength;
        switch ((ChargeTime)attackStrength)
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
