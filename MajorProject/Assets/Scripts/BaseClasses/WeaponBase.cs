using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEffects
{
    DamageReduction = 0,                //+-
    BonusIncapacitationPoints,      //+
    BonusDamage,                    //+
    BonusToEvil,                    //+
    BurnChance,                     //+
    BurnDamage,
    AdditionBurnChance,
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

    public WeaponEffect[] weapEffects;

    public Motion GetAnimationToPlay()
    { 
        return m_animToPlay;
    }

    public float GetAttack()
    {
        return m_attackDamage;
    }

    public void ApplyEffects(CharacterStatSheet attackingPlayer)
    {
        for(int i = 0; i < weapEffects.Length; i++)
        {
            if (weapEffects[i].effectType == eEffects.BurnChance && attackingPlayer.ChanceOfBurning())
            {
                attackingPlayer.GetEffectArray()[(int)weapEffects[i].effectType] = weapEffects[i].effectDamage;
                attackingPlayer.GetEffectTimeArray()[(int)weapEffects[i].effectType] = weapEffects[i].effectTime;
                attackingPlayer.m_burning = true;
            }
            else
            {
                attackingPlayer.GetEffectArray()[(int)weapEffects[i].effectType] = weapEffects[i].effectDamage;
                attackingPlayer.GetEffectTimeArray()[(int)weapEffects[i].effectType] = weapEffects[i].effectTime;
            }

        }
    }

    public bool DoesAttackAll()
    {
        return (m_attackType == AttackType.MultipleAll || m_attackType == AttackType.SingleAll || m_attackType == AttackType.HealAll) ? true : false;
    }
}
