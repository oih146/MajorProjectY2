﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class PlayerStat : CharacterStatSheet {

    public AlignmentLines m_alignmentLines;
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
            buff.transform.localPosition = new Vector3(-300, 0, 1);
            if (value < 0)
                m_GoodEvil = 0;
            else if (value > 100)
                m_GoodEvil = 100;
            else
                m_GoodEvil = value;
            if(value > 0)
                buff.GetComponent<UnityEngine.UI.Text>().text = m_alignmentLines.GetLightLine(30);
            else
                buff.GetComponent<UnityEngine.UI.Text>().text = m_alignmentLines.GetLightLine(0);
            m_LightSlider.value = m_GoodEvil;
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
            buff.transform.localPosition = new Vector3(-300, -15, 1);
            if (value < 0)
                m_OrderChaos = 0;
            else if (value > 100)
                m_OrderChaos = 100;
            else
                m_OrderChaos = value;
            if (value > 0)
                buff.GetComponent<UnityEngine.UI.Text>().text = m_alignmentLines.GetLawLine(30);
            else
                buff.GetComponent<UnityEngine.UI.Text>().text = m_alignmentLines.GetLawLine(0);
            m_LawSlider.value = m_OrderChaos;

        }
    }
    public GameObject m_notificationBox;
    public UnityEngine.UI.Slider m_LightSlider;
    public UnityEngine.UI.Slider m_LawSlider; 
    public Canvas m_playerCanvas;
    public float m_charisma;
    public bool m_inBattle;
    public static int m_maxSpellsPerDay = 2;
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
        Starts();
        m_spellsAvaliable = m_maxSpellsPerDay;
        m_LawSlider.maxValue = (int)LawNOrder.Lawful;
        m_LawSlider.value = Law;
        m_LightSlider.maxValue = (int)GoodNEvil.Paladin;
        m_LightSlider.value = Light;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        ConversationEvents.OnConversationStart += CheckSpellsPerDay;
        ConversationEvents.OnConversationEnd += SetSpellsPerDay;
    }

    void OnDisable()
    {
        ConversationEvents.OnConversationStart -= CheckSpellsPerDay;
        ConversationEvents.OnConversationEnd -= SetSpellsPerDay;
    }

    public void CheckPlayerStats()
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

    void CheckSpellsPerDay()
    {
        DialogueLua.SetVariable("SP", SpellAvaliable);
    }

    void SetSpellsPerDay()
    {
        SpellAvaliable = DialogueLua.GetVariable("SP").AsInt;
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
