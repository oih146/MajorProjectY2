using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBase : MonoBehaviour {

    public int m_maxDurability;
    private int m_currentDurability;
    public int[] m_breakStages;
    public int[] m_stageDamageReduction;
	// Use this for initialization
	void Start () {
        m_currentDurability = m_breakStages[0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetDamageReduction(int tempArmour)
    {
        int temp = m_currentDurability + tempArmour;
        if (temp > m_breakStages[1])
            return m_stageDamageReduction[0];
        else if (temp > m_breakStages[2])
            return m_stageDamageReduction[1];
        else
            return m_stageDamageReduction[2];
    }

    public void TookAHit()
    {
        m_currentDurability--;
        if (m_currentDurability < 0)
            m_currentDurability = 0;
    }

    public void RepairArmour(int repairArmorAmount)
    {
        m_currentDurability += repairArmorAmount;
    }
}
