﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPrototype : MonoBehaviour {

    public FadeBlack m_fadeBlack;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider hit)
    {
        if(hit.tag == "Player")
        {
            m_fadeBlack.m_fadeIn = false;
            m_fadeBlack.m_fading = true;
            FadeBlack.Activate();

        }
    }
}
