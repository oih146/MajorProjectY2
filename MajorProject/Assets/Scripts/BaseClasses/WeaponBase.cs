using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEffects
{
    BonusIncapacitationPoints = 0,  //+
    BonusIncapPointsOnDeath,
    Disarmed,
    BurnChance,                     //+
    BurnDamage,
    AdditionBurnChance,
    TakeBonusInterupt,
    DamageReduction,            //+-
    SpeedReduction,
    CounterStance,
    Invulnerability,
    BonusDamage,                    //+
    BonusToEvil,                    //+
    InteruptModifier,
    InteruptHealthMod,
    NumOfEffects
}

public enum AttackStrength
{
    Light = 3,
    Normal = 5,
    Heavy = 8,
    Magic = 10
}

public enum AttackType
{
    SingleOne,
    MultipleOne,
    MultipleAll,
    SingleAll,
    HealSelect,
    HealOne,
    HealAll,
    Flee,
    MassAttack,
    BuffDebuff,
    Branching,
    Drain
}

public class WeaponBase : MonoBehaviour {
    
    public float m_attackDamage;
    public AttackStrength m_strength;
    //public float m_LawOrderShift;
    public Motion m_animToPlay;
    public Animation m_animationForPlay;
    [Range(1, 10)]
    public int m_howManyHits;
    public bool m_attackFinished;
    public AttackType m_attackType = AttackType.SingleOne;

    public AnimationEffectScript m_animEffect;
    public bool HasEffect { get { return m_animEffect == null ? false : true; } }

    [System.Serializable]
    public struct WeaponEffect
    {
        public eEffects effectType;
        public int effectTime;
        public int effectDamage;
    }


    public WeaponEffect[] weapEffects;
    public WeaponEffect[] weapAbility;

    public Motion GetAnimationToPlay()
    { 
        return m_animToPlay;
    }

    public float GetAttack()
    {
        return m_attackDamage;
    }

    //Apply effects to only one attacker and one defender
    public void ApplyEffects(CharacterStatSheet userOfWeap, CharacterStatSheet attackingPlayer)
    {
        AddAbilities(userOfWeap);
        AddEffects(attackingPlayer);
    }

    //Apply Abilites, only used for the attacker
    public void AddAbilities(CharacterStatSheet attacker)
    {
        for (int i = 0; i < weapAbility.Length; i++)
        {
            attacker.AddEffect(weapAbility[i]);
        }
    }

    //Apply Effects, only used for the defenders
    public void AddEffects(CharacterStatSheet defender)
    {
        for (int i = 0; i < weapEffects.Length; i++)
        {
            if (weapEffects[i].effectType == eEffects.BurnChance)
            {
                defender.AddEffect(weapEffects[i]);
                if (defender.ChanceOfBurning())
                {
                    defender.Burning = true;
                    defender.m_burnTimer = weapEffects[i].effectTime;
                }
            }
            else if (weapEffects[i].effectType == eEffects.Disarmed)
            {
                defender.Disarmed = true;
                defender.AddEffect(weapEffects[i]);
            }
            else
            {
                defender.AddEffect(weapEffects[i]);
            }
        }
    }

    //Apply Effects to Multiple defenders but only one attacker
    public void ApplyEffects(CharacterStatSheet userOfWeap, CharacterStatSheet[] defenders)
    {
        AddAbilities(userOfWeap);
        foreach(CharacterStatSheet charSS in defenders)
        {
            AddEffects(charSS);
        }
    }

    public bool DoesAttackAll()
    {
        return (m_attackType == AttackType.MultipleAll || m_attackType == AttackType.MassAttack|| 
            m_attackType == AttackType.SingleAll || m_attackType == AttackType.HealAll || 
            m_attackType == AttackType.BuffDebuff || m_attackType == AttackType.Flee ||
            m_attackType == AttackType.HealOne) ? true : false;
    }

    public IEnumerator PlayWeaponEffect(CharacterStatSheet trigger)
    {
        yield return new WaitUntil(() => trigger.GetAnimScript().WeaponEffect);
        m_animEffect.PlayEffect();
    }
}
