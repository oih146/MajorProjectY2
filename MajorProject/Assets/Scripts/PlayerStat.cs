using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : CharacterStatSheet {

    public float m_GoodEvil;
    public float m_OrderChaos;
    public float m_charisma;
    public bool m_inBattle;
    public CharacterStatSheet[] m_allies = new CharacterStatSheet[1];

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddToOrder(float amountToAdd)
    {
        m_OrderChaos += amountToAdd;
    }

    public void AddToChaos(float amountToAdd)
    {
        m_OrderChaos -= amountToAdd;
    }

    public void AddToEvil(float amountToAdd)
    {
        m_GoodEvil -= amountToAdd;
    }

    public void AddToGood(float amountToAdd)
    {
        m_GoodEvil += amountToAdd;
    }
}
