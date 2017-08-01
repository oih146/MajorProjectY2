using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : CharacterStatSheet {

    public float m_GoodEvil;
    public float m_LawOrder;
    public float m_charisma;
    public bool m_inBattle;
    public CharacterStatSheet[] m_allies = new CharacterStatSheet[1];

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
