using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerBounce : BumpUp {

    void OnEnable()
    { 
        StartBump();
    }

    public override void Bumping()
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

    public override void StartBump()
    {
        m_upCatcher = 0;
        if (m_switchCatcher == true)
            m_toYPos = m_initPosY + m_upAmount;
        else
            m_toYPos = m_initPosY - m_upAmount;
        m_timeSinceStart = Time.time;
        m_goingUp = true;
        m_bumpUp = true;
        m_initPosY = transform.position.y;
    }
}
