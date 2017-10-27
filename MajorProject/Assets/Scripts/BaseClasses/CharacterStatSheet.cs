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
    public virtual float Health
    {
        get
        {
            return m_health;
        }

        set
        {
            m_health = value;
            //if (m_health <= 0)
            //StartFadeDeath();
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
    [HideInInspector]
    public WeaponBase m_ActiveWeapon;           //LEAVE THIS BLANK IN INSPECTOR, Holds the weapon or spell to use in combat, changes
    public SwordScript m_weapon;                 //Holds melee weapon, may change type to Sword
    public CharacterStatSheet m_playerToAttack; //Character that will be attacked during combat
    public ChargeTime m_attackCharge;     //Strength of attack, decides how slow attack will be to charge
    public bool m_knowMagic;                    //Does this character know magic
    public MagicAttack[] m_spells;              //Magic known by this character
    public MagicAttack[] m_abilities;
    public bool m_isDead = false;               //Is this character dead, mostly for combat
    public Animator m_animator;                 //Animator, holds animations

    //What Character is in Combat
    public AnimScript m_animScript;
    public bool m_decidedTarget;
    public bool m_decidedAttack;                //Has this player decided to attack yet
    public bool m_isEvil = false;               //Does this character take bonus holy damage
    public bool m_burning = false;              //Is this character currently on fire
    public bool Burning
    {
        get
        {
            return m_burning;
        }

        set
        {
            m_burning = value;
            m_statusBar.GetBurned().SetActive(value);
        }
    }
    [HideInInspector]
    public float m_burnTime;                    //How long intervals are in character burning (in seconds), may change
    [HideInInspector]
    public float m_burnTimer;                   //Timer for how long till character burns again, may change
    [HideInInspector]
    public int m_burnTimes;                     //Amount of times this character has been burned
    public bool m_disarmed;
    public bool Disarmed
    {
        get
        {
            return m_disarmed;
        }

        set
        {
            m_disarmed = value;
            m_statusBar.GetDisarmed().SetActive(value);
        }
    }
    public bool m_surrender;                //Has this player surrendered
    public bool Surrendered
    {
        get
        {
            return m_surrender;
        }

        set
        {
            m_surrender = value;
            m_statusBar.GetSurrendered().SetActive(value);
        }
    }

    //UI
    public Canvas m_playerCanvas;
    public UnityEngine.UI.Slider m_healthBar;   //Health bar attached to this character
    public CombatSliderScript m_combatBar;   //This character's combat bar that is shown during combat
    //public GameObject m_notificationBox;
    public StatusBar m_statusBar;

    //Effect and Abilities (relevent enums decide where variables are allocated)
    private StatusBase[] m_statusEffects = new StatusBase[(int)eEffects.NumOfEffects];       //Effects strength (damage)

    //Death Variables
    //-----------------------------------------
    public bool FadeDeath = false;              //For character death, does this character fade out in death
    // Use this for initialization
    void Start() {
        Starts();
    }

    // Update is called once per frame
    void Update() {
        Updates();
    }

    //To allow Start to be run by inheriting classes
    public virtual void Starts()
    {
        m_combatStatistics = GetComponent<CombatStats>();
        Health = 100;
        GenerateStatistics();
        ResetEffects();
    }

    //To allow Update to be run by inheriting classes
    public virtual void Updates()
    {

    }

    //Creates statistics for attributes 
    public void GenerateStatistics()
    {
        GetCombatBar().DecreaseBaseSliderSpeed(GetStatistics().GetSpeed());
        BaseDamage = GetStatistics().GetStrength();
        CriticalChance = GetStatistics().GetDexterity();
    }

    public virtual void ResetEffects()
    {
        for (int i = 0; i < GetEffectArray().Length; i++)
        {
            //Destroy(GetEffectArray()[i]);
            GetEffectArray()[i] = (StatusBase)ScriptableObject.CreateInstance("StatusBase");
        }
        Burning = false;
    }

    public StatusBase[] GetEffectArray()
    {
        return m_statusEffects;
    }

    //Used by attacker
    //If the defending character has a counterattack ability
    public IEnumerator CounterTakeDamage(float damageToTake)
    {
        if (damageToTake > 0)
        {
            Debug.Log(gameObject.name + " took Counter Damage: " + damageToTake);
            //m_playerToAttack.m_animator.Play();
            //yield return new WaitUntil(() => m_playerToAttack.m_animScript.Attacking);
            Health -= damageToTake;
            ReCheckHealth();
            yield return new WaitForSeconds(0.0f);
        }
    }

    //Take damage, virtual to allow inheriting classes to override
    public virtual float TakeDamage(float damageToTake, CombatStats attackerCombatStats, float bonusInterupt, ChargeTime attacktype, bool interrupt = true)
    {
        //Critical Hit Chance
        if (attackerCombatStats != null && damageToTake > 0)
        {
            damageToTake += attackerCombatStats.GetStrength();
            damageToTake *= (
                                Random.Range(0, 100) <= attackerCombatStats.GetDexterity() + 
                                (GetEffectArray()[(int)eEffects.DexterityIncrease].IsActive ? GetEffectArray()[(int)eEffects.DexterityIncrease].Strength : 0)
                                ) ? 1.5f : 1;
        }
        if (attacktype != ChargeTime.Magic)
            damageToTake -= m_armor.GetDamageReduction((GetEffectArray()[(int)eEffects.DamageReduction].IsActive) ? (int)GetEffectArray()[(int)eEffects.DamageReduction].Strength : 0);
        m_armor.TookAHit();
        //Damage can't be less than zero
        //Would be adding to health
        if (damageToTake < 0)
            damageToTake = 0;
        Debug.Log(gameObject.name + " took " + damageToTake.ToString());
        StartCoroutine(TakeHit(damageToTake));
        //Combat bar interrupt
        if (m_combatBar.m_combatSlider.value > 0.73)
        {
            float delay = (damageToTake + (m_InteruptMultiplier * bonusInterupt) + (GetEffectArray()[(int)eEffects.TakeBonusInterupt].IsActive ? GetEffectArray()[(int)eEffects.TakeBonusInterupt].Strength : 0)) / 17.5f;
            m_combatBar.TakeFromTimer(delay);
            if (GetEffectArray()[(int)eEffects.CounterStance].IsActive)
            {
                GetEffectArray()[(int)eEffects.CounterStance].Use(this);
                CounterAttack attack = new CounterAttack();
                attack.SecondaryUse(this);

                return GetEffectArray()[(int)eEffects.CounterStance].Strength;
            } else if(m_ActiveWeapon.GetType().ToString() == "SoulRipAttack")
            {
                SoulRipAttack attack = (SoulRipAttack)m_ActiveWeapon;
                attack.TakeFromTimer(m_ActiveWeapon.m_animEffect.m_animator, delay);
            }
        }

        return 0;
    }

    IEnumerator TakeHit(float damageToTake)
    {
        m_animator.Play("Hit");
        yield return new WaitUntil(() => GetAnimScript().TakeHit);
        GetAnimScript().ResetHit();
        Health -= damageToTake;
        ReCheckHealth();
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
        temp += (m_isEvil && GetEffectArray()[(int)eEffects.BonusToEvil].IsActive) ? GetEffectArray()[(int)eEffects.BonusToEvil].Strength : 0;
        temp += (GetEffectArray()[(int)eEffects.BonusDamage].IsActive) ? GetEffectArray()[(int)eEffects.BonusDamage].Strength : 0;
        return temp;
    }

    //Check if this character is dead
    public bool DeathCheck()
    {
        if (m_health <= 0)
        {
            m_isDead = true;
            //play death anim
            m_animator.Stop();
            //StartFadeDeath();
            return true;
        }
        return false;
    }

    public UnityEngine.UI.Slider GetHealthBar()
    {
        return m_healthBar;
    }

    public StatusBar GetStatusBar()
    {
        return m_statusBar;
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

    public AnimScript GetAnimScript()
    {
        return m_animScript;
    }

    public void StartFadeDeath()
    {
        m_animator.Play("Death");
        //gameObject.SetActive(false);
        //FadeDeath = true;
    }

    //Simplifier for adding Effects
    public void AddEffect(StatusBase statVars)
    {
        statVars.OnApply(this);

    }

    //Called at the end of every turn during combat, updates effects
    public virtual void UpdateEffects()
    {
        //CheckBurnDamage();
        for (int i = 0; i < GetEffectArray().Length; i++)
        {
            if (GetEffectArray()[i].TakeAndCheckActive())
            {
                GetEffectArray()[i].Remove(this);
            }
            else
            {
                GetEffectArray()[i].OnUpdate(this);
            }
        }
        //if (GetEffectArray()[(int)eEffects.SpeedReduction].IsActive)
        //    GetCombatBar().SetTemporarySpeedDecrease(GetEffectArray()[(int)eEffects.SpeedReduction].Strength);
        //if (Burning && GetEffectArray()[(int)eEffects.BurnDamage].IsActive)
        //{
        //    Health -= GetEffectArray()[(int)eEffects.BurnDamage].Strength;
        //    ReCheckHealth();
        //    if (GetEffectArray()[(int)eEffects.BurnDamage].IsActive)
        //        Burning = false;
        //    if (DeathCheck())
        //        TurnBasedScript.CallOnOutsideDeath();
        //}
    }

    public Canvas GetPersonalCanvas()
    {
        return m_playerCanvas;
    }

    public void ResetCombatVars()
    {
        m_ActiveWeapon.m_attackFinished = false;
        GetCombatBar().m_combatSlider.value = 0;
        GetCombatBar().SetPortraitBackgroundColor(TurnBasedScript.Instance.m_attackColors[(int)eAttackColors.Neutral]);
        UpdateEffects();
        GetCombatBar().Restart();
        GetCombatBar().CombatActive = true;
        m_decidedAttack = false;
        m_decidedTarget = false;
        m_playerToAttack = null;
    }

    public virtual void AfterAttackConsequences(UseConsequences conseq)
    {

    }

    public virtual void OnKillConsequences(UseConsequences conseq)
    {

    }

    public void ResetAnimationVariables()
    {
        GetAnimScript().ResetVariables();
        if (m_ActiveWeapon != null)
        {
            m_ActiveWeapon.m_animEffect.ResetAnimation();
            m_ActiveWeapon.m_animEffect.StopEffect();
        }

    }
}
