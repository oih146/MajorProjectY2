using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour {

    public GameObject m_disarmedIcon;
    public GameObject m_burnedIcon;
    public GameObject m_surrenderedIcon;
    public GameObject m_incapacitatedIcon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject GetSurrendered()
    {
        return m_surrenderedIcon;
    }

    public GameObject GetDisarmed()
    {
        return m_disarmedIcon;
    }

    public GameObject GetIncapacitated()
    {
        return m_incapacitatedIcon;
    }

    public GameObject GetBurned()
    {
        return m_burnedIcon;
    }
}
