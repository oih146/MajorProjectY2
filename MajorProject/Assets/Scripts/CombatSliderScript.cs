using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSliderScript : MonoBehaviour {

    public Slider m_combatSlider;
    public Image m_sliderPortraitRoot2;
    float m_timeSinceStart;
    public float m_defaultSpeed;
    public float m_timeDivider = 1;
    public float m_chargeTimeReduction = 0;
    public float m_speed = 0;
    float stopCatcher;
    float m_time;
    //public float timer;
    //public float timer2;
    public BumpUp m_bumpScript;
    public bool m_combatActive = false;
    private bool m_tempSliderSpeedIncrease;
    private float m_tempSpeedDecreaseValue;
    //public bool take = false;
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

    void Awake()
    {
        m_combatSlider = GetComponent<Slider>();
    }

    // Use this for initialization
    void Start () {

        m_combatSlider.value = 0;
        //Restart();
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

        //if(take)
        //{
        //    TakeFromTimer(1);
        //    take = !take;
        //    CombatActive = false;
        //}
    }

    public void TakeFromTimer(float taking)
    {
        m_timeSinceStart += taking;
        float timeSinceLerp = m_time - m_timeSinceStart;
        float percentageComplete = timeSinceLerp / m_speed;

        m_combatSlider.value = Mathf.Lerp(0, m_combatSlider.maxValue, percentageComplete);
        m_bumpScript.StartBump();
    }

    public void Restart()
    {
        m_time = Time.time;
        m_timeSinceStart = Time.time;
        m_combatSlider.value = 0;
        m_speed = m_defaultSpeed;
        CalculateSpeed();
        if (m_tempSliderSpeedIncrease)
            TemporarySliderSpeedDecrease();
        m_timeDivider = 1;
    }

    public void SlowDown(float howMuch)
    {
        if (howMuch < 0.001)
            howMuch = 0.001f;
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

    public void TemporarySliderSpeedDecrease()
    {
        m_tempSliderSpeedIncrease = false;
        m_speed += 2;
    }

    public void SetTemporarySpeedDecrease(float amount)
    {
        if(m_tempSpeedDecreaseValue < amount)
            m_tempSpeedDecreaseValue = amount;
        m_tempSliderSpeedIncrease = true;
    }
    
    void CalculateSpeed()
    {
        m_speed /= 0.73f;
    }

    public void SetPortraitBackgroundColor(Color color)
    {
        m_sliderPortraitRoot2.color = color;
        m_sliderPortraitRoot2.GetComponent<Image>().color = color;
    }
}
