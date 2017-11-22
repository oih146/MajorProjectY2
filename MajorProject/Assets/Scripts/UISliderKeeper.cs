using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISliderKeeper : MonoBehaviour {

    public static UISliderKeeper Instance;
    public UIButtonSlider m_currentOpenSlider;
    public RectTransform m_lerpPoint;
    public float m_lerpSpeed;

    void Awake()
    {
        Instance = this;
       UIButtonSlider.m_lerpSpeed = m_lerpSpeed;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ProcessSlideRequest(UIButtonSlider newSlider)
    {
        if (m_currentOpenSlider != null)
        {
            m_currentOpenSlider.m_isOpen = !m_currentOpenSlider.m_isOpen;
            if (!m_currentOpenSlider.MaterialHandler().IsOpen)
            {
                m_currentOpenSlider.SetMatHandler();
                m_currentOpenSlider.MaterialHandler().StartLerp(false);
            }
            m_currentOpenSlider.StartLerp();
        }
        m_currentOpenSlider = newSlider;
        m_currentOpenSlider.StartLerp();
    }

    public void EmptyCurrentSlider()
    {
        m_currentOpenSlider = null;
    }

    public void CloseCurrentSlider()
    {
        if (m_currentOpenSlider != null && m_currentOpenSlider.m_isOpen == false)
        {
            m_currentOpenSlider.SetOpenClosed();
        }
    }
}
