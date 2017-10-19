using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulRipEffect : AnimationEffectScript {

    public Transform m_targetForSoul;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {


    }

    public override void StopEffect()
    {
        StartCoroutine(WaitTillDeath());
    }

    IEnumerator WaitTillDeath()
    {
        yield return new WaitUntil(() => !m_partSys.IsAlive(false));
        m_rootHolder.SetActive(false);
        FinishedAnimation = true;
    }

    public override void OnSelect(CharacterStatSheet character)
    {
        base.OnSelect(character);

        //PlayEffect();
    }

    public override void OnUse(CharacterStatSheet character)
    {
        base.OnUse(character);

        m_targetForSoul.position = character.gameObject.transform.position;

    }
}
