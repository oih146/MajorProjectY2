using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterStatSheet {

    //Public Variables
    //----------------------------------
    public bool isCrippled;
    public bool m_vulnerableToFire;         // Mostly for wolves, may move
    public float m_incapacitationPoints;    //How many incap point enemy has
    public bool m_surrender;                //Has the enemy surrendered
    public int m_baseCapIP;                 //Base IP Limit, without willpower added. Added to m_maxIP with willpower for total IP limit


    //Static Variables
    //----------------------------------
    public static float IPonHit;

    //Private Variables
    //----------------------------------
    private int m_maxIP;

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
                m_surrender = true;
                TurnBasedScript.CallOnPlayerSurrender(this);
                GetCombatBar().m_combatActive = false;
                GetCombatBar().m_combatSlider.value = 0;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        Starts();
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
    public override float TakeDamage(float damageToTake, CombatStats attackerCombatStats, float bonusInterupt, bool interrupt = true)
    {
        //Regardless of how much damage is done, IP is gained
        IncapacitationPoints += IPonHit;

        return base.TakeDamage(damageToTake, attackerCombatStats, bonusInterupt, interrupt);
    }

    //Adds vulnerableToFire bool, for wolves
    public override bool ChanceOfBurning()
    {
        float chanceofBurning = 50;
        if (GetEffectTimeArray()[(int)eEffects.AdditionBurnChance] > 0 || m_vulnerableToFire)
            chanceofBurning += 50;
        if (Random.Range(0, 100) <= chanceofBurning)
        {
            return true;
        }
        return false;
    }
}
