using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEffectScript : MonoBehaviour {

    public ParticleSystem m_partSys;
    public Animator m_animator;
    public Motion m_anim;
    public float startPlayback;
    public bool FinishedAnimation { get; set; }
    public bool m_attachToUser;

    public bool HasParticle { get { return m_partSys == null ? false : true; } }
    public bool HasAnimation { get { return m_animator == null ? false : true; } }

	// Use this for initialization
	void Start () {
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

    public void PlayEffect()
    {
        ResetAnimation();
        gameObject.SetActive(true);
        if (m_animator != null)
        {
            m_animator.Play(m_anim.name);
        }
        if (m_partSys != null)
            m_partSys.Play(true);
    }

    public void StopEffect()
    {
        if (m_animator != null)
            m_animator.Stop();
        if (m_partSys != null) 
            m_partSys.Stop(true);
        gameObject.SetActive(false);
    }

    public void FinishedAnim()
    {
        FinishedAnimation = true;
    }

    public void ResetAnimation()
    {
        FinishedAnimation = false;
    }
}
