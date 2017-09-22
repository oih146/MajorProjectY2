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

    public AnimatorStateInfo GetAnimatorStateInfo()
    {
        return m_animator.GetCurrentAnimatorStateInfo(0);
    }

    public ParticleSystem GetParticleSystem()
    {
        return m_partSys;
    }

    public IEnumerator PlayEffect()
    {
        gameObject.SetActive(true);
        m_animator.Play(m_anim.name);
        m_partSys.Play(true);
        yield return new WaitForSeconds(1.0f);
        yield return new WaitUntil(() => !m_animator.GetCurrentAnimatorStateInfo(0).IsName(m_anim.name));
        StopEffect();
    }

    public void StopEffect()
    {
        m_animator.Stop();
        m_partSys.Stop();
        gameObject.SetActive(false);
    }
}
