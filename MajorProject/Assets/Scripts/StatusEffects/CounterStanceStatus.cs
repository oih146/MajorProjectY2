using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterStanceStatus : StatusBase {

    public override void OnApply(CharacterStatSheet applyTo)
    {
        
    }

    public override void Setup(CharacterStatSheet attacker)
    {
        attacker.AddEffect(this);
    }

    public override void Use(CharacterStatSheet useOn)
    {
        useOn.ResetCombatVars();
    }


}
