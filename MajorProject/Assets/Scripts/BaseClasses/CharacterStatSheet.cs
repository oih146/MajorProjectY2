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

    //Health Variables
    //-------------------------------------------
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

    //Combat Variables
    //-------------------------------------------
        //Armour
    public ArmorBase m_armor;                   //Armour, reduces incoming damage, wears down

        //Attributes
    private CombatStats m_combatStatistics;     //Holds combat attributes
    private int m_baseDamage;                   //Holder for strength, not used may delete
    public int BaseDamage
    {
        get
        {
            return m_baseDamage;
        }
        set
        {
            m_baseDamage = value;
        }
    }
    private float m_criticalChance;             //Critical Hit chance, not used may delete
    public float CriticalChance
    {
        get
        {
            return m_criticalChance;
        }
        set
        {
            m_criticalChance = value;
        }
    }
    public static int m_InteruptMultiplier;     //Interrupt modifier, how much defender is knocked back while casting, * by bonus interrupt (usually 1)

        //Weapons
    public WeaponBase m_ActiveWeapon;           //LEAVE THIS BLANK IN INSPECTOR, Holds the weapon or spell to use in combat, changes
    public WeaponBase m_weapon;                 //Holds melee weapon, may change type to Sword
    public CharacterStatSheet m_playerToAttack; //Character that will be attacked during combat
    public AttackStrength m_attackStrength;     //Strength of attack, decides how slow attack will be to charge
    public bool m_knowMagic;                    //Does this character know magic
    public MagicAttack[] m_spells;              //Magic known by this character
    public bool m_isDead = false;               //Is this character dead, mostly for combat
    public Animator m_animator;                 //Animator, holds animations

        //What Character is in Combat
    public bool m_attacking;                    //Is this character attacking
    public bool m_decidedAttack;                //Has this player decided to attack yet
    public bool m_isEvil = false;               //Does this character take bonus holy damage
    public bool m_burning = false;              //Is this character currently on fire
    public float m_burnTime;                    //How long intervals are in character burning (in seconds), may change
    public float m_burnTimer;                   //Timer for how long till character burns again, may change
    public bool m_disarmed;

    //UI
    public Canvas m_playerCanvas;
    public UnityEngine.UI.Slider m_healthBar;   //Health bar attached to this character
    public CombatSliderScript m_combatBar;   //This character's combat bar that is shown during combat
    public GameObject m_notificationBox;

    //Effect and Abilities (relevent enums decide where variables are allocated)
    private float[] m_effectTime = new float[(int)eEffects.NumOfEffects];           //Effects time
    private float[] m_effectsToApply = new float[(int)eEffects.NumOfEffects];       //Effects strength (damage)

    //Death Variables
    //-----------------------------------------
    public bool FadeDeath = false;              //For character death, does this character fade out in death
    public SpriteRenderer m_renderer;           //Sprite render, used to fade death
    public float m_fadeSpeed;                   //Speed at which character fades when dying

    // Use this for initialization
    void Start() {
        m_combatStatistics = GetComponent<CombatStats>();
        Health = 100;
        GenerateStatistics();
        ResetEffects();
    }
	
	// Update is called once per frame
	void Update() {
        if (m_burning)
        {
            m_burnTimer -= Time.deltaTime;
            if (m_burnTimer % 1.0f == 0.0f)
            {
                Health -= 1 + m_effectsToApply[(int)eEffects.BurnDamage];
                ReCheckHealth();
                if(DeathCheck())
                {
                    TurnBasedScript.CallOnOutsideDeath();
                    m_burning = false;
                }
                //m_burnTimer = m_burnTime;
            }
        }
	}

    //To allow Start to be run by inheriting classes
    public void Starts()
    {
        Start();
    }

    //To allow Update to be run by inheriting classes
    public void Updates()
    {
        Update();
    }

    //Creates statistics for attributes 
    public void GenerateStatistics()
    {
        GetCombatBar().DecreaseBaseSliderSpeed(GetStatistics().GetSpeed());
        BaseDamage = GetStatistics().GetStrength();
        CriticalChance = GetStatistics().GetDexterity();
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

    //Used by attacker
    //If the defending character has a counterattack ability
    public float CounterTakeDamage(float damageToTake)
    {
        Debug.Log(gameObject.name + " took Counter Damage: " + damageToTake);
        return Health - damageToTake;
    }

    //Take damage, virtual to allow inheriting classes to override
    public virtual float TakeDamage(float damageToTake, CombatStats attackerCombatStats, float bonusInterupt, AttackStrength attacktype, bool interrupt = true)
    {
        //Critical Hit Chance
        if (attackerCombatStats != null)
        {
            damageToTake += attackerCombatStats.GetStrength();
            damageToTake *= (Random.Range(0, 100) <= attackerCombatStats.GetDexterity()) ? 2 : 1;
        }
        if(attacktype != AttackStrength.Magic)
            damageToTake -= m_armor.GetDamageReduction(((int)m_effectTime[(int)eEffects.DamageReduction] > 0) ? (int)m_effectsToApply[(int)eEffects.DamageReduction] : 0);
        m_armor.TookAHit();
        //Damage can't be less than zero
        //Would be adding to health
        if (damageToTake < 0)
            damageToTake = 0;
        Debug.Log(gameObject.name + " took " + damageToTake.ToString());
        m_health -= damageToTake;
        //Combat bar interrupt
        if(m_combatBar.m_combatSlider.value > 0.73 && interrupt)
            m_combatBar.TakeFromTimer(damageToTake / (m_InteruptMultiplier * bonusInterupt));
        if (m_effectTime[(int)eEffects.CounterStance] > 0)
            return m_effectsToApply[(int)eEffects.CounterStance];
        return 0;
    }

    //If the weapon allows player to heal
    public void HealSelf(float amountToHeal)
    {
        Health += amountToHeal;
    }

    //Additional Damage
    //Holy and Bonus
    public float AdditionalDamage()
    {
        float temp = 0;
        temp += (m_isEvil && m_effectTime[(int)eEffects.BonusToEvil] > 0) ? m_effectsToApply[(int)eEffects.BonusToEvil] : 0;
        temp += (m_effectTime[(int)eEffects.BonusDamage] > 0) ? m_effectsToApply[(int)eEffects.BonusDamage] : 0;
        return temp;
    }

    //Check if this character is dead
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

    public CombatSliderScript GetCombatBar()
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

    //Gets the current animation playing
    public AnimatorStateInfo GetAnimatorStateInfo()
    {
        return m_animator.GetCurrentAnimatorStateInfo(0);
    }

    //For animation events
    //Syncs animation with damage
    public void HitPointOpen()
    {
        m_attacking = true;
    }

    //For animation events
    //Syncs animation with damage
    public void HitPointClosed()
    {
        m_attacking = false;
    }

    public void StartFadeDeath()
    {
        FadeDeath = true;
    }

    //Simply turns current sprite alpha down
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

    //Simplifier for adding Effects
    public void AddEffect(WeaponBase.WeaponEffect effect)
    {
        m_effectsToApply[(int)effect.effectType] = effect.effectDamage;
        m_effectTime[(int)effect.effectType] = effect.effectTime;
    }

    //Called at the end of every turn during combat, updates effects
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
        if (m_effectTime[(int)eEffects.SpeedReduction] > 0)
            GetCombatBar().SetTemporarySpeedDecrease(m_effectsToApply[(int)eEffects.SpeedReduction]);

    }

    //Calculate if character takes burning damage or not
    public virtual bool ChanceOfBurning()
    {
        float chanceofBurning = 50;
        if (m_effectTime[(int)eEffects.AdditionBurnChance] > 0)
            chanceofBurning += 50;
        if (Random.Range(0, 100) <= chanceofBurning)
        {
            return true;
        }
        return false;
    }

    public Canvas GetPersonalCanvas()
    {
        return m_playerCanvas;
    }
}
