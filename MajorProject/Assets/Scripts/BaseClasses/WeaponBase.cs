using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackStrength
{
    Light,
    Normal,
    Heavy
}

public class WeaponBase : MonoBehaviour {
    
    public float m_attackDamage;
    public bool m_attackAll;
    //public float m_LawOrderShift;
    public Motion m_animToPlay;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Motion GetAnimationToPlay()
    {
        return m_animToPlay;
    }

    public float GetAttack()
    {
        return m_attackDamage;
    }
}
