using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisarmedStatus : StatusBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnApply(CharacterStatSheet applyTo)
    {
        applyTo.Disarmed = true;
    }

    public override void Remove(CharacterStatSheet removeFrom)
    {
        removeFrom.Disarmed = false;
    }
}
