using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PtUAttack : MagicAttack {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnEnd(CharacterStatSheet character)
    {
        base.OnEnd(character);

        PtUEffect ptueffect = (PtUEffect)m_animEffect;
        ptueffect.StopEffect();
    }
}
