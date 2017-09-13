using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterStatSheet {

    public override float Health
    {
        get
        {
            return base.Health;
        }

        set
        {
            base.Health = value;
            if (Health <= 0)
                StartFadeDeath();
        }
    }
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
                Vector3 temp = gameObject.transform.position;
                temp.y += 4;
                temp.x += 2;
                GameObject obj = Instantiate(m_notificationBox, Camera.main.WorldToScreenPoint(temp), Quaternion.identity, GetPersonalCanvas().transform);
                obj.GetComponent<UnityEngine.UI.Text>().text = "I Surrender!";
                m_surrender = true;
                GetCombatBar().m_combatActive = false;
                GetCombatBar().enabled = false;
                GetCombatBar().m_combatSlider.value = 0;
                TurnBasedScript.CallOnPlayerSurrender(this);

            }
        }
    }

    // Use this for initialization
    void Start()
    {
        Starts();
        m_maxIP += GetStatistics().GetWillPowerIPIncrease();
        m_renderer = GetComponent<SpriteRenderer>();
        //fadeDeathLerping.StartLerp(2, m_renderer.color.a, 0);
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
    public override float TakeDamage(float damageToTake, CombatStats attackerCombatStats, float bonusInterupt, AttackStrength attackStrength, bool interrupt = true)
    {
        //Regardless of how much damage is done, IP is gained
        Debug.Log("IP Gained");
        IncapacitationPoints += IPonHit;

        return base.TakeDamage(damageToTake, attackerCombatStats, bonusInterupt, attackStrength, interrupt);
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
