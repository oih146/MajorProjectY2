using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStats : MonoBehaviour {

    public int m_strength = 1;            /*Stat for damage dealt*/
    private float m_strengthPercentage = 2;  //Damage percentage added per strength point
    public int m_speed = 1;               //Stat for Combatbar fill speed
    private float m_speedPercentage = 0.25f;     //Speed percentage added per speed point
    public int m_dexterity = 1;           //Stat for dodge chance
    private float m_dexterityPercentage = 2; //Dexterity percentage added per dexterity point
    public int m_willpower = 1;           //Stat for aditional spell charges
    private float m_willpowerSpellCastTimeDecrease = 0.5f;
    private int m_willpowerIPincrease = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Increases Base Damage
    public int GetStrength()
    {
        if(m_strength != 0)
            return m_strength * (int)m_strengthPercentage;
        return 0;
    }

    //Decreases Charge bar time (overall)
    public float GetSpeed()
    {
        if(m_speed != 0)
            return m_speed * m_speedPercentage;
        return 0;
    }

    public float GetDexterity()
    {
        if (m_dexterity != 0)
            return m_dexterity * m_dexterityPercentage;
        return 0;
    }

    public int GetWillPower()
    {
        return m_willpower;
    }

    public float GetWillPowerCastTimeDecrease()
    {
        if(m_willpower > 0)
            return m_willpower * m_willpowerSpellCastTimeDecrease;
        return 0;
    }

    public int GetWillPowerIPIncrease()
    {
        if (m_willpower > 0)
            return m_willpower * m_willpowerIPincrease;
        return 0;
    }
}
