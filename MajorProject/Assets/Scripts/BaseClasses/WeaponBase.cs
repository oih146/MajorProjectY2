using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEffects
{
    BonusIncapacitationPoints = 0,  //+
    BurnChance,                     //+
    BurnDamage,
    AdditionBurnChance,
    TakeBonusInterupt,
    NumOfEffects
}

public enum eAbilities
{
    DamageReduction = 0,            //+-
    CounterStance,
    BonusDamage,                    //+
    BonusToEvil,                    //+
    InteruptModifier,
    InteruptHealthMod,
    NumOfAbilities
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
    HealAll
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
    [System.Serializable]
    public struct WeaponEffect
    {
        public eEffects effectType;
        public int effectTime;
        public int effectDamage;
    }

    [System.Serializable]
    public struct WeaponAbility
    {
        public eAbilities abilityType;
        public int abilityTime;
        public int abilityDamage;
    }

    public WeaponEffect[] weapEffects;
    public WeaponAbility[] weapAbility;

    public Motion GetAnimationToPlay()
    { 
        return m_animToPlay;
    }

    public float GetAttack()
    {
        return m_attackDamage;
    }

    public void ApplyEffects(CharacterStatSheet userOfWeap, CharacterStatSheet attackingPlayer)
    {
        for(int i = 0; i < weapEffects.Length; i++)
        {
            if (weapEffects[i].effectType == eEffects.BurnChance && attackingPlayer.ChanceOfBurning())
            {
                attackingPlayer.AddEffect(weapEffects[i]);
                attackingPlayer.m_burning = true;
            }
            else
            {
                attackingPlayer.AddEffect(weapEffects[i]);
            }
        }

        for(int i = 0; i < weapAbility.Length; i++)
        {
            userOfWeap.AddAbility(weapAbility[i]);
        }
    }

    public bool DoesAttackAll()
    {
        return (m_attackType == AttackType.MultipleAll || m_attackType == AttackType.SingleAll || m_attackType == AttackType.HealAll) ? true : false;
    }
}
