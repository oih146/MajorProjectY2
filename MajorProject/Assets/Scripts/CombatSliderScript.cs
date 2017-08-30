using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSliderScript : MonoBehaviour {

    public UnityEngine.UI.Slider m_combatSlider;
    float m_timeSinceStart;
    public float m_defaultSpeed;
    public float m_timeDivider = 1;
    public float m_chargeTimeReduction = 0;
    public float m_speed = 0;
    float stopCatcher;
    float m_time;
    //public float timer;
    //public float timer2;
    public bool m_combatActive = false;
    public bool slow = false;
    public bool CombatActive
    {
        get
        {
            return m_combatActive;
        }

        set
        {
            //stopCatcher = m_combatSlider.value;
            m_combatActive = value;
        }
    }
    // Use this for initialization
    void Start () {
        m_combatSlider = GetComponent<UnityEngine.UI.Slider>();
        m_combatSlider.value = 0;
        Restart();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_combatActive)
        {
            m_time += Time.deltaTime / m_timeDivider;
            float timeSinceLerp = m_time - m_timeSinceStart;
            float percentageComplete = timeSinceLerp / m_speed;

            m_combatSlider.value = Mathf.Lerp(0, m_combatSlider.maxValue, percentageComplete);
            //timer += Time.deltaTime;
            //if (percentageComplete >= 0.73 && slow != true)
            //{
            //    SlowDown(12);
            //    slow = true;
            //}
            //if (slow != true)
            //    timer2 += Time.deltaTime;
            //if (percentageComplete >= 1f)
            //    m_combatActive = false;
        }
    }

    public void TakeFromTimer(float taking)
    {
        m_timeSinceStart += taking;
        float timeSinceLerp = m_time - m_timeSinceStart;
        float percentageComplete = timeSinceLerp / m_speed;

        m_combatSlider.value = Mathf.Lerp(0, m_combatSlider.maxValue, percentageComplete);
    }

    public void Restart()
    {
        m_time = Time.time;
        m_timeSinceStart = Time.time;
        m_combatSlider.value = 0;
        m_speed = m_defaultSpeed;
        CalculateSpeed();
        m_timeDivider = 1;
    }

    public void SlowDown(float howMuch)
    {
        howMuch *= (0.27f * 2);
        m_timeDivider = howMuch;
    }

    public void ChargeTimeReduction(float chargeTimeReduct)
    {
        m_chargeTimeReduction = chargeTimeReduct;
    }

    public void DecreaseBaseSliderSpeed(float addition)
    {
        m_defaultSpeed -= addition;
        m_speed = m_defaultSpeed;
        CalculateSpeed();
    }
    
    void CalculateSpeed()
    {
        m_speed /= 0.73f;
    }
}
