using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    //target should always be camera
    public GameObject m_target;
    //first rain should always be one furthest right
    public GameObject[] m_rainHolders;
    public float m_moveSpeed;
    public bool m_moveRight;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (PlayerMovement.m_amMoving && m_target.transform.parent != null)
        {
            Vector3 buff = gameObject.transform.position;
            buff.x += (PlayerMovement.m_speed * m_moveSpeed);
            gameObject.transform.position = buff;

            buff = m_rainHolders[1].transform.position;
            buff.x += (PlayerMovement.m_speed * m_moveSpeed);
            m_rainHolders[1].transform.position = buff;
        }
	}

    void Update()
    {
        if(PlayerMovement.m_amMoving || m_target.transform.parent == null)
            LeapFrog();
    }

    public void LeapFrog()
    {
        if (m_target.transform.position.x > ((m_moveRight) ? m_rainHolders[0].transform.position.x : m_rainHolders[1].transform.position.x))
        {
            float rain1 = m_rainHolders[0].transform.position.x;
            float rain2 = m_rainHolders[1].transform.position.x;
            float distance = (m_moveRight) ? rain1 - rain2 : rain2 - rain1;
            distance = Mathf.Abs(distance);
            if (m_moveRight)
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
            m_moveRight = !m_moveRight;
        }
    }
}
