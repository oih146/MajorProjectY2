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

    public void ApplyEffects(CharacterStatSheet userOfWeap, CharacterStatSheet attackingPlayer)
    {
        for(int i = 0; i < weapEffects.Length; i++)
        {
            if (weapEffects[i].effectType == eEffects.BurnChance && attackingPlayer.ChanceOfBurning())
            {
                attackingPlayer.AddEffect(weapEffects[i]);
                attackingPlayer.m_burning = true;
                attackingPlayer.m_burnTimer = weapEffects[i].effectTime;
            }
            else if(weapEffects[i].effectType == eEffects.Disarmed)
            {
                attackingPlayer.m_disarmed = true;
                if(attackingPlayer.m_decidedAttack)
                {
                    weapEffects[i].effectTime++;
                    attackingPlayer.AddEffect(weapEffects[i]);
                }
            } else
            {
                attackingPlayer.AddEffect(weapEffects[i]);
            }
        }

        for(int i = 0; i < weapAbility.Length; i++)
        {
            userOfWeap.AddEffect(weapAbility[i]);
        }
    }

    public bool DoesAttackAll()
    {
        return (m_attackType == AttackType.MultipleAll || m_attackType == AttackType.MassAttack|| 
            m_attackType == AttackType.SingleAll || m_attackType == AttackType.HealAll || 
            m_attackType == AttackType.BuffDebuff) ? true : false;
    }
}
