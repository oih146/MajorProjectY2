using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapFrogScript : MonoBehaviour {

    //target should always be camera
    public GameObject m_target;
    //first rain should always be one furthest right
    public GameObject[] m_rainHolders;
    public bool m_FirstRain;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LeapFrog();
	}

    public void LeapFrog()
    {
        if(m_target.transform.position.x > ((m_FirstRain) ? m_rainHolders[0].transform.position.x : m_rainHolders[1].transform.position.x))
        {
            float rain1 = m_rainHolders[0].transform.position.x;
            float rain2 = m_rainHolders[1].transform.position.x;
            float distance = (m_FirstRain) ? rain1 - rain2 : rain2 - rain1;
            distance = Mathf.Abs(distance);
            if(m_FirstRain)
            {
                Vector3 temp = m_rainHolders[0].transform.position;
                temp.x = m_rainHolders[0].transform.position.x + distance;
                m_rainHolders[1].transform.position = temp;
            }
            else
            {
                Vector3 temp = m_rainHolders[1].transform.position;
                temp.x = m_rainHolders[1].transform.position.x + distance;
                m_rainHolders[0].transform.position = temp;
            }
            m_FirstRain = !m_FirstRain;
        }
    }
}
