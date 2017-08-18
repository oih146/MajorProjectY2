using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapFrogScript : MonoBehaviour {

    //target should always be camera
    public GameObject m_target;
    //first rain should always be one furthest right
    public GameObject[] m_rainHolders;
    public bool m_FirstRain;
    public bool m_DoesMove;
    public float m_MoveSpeed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(m_DoesMove)
        {
            for(int i = 0; i < m_rainHolders.Length; i++)
            {
                m_rainHolders[i].transform.Translate(Vector3.right * m_MoveSpeed);
            }
        }
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
        else if (m_target.transform.position.x < ((!m_FirstRain) ? m_rainHolders[0].transform.position.x : m_rainHolders[1].transform.position.x))
        {
            Vector3 backCloudTemp = ((!m_FirstRain) ? m_rainHolders[0].transform.position : m_rainHolders[1].transform.position);
            Vector3 frontCloudTemp = ((m_FirstRain) ? m_rainHolders[0].transform.position : m_rainHolders[1].transform.position);
            float rain1 = backCloudTemp.x;
            float rain2 = frontCloudTemp.x;
            Vector3 temp = backCloudTemp;
            float distance = (m_FirstRain) ? rain1 - rain2 : rain2 - rain1;
            distance = Mathf.Abs(distance);
            temp.x = backCloudTemp.x - distance;
            frontCloudTemp = temp;
            m_FirstRain = !m_FirstRain;
            if (!m_FirstRain)
            {
                m_rainHolders[0].transform.position = backCloudTemp;
                m_rainHolders[1].transform.position = frontCloudTemp;
            }
            else
            {
                m_rainHolders[0].transform.position = frontCloudTemp;
                m_rainHolders[1].transform.position = backCloudTemp;
            }
        }
    }
}
