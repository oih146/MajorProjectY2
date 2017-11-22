using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDivider : MonoBehaviour {

    public bool m_AlreadyPassed = false;
    public bool m_PlayerIn;
    [Tooltip("How many hours will pass when the player moves through here")]
    public int m_hoursToPass;
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
            m_PlayerIn = true;
            GameTime.Instance.AddHours(m_hoursToPass);
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.tag == "Player" && m_PlayerIn == true)
        {
            if (PlayerMovement.Instance.m_speed > 0)
            {
                m_AlreadyPassed = true;
                ForwardBackground.Instance.StartLerp();
            }
            else
                m_AlreadyPassed = false;
            //MapCreator.instance.NewMapPosition(m_AlreadyPassed);
        }
    }
}
