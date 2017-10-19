using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttack : MagicAttack {

    public Motion m_counterMotion;
    public Motion m_endMotion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnSelect(CharacterStatSheet character)
    {
        base.OnSelect(character);

        character.m_animator.Play(m_animToPlay.name);
    }

    public override void OnUse(CharacterStatSheet character)
    {
        base.OnUse(character);
    }

    public void SecondaryUse(CharacterStatSheet character)
    {
        character.m_animator.Play(m_counterMotion.name);
    }

    public override void OnEnd(CharacterStatSheet character)
    {
        base.OnEnd(character);

        character.m_animator.Play(m_endMotion.name);
    }

}
