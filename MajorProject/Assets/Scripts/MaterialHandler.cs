using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHandler : MonoBehaviour {

    public UnityEngine.UI.Image image;

    float m_lerpSpeed;

    bool m_fadingIn;
    bool m_lerping;
    float m_timeSinceStart;
    float m_startValue;

    bool m_isOpen = true;
    public bool IsOpen
    {
        get
        {
            return m_isOpen;
        }
        set
        {
            m_isOpen = value;
        }
    }

    void Start()
    {
        Material mat = Instantiate(image.material);
        image.material = mat;
    }

    void Update()
    {
        if (m_lerping)
            LerpBody();
    }

    void LerpBody()
    {
        float timeInLerp = Time.time - m_timeSinceStart;
        float percentage = timeInLerp / m_lerpSpeed;

        float temp = Mathf.Clamp(Mathf.Lerp(m_startValue, m_fadingIn ? 1 : 0, percentage), 0, 1);
        SetMaterialValues(temp);

        if(percentage >= 1f)
        {
            SetMaterialValues(m_fadingIn ? 1 : 0);
            m_lerping = false;
        }
    }

    public void StartLerp(bool isFadingIn)
    {
        m_fadingIn = isFadingIn;
        m_startValue = image.material.GetFloat("_SparkleAmp");
        m_lerpSpeed = Mathf.Abs(m_fadingIn ? 1 - m_startValue : 0 + m_startValue);
        m_timeSinceStart = Time.time;
        m_lerping = true;
    }

    public void IsHovering(bool status)
    {
        //Material mat = Instantiate(image.material);
        if (status)
        {
            if(IsOpen)
                StartLerp(true);
        }
        else
        {
            if(IsOpen)
                StartLerp(false);
        }
        //image.material = mat;
    }

    void SetMaterialValues(float value)
    {
        image.material.SetFloat("_SparkleAmp", value);
        image.material.SetFloat("_CloudAmp", value);
        //mat.SetFloat("_SparkleAmp", 0);
        //mat.SetFloat("_CloudAmp", 0);
    }

    public void SetOpen(bool status)
    {
        IsOpen = status;
    }

    public void SetMaterialOn()
    {
        SetMaterialValues(1);
    }

    public void SetMaterialOff()
    {
        SetMaterialValues(0);
    }
}
