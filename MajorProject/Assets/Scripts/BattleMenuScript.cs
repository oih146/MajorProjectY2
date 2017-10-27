using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenuScript : MonoBehaviour {

    public Button[] AttackButtons;
    public GameObject FleeButton;
    public GameObject EndTurnButton;
    public GameObject MagicButtonRoot;
    public Button[] magicButtons;
    public GameObject CancelAttackButton;
    public GameObject SpellChargeRoot;
    public GameObject[] spellCharges = new GameObject[PlayerStat.m_maxSpellsPerDay];
    public GameObject CombatBar;
    public static BattleMenuScript Instance;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
