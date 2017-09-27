using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEffectScript : MonoBehaviour {

    public enum EffectPlacement
    {
        User,
        Victim,
        MiddleVictims
    }

    public EffectPlacement m_effectPlacement = EffectPlacement.User;
    public ParticleSystem m_partSys;
    public Animator m_animator;
    public Motion m_anim;
    public float startPlayback;
    public bool FinishedAnimation { get; set; }
    public bool m_attachToUser;
    public bool m_needsToBeStopped = false;
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

    public float GetEffectPosition(CharacterStatSheet user, CharacterStatSheet defender, CharacterStatSheet[] defendingTeam)
    {
        float result = 0;
        switch (m_effectPlacement)
        {
            case EffectPlacement.User:
                result = user.gameObject.transform.position.x;
                break;
            case EffectPlacement.Victim:
                result = defender.gameObject.transform.position.x;
                break;
            case EffectPlacement.MiddleVictims:
                result = FindMiddleGround(defendingTeam);
                break;
            default:
                break;
        }
        return result;
    }

    float FindMiddleGround(CharacterStatSheet[] team)
    {
        float highestX = team[0].transform.position.x;
        float lowestX = team[0].transform.position.x;

        foreach (CharacterStatSheet charSS in team)
        {
            if (lowestX > charSS.transform.position.x)
                lowestX = charSS.transform.position.x;
            if (highestX < charSS.transform.position.x)
                highestX = charSS.transform.position.x;
        }

        float diff = (highestX - lowestX) / 2;

        return lowestX + diff;
    }
}
