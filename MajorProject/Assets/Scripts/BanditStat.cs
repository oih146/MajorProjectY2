using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditStat : EnemyBase {



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
