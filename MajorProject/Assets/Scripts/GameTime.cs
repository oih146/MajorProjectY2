using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class GameTime : MonoBehaviour {

    static UnityEngine.UI.Text m_timeText;

    static int m_hours;
    public static int Hours
    {
        get
        {
            return m_hours;
        }
    }
    static int m_days;
    public static int Days
    {
        get
        {
            return m_days;
        }
    }

    void Awake()
    {
        m_timeText = GetComponent<UnityEngine.UI.Text>();
    }

	// Use this for initialization
	void Start () {
        UpdateTimeText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        ConversationEvents.OnConversationStart += SetDialogueVariables;
        ConversationEvents.AfterPlayerResponse += UpdateDialogueVariables;
    }

    void OnDisable()
    {
        ConversationEvents.OnConversationStart -= SetDialogueVariables;
        ConversationEvents.AfterPlayerResponse -= UpdateDialogueVariables;
    }

    public static void AddHours(int amount)
    {
        m_hours += amount;
        int toAdd = 0;
        while(m_hours > 23)
        {
            m_hours -= 24;
            toAdd++;

        }
        if (toAdd > 0)
            AddDays(toAdd);
        else
            UpdateTimeText();
    }

    public static void AddDays(int amount)
    {
        m_days += amount;
        UpdateTimeText();
    }

    //For Sequence on Dialogue System
    //It doesn't work with static functions
    //public void addhours(string amount)
    //{
    //    int toAdd;
    //    int.TryParse(amount, out toAdd);
    //    m_hours += toAdd;
    //    //repurpose
    //    toAdd = 0;
    //    while (m_hours > 23)
    //    {
    //        m_hours -= 24;
    //        toAdd++;
    //    }
    //    if(toAdd > 0)
    //        addhours(toAdd.ToString());
    //    else
    //        UpdateTimeText();
    //}

    //For Sequence on Dialogue System
    //It doesn't work with static functions
    //public void addDays(string amount)
    //{
    //    int toAdd;
    //    int.TryParse(amount, out toAdd);
    //    m_days += toAdd;
    //    UpdateTimeText();
    //}

    public static int[] GetTime()
    {
        int[] timeArray = new int[4];
        timeArray[2] = m_hours;
        timeArray[3] = m_days;
        return timeArray;
    }

    static void UpdateTimeText()
    {
        m_timeText.text = "Days: " + Days + "\nHours: " + Hours;
    }



    public void UpdateDialogueVariables()
    {
        if(DialogueLua.GetVariable("Hours").AsInt > Hours)
        {
            AddHours(DialogueLua.GetVariable("Hours").AsInt - Hours);
            DialogueLua.SetVariable("Hours", Hours);
        }
    }

    public void SetDialogueVariables()
    {
        DialogueLua.SetVariable("Hours", Hours);
    }
}
