﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenuScript : MonoBehaviour {

    public Button AttackButton;
    public Button FleeButton;
    public Button EndTurnButton;
    public Button MagicButton;
    public Button CancelAttackButton;
    public Image[] spellCharges = new Image[PlayerStat.m_maxSpellsPerDay];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
