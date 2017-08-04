using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour {

    public float m_attackDamage;
    public bool m_attackAll;
    //public float m_LawOrderShift;
    public Animator m_animator;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float GetAttack()
    {
        return m_attackDamage;
    }
}
