using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulRipAttack : MagicAttack {

    public Motion m_initalAnim;

    float m_animSpeed = 1;
    double m_animTime;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    }

    public override void OnSelect(CharacterStatSheet character)
    {
        base.OnSelect(character);

        m_animEffect.m_rootHolder.transform.position = character.m_playerToAttack.transform.position;

        character.m_animator.Play(m_animToPlay.name);

        m_animEffect.m_rootHolder.SetActive(true);
        m_animEffect.m_animator.Play(m_initalAnim.name);
        m_animEffect.m_partSys.Play();

        DelayAnimation(m_animEffect.m_animator, (int)m_chargeTime);
    }

    public override void OnUse(CharacterStatSheet character)
    {
        base.OnUse(character);
        m_animEffect.m_animator.speed = 1;
        //m_animEffect.m_animator.Play(m_animToPlay.name);
        m_animEffect.OnUse(character);
    }

    public override void OnEnd(CharacterStatSheet character)
    {
        base.OnEnd(character);
    }

    public void DelayAnimation(Animator anim, float delay)
    {
        if (delay < 0.001)
            delay = 0.001f;
        delay *= (0.27f * 2);

        anim.speed /= delay;
        m_animSpeed = anim.speed;
    }

    public void TakeFromTimer(Animator anim, float percentage)
    {
        AnimationClip clip = new AnimationClip();
        bool foundclip = false; 
        foreach(AnimationClip animClip in anim.runtimeAnimatorController.animationClips)
        {
            if(animClip.name == m_initalAnim.name)
            {
                clip = animClip;
                foundclip = true;
            }
        }
        if (foundclip)
            m_animTime = percentage * clip.length;
        else
            Debug.Log("Error! Didn't find Soul Rip Animation Clip");
        //anim.SetTime((anim.GetTime() - delay) * anim.speed);
    }

    public override void OnPause()
    {
        Debug.Log("Pausing");
        m_animSpeed = m_animEffect.m_animator.speed;
        m_animTime = m_animEffect.m_animator.GetTime();
        m_animEffect.m_animator.speed = 0;
    }

    public override void OnResume(CharacterStatSheet character)
    {
            Debug.Log("Restart");
            m_animEffect.m_animator.speed = m_animSpeed;
            m_animEffect.m_animator.SetTime(m_animTime);
            //m_animEffect.m_animator.Play(m_animEffect.m_anim.name);
            Debug.Log(m_animEffect.m_animator.speed);
        m_animEffect.m_animator.Play(m_initalAnim.name);
    }
}
