using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoodNEvil
{
    BlackGuard = 0,
    Knight = 50,
    Paladin = 100
}

public enum LawNOrder
{
    Chaotic = 0,
    Neutral = 50,
    Lawful = 100
   
}
  
[RequireComponent(typeof(CombatStats))]
public class CharacterStatSheet : MonoBehaviour {

    public static float m_maxHealth = 100;
    public float MaxHealth
    {
        get
        {
            return m_maxHealth;
        }
    }
    private float m_health = m_maxHealth;
    public float Health
    {
        get
        {
            return m_health;
        }

        set
        {
            m_health = value;
        }
    }
    public ArmorBase m_armor;
    //LEAVE THIS BLANK
    public WeaponBase m_ActiveWeapon;
    public WeaponBase m_weapon;
    public CharacterStatSheet m_playerToAttack;
    public AttackStrength m_attackStrength;
    public bool m_knowMagic;
    public MagicAttack[] m_spells;
    public bool m_isDead = false;
    public Animator m_animator;
    public bool m_attacking;
    public UnityEngine.UI.Slider m_healthBar;
    public UnityEngine.UI.Slider m_combatBar;
    private CombatStats m_combatStatistics;
    public bool m_decidedAttack;
    public bool FadeDeath = false;
    public SpriteRenderer m_renderer;
    public float m_fadeSpeed;
    private float[] m_effectTime = new float[(int)eEffects.NumOfEffects];
    private float[] m_effectsToApply = new float[(int)eEffects.NumOfEffects];
    public bool m_vulnerableToFire;
    public bool m_isEvil = false;
    public int m_incapacitationPoints;
    public bool m_burning = false;
    public float m_burnTime;
    public float m_burnTimer;
    public bool m_surrender;
    // Use this for initialization
    void Start() {
        m_combatStatistics = GetComponent<CombatStats>();
        Health = 100;  
        ResetEffects();
    }
	
	// Update is called once per frame
	void Update() {
        if (m_burning)
        {
            m_burnTimer -= Time.deltaTime;
            if (m_burnTimer <= 0)
            {
                Health -= 1 + m_effectsToApply[(int)eEffects.BurnDamage];
                ReCheckHealth();
                if(DeathCheck())
                {
                    TurnBasedScript.CallOnOutsideDeath();
                }
                m_burnTimer = m_burnTime;
            }
        }
	}

    public void Starts()
    {
        Start();
    }

    public void Updates()
    {
        Update();
    }

    public void ResetEffects()
    {
        for(int i = 0; i < m_effectsToApply.Length; i++)
        {
            m_effectsToApply[i] = 0;
            m_effectTime[i] = 0;
        }
        m_burning = false;
    }

    public float[] GetEffectArray()
    {
        return m_effectsToApply;
    }

    public float[] GetEffectTimeArray()
    {
        return m_effectTime;
    }

    public void TakeDamage(float damageToTake, CombatStats attackerCombatStats = null, float bonusInterupt = 1)
    {
        damageToTake += AdditionalDamage();
        //Critical Hit Chance
        if(attackerCombatStats != null)
            damageToTake *= (Random.Range(0, 100) <= (attackerCombatStats.m_dexterity * attackerCombatStats.m_dexterityPercentage)) ? 2 : 1;
        damageToTake -= m_armor.GetDamageReduction();
        m_armor.TookAHit();
        Debug.Log(gameObject.name + " took " + damageToTake.ToString());
        m_health -= damageToTake;
        if(m_combatBar.value > 0.73)
            m_combatBar.GetComponent<CombatSliderScript>().TakeFromTimer((damageToTake / (10 * bonusInterupt)));
    }

    public void HealSelf(float amountToHeal)
    {
        Health += amountToHeal;
    }

    public float AdditionalDamage()
    {
        float temp = 0;
        temp += (m_isEvil && m_effectTime[(int)eEffects.BonusToEvil] > 0) ? m_effectsToApply[(int)eEffects.BonusToEvil] : 0;
        temp += (m_effectTime[(int)eEffects.BonusDamage] > 0) ? m_effectsToApply[(int)eEffects.BonusDamage] : 0;
        return temp;
    }

    public bool DeathCheck()
    {
        if(m_health <= 0)
        {
            m_isDead = true;
            //play death anim
            m_animator.Stop();
            StartFadeDeath();
            return true;
        }
        return false;
    }

    public UnityEngine.UI.Slider GetHealthBar()
    {
        return m_healthBar;
    }

    public UnityEngine.UI.Slider GetCombatBar()
    {
        return m_combatBar;
    }

    public CombatStats GetStatistics()
    {
        return m_combatStatistics;
    }

    public void ReCheckHealth()
    {
        m_healthBar.value = m_health;
    }

    public void GetHitAnim()
    {

    }

    public AnimatorStateInfo GetAnimatorStateInfo()
    {
        return m_animator.GetCurrentAnimatorStateInfo(0);
    }

    public void HitPointOpen()
    {
        m_attacking = true;
    }

    public void HitPointClosed()
    {
        m_attacking = false;
    }

    public void StartFadeDeath()
    {
        FadeDeath = true;
    }
    public void FadingDeath()
    {
        Color buffCol = m_renderer.color;
        buffCol.a = Mathf.Lerp(m_renderer.color.a, 0, m_fadeSpeed * Time.deltaTime);
        m_renderer.color = buffCol;
        if (m_renderer.color.a < 0.001f)
        {
            FadeDeath = false;
            gameObject.SetActive(false);
        }
    }

    public void AddEffect(eEffects effect, float timeInTurns, float effectDamage)
    {
        m_effectsToApply[(int)effect] = effectDamage;
        m_effectTime[(int)effect] = timeInTurns;
    }

    public void UpdateEffects()
    {
        //CheckBurnDamage();
        for (int i = 0; i < m_effectsToApply.Length; i++)
        {
            if(m_effectTime[i] > 0)
            {
                m_effectTime[i]--;
            }
        }

    }

    public bool ChanceOfBurning()
    {
        float chanceofBurning = 50;
        if (m_effectTime[(int)eEffects.AdditionBurnChance] > 0 || m_vulnerableToFire)
            chanceofBurning += 50;
        if (Random.Range(0, 100) <= chanceofBurning)
        {
            return true;
        }
        return false;
    }

    public void CheckBurnDamage()
    {
        if (m_effectTime[(int)eEffects.BurnChance] > 0)
        {
            TakeDamage( 5 + ((m_effectTime[(int)eEffects.BurnDamage] > 0) ? m_effectsToApply[(int)eEffects.BurnDamage] : 0));
            ReCheckHealth();
            if(DeathCheck())
            {

            }
        }
    }
}
