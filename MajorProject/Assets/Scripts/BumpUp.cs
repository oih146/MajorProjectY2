using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpUp : MonoBehaviour {

    public bool m_bumpUp;
    public bool m_switchCatcher;
    public bool m_goingUp;
    [Tooltip("How many times do you want the portrait to bounce?")]
    public int m_timesUp;
    protected int m_upCatcher;
    public float m_upAmount;
    protected float m_timeSinceStart;
    public float m_speed;
    protected float m_initPosY;
    protected float m_toYPos;
	// Use this for initialization
	void Start () {
        m_initPosY = transform.position.y;
        Starts();
    }
	
	// Update is called once per frame
	void Update () {
        Bumping();
        Updates();
    }

    public virtual void Bumping()
    {
        if (m_bumpUp)
        {
            float timeSinceLerp = Time.time - m_timeSinceStart;
            float percentageComplete = timeSinceLerp / m_speed;

            Vector3 vec = transform.position;
            vec.y = Mathf.Lerp((m_goingUp) ? m_initPosY : m_toYPos, (!m_goingUp) ? m_initPosY : m_toYPos, percentageComplete);
            transform.position = vec;
            if (percentageComplete >= 1)
            {
                if (m_goingUp == true)
                {
                    m_timeSinceStart = Time.time;
                    m_goingUp = false;
                }
                else
                {
                    if (m_timesUp == m_upCatcher)
                        m_bumpUp = false;
                    else
                    {
                        m_upCatcher++;
                        m_timeSinceStart = Time.time;
                        m_goingUp = true;
                    }
                }
            }
        }
    }

    public virtual void StartBump()
    {
        m_upCatcher = 0;
        if (m_switchCatcher == true)
            m_toYPos = m_initPosY + m_upAmount;
        else
            m_toYPos = m_initPosY - m_upAmount;
        m_timeSinceStart = Time.time;
        m_goingUp = true;
        m_bumpUp = true;
    }

    public void StopBump()
    {
        m_bumpUp = false;
    }

    public virtual void Starts() { }
    public virtual void Updates() { }

}
