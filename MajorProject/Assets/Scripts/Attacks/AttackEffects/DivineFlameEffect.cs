using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivineFlameEffect : AnimationEffectScript {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnEnd(CharacterStatSheet character)
    {
        StopEffect();
    }
}
