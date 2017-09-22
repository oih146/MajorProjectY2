using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEffectScript : MonoBehaviour {

    ParticleSystem m_partSys;
    Animator m_animator;
    public Motion m_anim;

	// Use this for initialization
	void Start () {
        m_partSys = GetComponent<ParticleSystem>();
        m_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayEffect()
    {
        m_animator.Play(m_anim.name);
        m_partSys.Play(true);
    }

    public void StopEffect()
    {
        m_animator.Stop();
        m_partSys.Stop();
    }
}
