using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationEvents : MonoBehaviour {

    public delegate void OnConversationEvent();
    public static event OnConversationEvent OnConversationEnd;
    public static event OnConversationEvent OnConversationStart;
    public static event OnConversationEvent AfterPlayerResponse;
    public static int m_playerResponseNumber = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CallConversationEndEvents()
    {
        if(OnConversationEnd != null)
        {
            OnConversationEnd();
        }
    }

    public void CallConcersationStartEvents()
    {
        m_playerResponseNumber = 0;
        if (OnConversationStart != null)
        {
            OnConversationStart();
        }
    }

    public void CallAfterPlayerResponseEvents()
    {
        m_playerResponseNumber++;
        if(AfterPlayerResponse != null)
        {
            AfterPlayerResponse();
        }
    }

    public void ResetTrigger()
    {

    }
}
