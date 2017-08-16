using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStats : MonoBehaviour {

    public int m_strength = 1;            /*Stat for damage dealt*/
    public float m_strengthPercentage = 1;  //Damage percentage added per strength point
    public int m_speed = 1;               //Stat for Combatbar fill speed
    public float m_speedPercentage = 1;     //Speed percentage added per speed point
    public int m_dexterity = 1;           //Stat for dodge chance
    public float m_dexterityPercentage = 1; //Dexterity percentage added per dexterity point
    public int m_willpower = 1;           //Stat for aditional spell charges

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
