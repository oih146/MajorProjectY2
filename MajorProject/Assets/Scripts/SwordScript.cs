using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : WeaponBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CalculateDamage()
    {
        if (m_strength == AttackStrength.Light)
            m_attackDamage = 15;
        else if (m_strength == AttackStrength.Normal)
            m_attackDamage = 25;
        else if (m_strength == AttackStrength.Heavy)
            m_attackDamage = 50;
    }
}
