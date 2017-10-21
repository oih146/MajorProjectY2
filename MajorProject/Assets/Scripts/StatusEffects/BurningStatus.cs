using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningStatus : StatusBase {

    public int m_burnChance = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnUpdate(CharacterStatSheet useOn)
    {
        useOn.Health -= Strength;
        useOn.ReCheckHealth();
        if (useOn.DeathCheck())
            TurnBasedScript.CallOnOutsideDeath();
    }

    public override void OnApply(CharacterStatSheet applyTo)
    {
        if(ApplyChance(applyTo))
        {
            base.OnApply(applyTo);
        }
    }

    public bool ApplyChance(CharacterStatSheet applyTo)
    {
        int burnChance = m_burnChance;
        burnChance += 50;
        if (IsActive)
            burnChance += 50;
        if (Random.Range(0, 100) <= burnChance)
        {
            return true;
        }
        return false;
    }
}
