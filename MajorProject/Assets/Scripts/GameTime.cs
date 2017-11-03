using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class GameTime : MonoBehaviour {

    public static GameTime Instance;

    public GameObject m_lightSource;
    UnityEngine.UI.Text m_timeText;

    float m_timeSinceStart;
    public float m_lerpSpeed;
    bool m_lerping;
    float m_initalXRot;
    float m_toXRot;

    int m_hours;
    public int Hours { get { return m_hours; } }
    int m_days;
    public int Days { get { return m_days; } }

    void Awake()
    {
        Instance = this;
        m_timeText = GetComponent<UnityEngine.UI.Text>();
    }

	// Use this for initialization
	void Start () {
        UpdateTimeText();
        AddHours(14);
	}
	
	// Update is called once per frame
	void Update () {
        if (m_lerping)
            LerpBody();
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
    
    void LerpBody()
    {
        float timeInLerp = Time.time - m_timeSinceStart;
        float percentage = timeInLerp / m_lerpSpeed;

        Vector3 temp =  new Vector3 (m_lightSource.transform.eulerAngles.x, 0 , 0);
        temp.x = Mathf.Lerp(m_initalXRot, m_toXRot, percentage);
        //temp.x = (temp.x > 360 ? temp.x / 360 :  temp.x + 0);
        m_lightSource.transform.eulerAngles = temp;
        if(percentage >= 1f)
        {
            m_lerping = false;
        }
    }

    void StartLerp(float newXRot)
    {
        m_timeSinceStart = Time.time;
        m_initalXRot = m_lightSource.transform.eulerAngles.x;
        m_toXRot =  m_initalXRot + newXRot;
        m_lerping = true;
    }

    public void AddHours(int amount)
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
        {
            StartLerp(((amount / 24.0f) * 360) /5);
            UpdateTimeText();
        }
    }

    public void AddDays(int amount)
    {
        m_days += amount;
        StartLerp(((amount / 24.0f) * 360) / 5);
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

    public int[] GetTime()
    {
        int[] timeArray = new int[4];
        timeArray[2] = m_hours;
        timeArray[3] = m_days;
        return timeArray;
    }

    void UpdateTimeText()
    {
        m_timeText.text = "Days: " + Days + "\nHours: " + Hours;
    }

    void UpdateLight(float amountToRot)
    {
        m_lightSource.transform.eulerAngles = new Vector3(m_lightSource.transform.eulerAngles.x + amountToRot, 0, 0);
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
