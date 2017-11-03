using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterStanceStatus : StatusBase {

    public override void OnApply(CharacterStatSheet applyTo)
    {
    }

    public override void Setup(CharacterStatSheet attacker)
    {
        foreach(WeaponBase.WeaponEffect weapeffect in attacker.m_ActiveWeapon.weapAbility)
        {
            if (weapeffect.effect.m_effectType == m_effectType)
            {
                this.Init(weapeffect.effectTime, weapeffect.effectDamage);
                attacker.GetEffectArray()[(int)m_effectType] = this;
            }
        }
    }

    public override void Use(CharacterStatSheet useOn)
    {
    }


}
