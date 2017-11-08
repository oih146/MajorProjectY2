using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDamageStatus : StatusBase {

    public override void OnUpdate(CharacterStatSheet useOn)
    {
        base.OnUpdate(useOn);

        if(useOn.m_abilities[5].HasConsequences)
            useOn.OnKillConsequences(useOn.m_abilities[5].m_consequences);
    }

    public override void Remove(CharacterStatSheet removeFrom)
    {
        base.Remove(removeFrom);

        Debug.Log("Turning Off Ability");
        removeFrom.m_abilities[5].m_animEffect.StopEffect();
    }

}
