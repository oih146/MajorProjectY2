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
        return m_strength * (int)m_strengthPercentage;
    }

    //Decreases Charge bar time (overall)
    public float GetSpeed()
    {
        return m_speed * m_speedPercentage;
    }

    public float GetDexterity()
    {
        return m_dexterity * m_dexterityPercentage;
    }

    public int GetWillPower()
    {
        return m_willpower;
    }

    public float GetWillPowerCastTimeDecrease()
    {
        return m_willpowerSpellCastTimeDecrease;
    }

    public int GetWillPowerIPIncrease()
    {
        return m_willpowerIPincrease;
    }
}
