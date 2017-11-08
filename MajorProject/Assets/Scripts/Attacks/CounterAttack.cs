using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttack : MagicAttack {

    public Motion m_counterMotion;
    public Motion m_startMotion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnSelect(CharacterStatSheet character)
    {
        base.OnSelect(character);

        for (int i = 0; i < weapAbility.Length; i++)
        {
            character.AddEffect(weapAbility[i].effect.Init(weapAbility[i].effectTime, weapAbility[i].effectDamage));
        }
        character.m_animator.Play(m_startMotion.name);
    }

    public override void OnUse(CharacterStatSheet character)
    {
        base.OnUse(character);
    }

    public void SecondaryUse(CharacterStatSheet character)
    {
        character.m_animator.Play(m_counterMotion.name);
        //StartCoroutine(WaitTillEnd(character));
    }

    IEnumerator WaitTillEnd(CharacterStatSheet character)
    {
        float m_timegrab = Time.time;
        yield return new WaitForSeconds(character.m_animator.GetCurrentAnimatorStateInfo(0).length);
        character.m_animator.Stop();
        character.m_animator.Play(m_counterMotion.name);
        yield return new WaitForSeconds(0.1f);
        character.m_animator.Stop();
    }

    public override void OnEnd(CharacterStatSheet character)
    {
        base.OnEnd(character);

        character.SetToBattleIdle();
    }

}
