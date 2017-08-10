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
    public bool FadeDeath = false;
    public SpriteRenderer m_renderer;
    public float m_fadeSpeed;
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
            StartFadeDeath();
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

    public void StartFadeDeath()
    {
        FadeDeath = true;
    }
    public void FadingDeath()
    {
        Color buffCol = m_renderer.color;
        buffCol.a = Mathf.Lerp(m_renderer.color.a, 0, m_fadeSpeed * Time.deltaTime);
        m_renderer.color = buffCol;
        if (m_renderer.color.a < 0.001f)
        {
            FadeDeath = false;
            gameObject.SetActive(false);
        }
    }
}
