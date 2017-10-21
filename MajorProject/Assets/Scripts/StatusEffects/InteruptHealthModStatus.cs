using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteruptHealthModStatus : StatusBase {

    public override void Setup(CharacterStatSheet attacker)
    {
        attacker.GetCombatBar().SlowDown(((attacker.m_playerToAttack.Health) / 10) -
            (attacker.m_ActiveWeapon.m_chargeTime == ChargeTime.Magic ? attacker.GetStatistics().GetWillPowerCastTimeDecrease() : 0));
    }

}
