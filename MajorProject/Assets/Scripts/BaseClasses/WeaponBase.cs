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
    Light,
    Normal,
    Heavy
}

public class WeaponBase : MonoBehaviour {
    
    public float m_attackDamage;
    public bool m_attackAll;
    //public float m_LawOrderShift;
    public Motion m_animToPlay;
    public bool m_multipleHits;
    public int m_howManyHits;
    public bool m_attackFinished;
    public bool m_Offensive;
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
                attackingPlayer.m_effectsToApply[(int)weapEffects[i].effectType] = weapEffects[i].effectDamage;
                attackingPlayer.m_effectTime[(int)weapEffects[i].effectType] = weapEffects[i].effectTime;
            }
            else
            {
                attackingPlayer.m_effectsToApply[(int)weapEffects[i].effectType] = weapEffects[i].effectDamage;
                attackingPlayer.m_effectTime[(int)weapEffects[i].effectType] = weapEffects[i].effectTime;
            }

        }
    }
}
