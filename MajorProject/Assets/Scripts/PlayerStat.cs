using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class PlayerStat : CharacterStatSheet {

    public AlignmentLines m_alignmentLines;
    public int m_light;
    public int Light
    {
        get
        {
            return m_light;
        }

        set
        {
            if (value < 0)
                m_light = 0;
            else if (value > 100)
                m_light = 100;
            else
                m_light = value;
            if (m_light - value < 0)
                NotificationManager.Instance.AddToList(m_alignmentLines.GetLightLine(30));
            else
                NotificationManager.Instance.AddToList(m_alignmentLines.GetLightLine(0));
            m_light = value;
            m_LightSlider.value = m_light;
            if (value < 0)
                m_light = 0;
            else if (value > 100)
                m_light = 100;

        }
    }
    public int m_law;
    public int Law
    {
        get
        {
            return m_law;
        }

        set
        {
            if (m_law - value < 0)
                NotificationManager.Instance.AddToList(m_alignmentLines.GetLawLine(30));
            else
                NotificationManager.Instance.AddToList(m_alignmentLines.GetLawLine(0));
            m_law = value;
            m_LawSlider.value = m_law;
            if (value < 0)
                m_law = 0;
            else if (value > 100)
                m_law = 100;

        }
    }
    public UnityEngine.UI.Slider m_LightSlider;
    public UnityEngine.UI.Slider m_LawSlider; 
    public float m_charisma;
    public bool m_inBattle;
    public static int m_maxSpellsPerDay = 1;
    public int MaxSpells
    {
        get
        {
            return m_maxSpellsPerDay;
        }

        private set
        {
            m_maxSpellsPerDay = value;
        }
    }
    public GameObject spellRoot;
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
            for (int i = 0; i < spellRoot.transform.childCount; i++)
            {
                if (m_spellsAvaliable > i)
                    spellRoot.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                else
                    spellRoot.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    public CharacterStatSheet[] m_allies = new CharacterStatSheet[1];

    // Use this for initialization
    void Start () {
        Starts();
        //AddToOrderChaos(5);
        //AddToOrderChaos(-30);
        //AddToOrderChaos(-30);
        NotificationManager.Instance.PushNotificationBlock();
        GenerateWillPower();
        m_spellsAvaliable = MaxSpells;
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

    public void GenerateWillPower()
    {
        MaxSpells += GetStatistics().GetWillPower() / 2;
        if (MaxSpells > 6)
            MaxSpells = 6;
        //SetSpellCharges(true);
    }

    public void SetSpellCharges(bool status)
    {
        int spellmax = MaxSpells;
        GameObject spellChargeRoot = BattleMenuScript.Instance.SpellChargeRoot;
        for (int i = 0; i < spellChargeRoot.transform.childCount; i++, spellmax--)
        {
            if (spellmax > 0)
                spellChargeRoot.transform.GetChild(i).gameObject.SetActive(status);
        }
    }

    public void CheckPlayerStats()
    {
       SetAlignment();
    }

    void SetAlignment()
    {
        bool pushing = false;
        if (DialogueLua.GetVariable("Law").AsFloat != 0)
        {
            AddToOrderChaos(DialogueLua.GetVariable("Law").AsInt);
            pushing = true;
        }
        DialogueLua.SetVariable("Law", 0);
        if (DialogueLua.GetVariable("Light").AsFloat != 0)
        {
            AddToGoodEvil(DialogueLua.GetVariable("Light").AsInt);
            pushing = true;
        }
        if (pushing)
            NotificationManager.Instance.PushNotificationBlock();
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

    public void AddToOrderChaos(int amountToAdd)
    {
        Law += amountToAdd * 10;
    }

    public void AddToGoodEvil(int amountToAdd)
    {
        Light += amountToAdd * 10;
    }
    public override void UpdateEffects()
    {
        base.UpdateEffects();

    }

    public override void ResetEffects()
    {
        base.ResetEffects();
    }

    public override void AfterAttackConsequences(UseConsequences conseq)
    {
        conseq.ApplyConsequences(this, UseConsequences.ConsequenceApplication.AfterUse);
    }

    public override void OnKillConsequences(UseConsequences conseq)
    {
        conseq.ApplyConsequences(this, UseConsequences.ConsequenceApplication.OnKill);
    }

}
