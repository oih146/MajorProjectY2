﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour {

    public float m_attackDamage;
    //public float m_LawOrderShift;

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
