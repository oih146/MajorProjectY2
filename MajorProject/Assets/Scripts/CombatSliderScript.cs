using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSliderScript : MonoBehaviour {

    [SerializeField]
    private ParticleSystem m_partSystem;

    public Slider m_combatSlider;
    public Image m_sliderPortraitRoot2;
    float m_timeSinceStart;
    public float m_defaultSpeed;
    public float m_timeDivider = 1;
    public float m_chargeTimeReduction = 0;
    public float m_speed = 0;
    public float m_percentageGetter = 0;
    float stopCatcher;
    float m_time;
    //public float timer;
    //public float timer2;
    public BumpUp m_bumpScript;
    public bool m_combatActive = false;
    private bool m_tempSpeedChange;
    private float m_tempSpeedValue;

    private List<StatusBase> speedEffects = new List<StatusBase>();

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
            if(value == true)
            {
                m_partSystem.Play();
            } else
            {
                m_partSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
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
            m_percentageGetter = percentageComplete;

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

        //if (take)
        //{
        //    SpeedUp(10);
        //    take = !take;
        //}
    }

    public float GetPercentageComplete()
    {
        return m_percentageGetter;
    }

    public void TakeFromTimer(float taking)
    {
        //taking /= 0.75f;
        m_timeSinceStart += taking;
        if (m_timeSinceStart > m_time)
            m_timeSinceStart = m_time;
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
        m_speed = m_defaultSpeed * 0.75f;
        m_timeDivider = 1;
        CheckForSpeedChange();
        CalculateSpeed();
    }

    public void SlowDown(float howMuch)
    {
        howMuch *= (0.27f * 2);
        m_timeDivider += (howMuch * 0.75f);
    }

    public void SpeedUp(float howMuch)
    {
        howMuch *= (0.27f * 2);
        m_timeDivider -= (howMuch * 0.75f);
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

    public void CheckForSpeedChange()
    {
        for(int i = 0; i < speedEffects.Count; i++)
        {
            StatusBase stat = speedEffects[i];
            if(stat.TakeAndCheckActive())
            {
                speedEffects.Remove(stat);
                i--;
            }
            else
            {
                if (stat.m_effectType == eEffects.SpeedIncrease)
                    m_speed += -stat.Strength;
                else
                    m_speed += stat.Strength;
            }
        }

        if (m_speed < 1)
            m_speed = 1;
    }

    public void SetTemporarySpeedValue(StatusBase effect)
    {
        if (effect.m_effectType == eEffects.SpeedIncrease)
            SpeedUp(effect.Strength);
        else
            SlowDown(effect.Strength);
        StatusBase newEffect = (StatusBase)ScriptableObject.CreateInstance("StatusBase");
        newEffect.Strength = effect.Strength;
        newEffect.TimeActive = effect.TimeActive;
        newEffect.m_effectType = effect.m_effectType;
        speedEffects.Add(newEffect);

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

    public void ResetTempSpeedArray()
    {
        speedEffects.Clear();
    }
}
