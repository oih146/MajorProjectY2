using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSliderScript : MonoBehaviour {

    public UnityEngine.UI.Slider m_combatSlider;
    float m_timeSinceStart;
    public float m_speed = 0;
    float stopCatcher;
    float m_time;
    public bool m_combatActive = false;
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
            m_time += Time.deltaTime;
            float timeSinceLerp = m_time - m_timeSinceStart;
            float percentageComplete = timeSinceLerp / m_speed;

            m_combatSlider.value = Mathf.Lerp(0, m_combatSlider.maxValue, percentageComplete);
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
    }
}
