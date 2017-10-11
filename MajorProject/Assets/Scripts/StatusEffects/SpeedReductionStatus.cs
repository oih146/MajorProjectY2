using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedReductionStatus : StatusBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Use(CharacterStatSheet applyTo)
    {
        applyTo.GetCombatBar().SetTemporarySpeedDecrease(Strength);
    }
}
