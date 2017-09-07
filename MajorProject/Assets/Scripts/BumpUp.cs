using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpUp : MonoBehaviour {

    public bool m_bumpUp;
    public bool m_switchCatcher;
    public bool m_goingUp;
    [Tooltip("How many times do you want the portrait to bounce?")]
    public int m_timesUp;
    private int m_upCatcher;
    public float m_upAmount;
    float m_timeSinceStart;
    public float m_speed;
    float m_initPosY;
    float m_toYPos;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (m_bumpUp)
        {
            float timeSinceLerp = Time.time - m_timeSinceStart;
            float percentageComplete = timeSinceLerp / m_speed;

            Vector3 vec = transform.position;
            vec.y = Mathf.Lerp((m_goingUp) ? m_initPosY : m_toYPos, (!m_goingUp) ? m_initPosY : m_toYPos, percentageComplete);
            transform.position = vec;
            if (percentageComplete >= 1)
            {
                if (m_goingUp == m_switchCatcher)
                {
                    m_timeSinceStart = Time.time;
                    m_goingUp = !m_switchCatcher;
                }
                else
                {
                    if (m_timesUp == m_upCatcher)
                        m_bumpUp = false;
                    else
                    {
                        m_upCatcher++;
                        m_timeSinceStart = Time.time;
                        m_goingUp = m_switchCatcher;
                    }
                }
            }
        }
    }

    public void StartBump()
    {
        m_initPosY = transform.position.y;
        if (m_switchCatcher == true)
            m_toYPos = m_initPosY + m_upAmount;
        else
            m_toYPos = m_initPosY - m_upAmount;
        m_timeSinceStart = Time.time;
        m_goingUp = m_switchCatcher;
        m_bumpUp = true;
    }
}
