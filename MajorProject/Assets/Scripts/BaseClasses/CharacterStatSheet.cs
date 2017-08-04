using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoodNEvil
{
    Knight = 50,
    BlackGuard = 0,
    Paladin = 100
}

public enum LawNOrder
{
    Neutral = 50,
    Lawful = 100,
    Chaotic = 0
}

public class CharacterStatSheet : MonoBehaviour {

    public float m_maxHealth;
    //[HideInInspector]
    public float m_health;

    public float Health
    {
        get
        {
            return m_health;
        }

        set
        {
            m_health = value;
        }
    }
    public float m_armour;
    public float Armour
    {
        get
        {
            return m_armour;
        }

        set
        {
            m_armour = value;
        }
    }
    public WeaponBase m_weapon;
    public bool m_knowMagic;
    public MagicAttack[] m_spells;
    public bool m_isDead = false;
    public Animator m_animator;
    public bool m_attacking;
    public UnityEngine.UI.Slider m_healthBar;

    // Use this for initialization
    void Start () {
        m_health = m_maxHealth;
        m_healthBar = gameObject.GetComponentInChildren<UnityEngine.UI.Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool DeathCheck()
    {
        if(m_health <= 0)
        {
            m_isDead = true;
            //play death anim
            m_animator.Stop();
            return true;
        }
        return false;
    }

    public UnityEngine.UI.Slider GetHealthBar()
    {
        return m_healthBar;
    }

    public void ReCheckHealth()
    {
        m_healthBar.value = m_health;
    }

    public void GetHitAnim()
    {

    }

    public AnimatorStateInfo GetAnimatorStateInfo()
    {
        return m_animator.GetCurrentAnimatorStateInfo(0);
    }

    public void HitPointOpen()
    {
        m_attacking = true;
    }

    public void HitPointClosed()
    {
        m_attacking = false;
    }
}
