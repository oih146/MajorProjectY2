using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulRipEffect : AnimationEffectScript {

    public ParticleSystem m_soulSystem;
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

    public override void PlayEffect()
    {
        m_soulSystem.gameObject.SetActive(true);
        m_soulSystem.Play();
        m_animator.Play(m_anim.name);
    }

    IEnumerator WaitTillDeath()
    {
        var variable = m_soulSystem.emission;
        yield return new WaitUntil(() => !m_soulSystem.emission.enabled);
        variable.enabled = true;
        m_partSys.Stop();
        m_soulSystem.Stop();
        m_animator.Stop();
        m_rootHolder.SetActive(false);
        m_targetForSoul.parent = m_partSys.transform;
        m_soulSystem.gameObject.SetActive(false);
        FinishedAnimation = true;
    }

    public override void OnSelect(CharacterStatSheet character)
    {
        base.OnSelect(character);

        ParentSoul(character);
        //m_targetForSoul.position = playerAnim.CastHandPosition.position;

        //PlayEffect();
    }

    public override void OnUse(CharacterStatSheet character)
    {
        base.OnUse(character);

        //m_soulSystem.Play();

        ParentSoul(character);
        //m_targetForSoul.position = playerAnim.CastHandPosition.position;

    }

    public void ParentSoul(CharacterStatSheet character)
    {
        PlayerAnimScript playerAnim = (PlayerAnimScript)character.m_animScript;

        m_targetForSoul.parent = playerAnim.CastHandPosition;
        m_targetForSoul.localPosition = Vector3.zero;
    }

    public void UnParentSoul()
    {
        m_targetForSoul.parent = m_partSys.transform;
    }
}
