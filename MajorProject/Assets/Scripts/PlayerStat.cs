﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class PlayerStat : CharacterStatSheet {

    public float m_GoodEvil;
    public float Light
    {
        get
        {
            return m_GoodEvil;
        }

        set
        {
            GameObject buff = Instantiate(m_notificationBox, GetPersonalCanvas().transform);
            buff.SetActive(true);
            buff.transform.localPosition = new Vector3(-220, 40, 1);
            buff.GetComponent<UnityEngine.UI.Text>().text = "Light is " + value;
            m_GoodEvil = value;
        }
    }
    public float m_OrderChaos;
    public float Law
    {
        get
        {
            return m_OrderChaos;
        }

        set
        {
            GameObject buff = Instantiate(m_notificationBox, GetPersonalCanvas().transform);
            buff.SetActive(true);
            buff.transform.localPosition = new Vector3(-220, 28);
            buff.GetComponent<UnityEngine.UI.Text>().text = "Law is " + value;
            m_OrderChaos = value;
        }
    }
    public GameObject m_notificationBox;
    public Canvas m_playerCanvas;
    public float m_charisma;
    public bool m_inBattle;
    public int m_maxSpellsPerDay;
    public int MaxSpells
    {
        get
        {
            return m_maxSpellsPerDay;
        }
    }
    public int m_spellsAvaliable;
    public int SpellAvaliable
    {
        get
        {
            return m_spellsAvaliable;
        }

        set
        {
            m_spellsAvaliable = value;
        }
    }
    public CharacterStatSheet[] m_allies = new CharacterStatSheet[1];

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckAlignment()
    {
       SetAlignment();
    }

    void SetAlignment()
    {
        if(DialogueLua.GetVariable("Law").AsFloat != 0)
            AddToOrderChaos(DialogueLua.GetVariable("Law").AsFloat);
        DialogueLua.SetVariable("Law", 0);
        if(DialogueLua.GetVariable("Light").AsFloat != 0)
            AddToGoodEvil(DialogueLua.GetVariable("Light").AsFloat);
        DialogueLua.SetVariable("Light", 0);
    }

    public void AddToOrderChaos(float amountToAdd)
    {
        Law += amountToAdd;
    }

    public void AddToGoodEvil(float amountToAdd)
    {
        Light += amountToAdd;
    }

    public Canvas GetPersonalCanvas()
    {
        return m_playerCanvas;
    }
}
