using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoodNEvil
{
    Knight = 0,
    BlackGuard = -1,
    Paladin = 1
}

public enum LawNOrder
{
    Neutral = 0,
    Lawful = 1,
    Chaotic = -1
}

public class CharacterStatSheet : MonoBehaviour {

    public float m_health;
    public float m_mana;
    public WeaponBase m_weapon;
    public bool m_knowMagic;
    public MagicAttack[] m_spells;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
