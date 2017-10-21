using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonSlider : MonoBehaviour {

    private Vector3 m_originalPos;
    public bool m_isOpen = false;
    public bool m_lerping = false;
    private float m_timeSinceStart = 0;
    public static float m_lerpSpeed;
    private float m_percentage = 1;
    private float m_time = 0;
    Animator m_animator;
    RectTransform rect;

    public Transform m_LerpPos;

	// Use this for initialization
	void Start () {
        m_animator = gameObject.GetComponent<Animator>();
        rect = GetComponent<RectTransform>();
        m_originalPos = rect.position;

	}

    void Update()
    {
        if(m_lerping)
        {
            m_time += Time.deltaTime;
            float timeInLerp = m_time - m_timeSinceStart;
            m_percentage = timeInLerp / m_lerpSpeed;
            rect.position = Vector3.Lerp(
                                (m_isOpen ? m_LerpPos.position : m_originalPos),
                                (m_isOpen ? m_originalPos : m_LerpPos.position),
                                m_percentage
                                );
            if (m_percentage >= 1f)
            {
                m_lerping = false;
            }
        }
    }

    public void StartLerp()
    {
        m_time = Time.time;

        m_timeSinceStart = Time.time;
        if (m_percentage < 1)
            m_time += ((1 - m_percentage) * m_lerpSpeed);

        m_lerping = true;
    }

    public void SetOpenClosed()
    {
        m_isOpen = !m_isOpen;
        if (!m_isOpen)
            UISliderKeeper.Instance.ProcessSlideRequest(this);
        else
        {
            UISliderKeeper.Instance.EmptyCurrentSlider();
            StartLerp();
        }


        //if (m_isOpen)
        //    m_animator.Play("OpenState");
        //else
        //    m_animator.Play("ClosedState");

    }

    public void SetOpenState()
    {
        m_animator.Play("OpenState");
    }

    public void SetClosedState()
    {
        float playback = 1; 
        if(m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
           playback = m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        m_animator.Play("ClosedState", 0, 1 - playback);
    }

    public Animator GetAnimator()
    {
        return m_animator;
    }
}
