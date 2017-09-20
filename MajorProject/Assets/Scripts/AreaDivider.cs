using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDivider : MonoBehaviour {

    public bool m_AlreadyPassed = false;
    public bool m_PlayerIn;
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
            GameTime.AddHours(15);
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.tag == "Player" && m_PlayerIn == true)
        {
            if (PlayerMovement.m_speed > 0)
            {
                m_AlreadyPassed = true;
                ForwardBackground.Instance.StartLerp();
            }
            else
                m_AlreadyPassed = false;
            MapCreator.instance.NewMapPosition(m_AlreadyPassed);
        }
    }
}
