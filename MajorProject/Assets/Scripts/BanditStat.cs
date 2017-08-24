using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditStat : EnemyBase {

    public int m_maxIP;
    public int IncapacitationPoints
    {
        get
        {
            return m_incapacitationPoints;
        }

        set
        {
            m_incapacitationPoints += value;
            if (m_incapacitationPoints > m_maxIP)
                m_surrender = true;
        }
    }

	// Use this for initialization
	void Start () {
        m_renderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (FadeDeath)
            FadingDeath();
	}
}
