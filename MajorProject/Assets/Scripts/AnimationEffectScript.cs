﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEffectScript : MonoBehaviour {

    public enum EffectPlacement
    {
        User,
        Victim,
        MiddleVictims
    }

    public GameObject m_rootHolder;
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
        if (m_rootHolder == null)
            m_rootHolder = gameObject;
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
        m_rootHolder.SetActive(true);
        Debug.Log(m_rootHolder.activeInHierarchy);
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
        m_rootHolder.SetActive(false);
    }

    public void FinishedAnim()
    {
        FinishedAnimation = true;
    }

    public void ResetAnimation()
    {
        FinishedAnimation = false;
    }

    public Vector3 GetEffectPosition(CharacterStatSheet user, CharacterStatSheet defender, CharacterStatSheet[] defendingTeam)
    {
        Vector3 result = Vector3.zero;
        switch (m_effectPlacement)
        {
            case EffectPlacement.User:
                result = user.gameObject.transform.position;
                break;
            case EffectPlacement.Victim:
                result = defender.gameObject.transform.position;
                break;
            case EffectPlacement.MiddleVictims:
                float xPos = FindMiddleGround(defendingTeam);
                result = new Vector3(xPos, user.gameObject.transform.position.y, user.gameObject.transform.position.z);
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
