using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonSlider : MonoBehaviour {

    [SerializeField]
    private MaterialHandler m_matHandler;

    public bool m_isOpen = true;
    public bool m_lerping = false;
    public UnityEngine.UI.Button m_tabCloser;
    private float m_timeSinceStart = 0;
    public static float m_lerpSpeed;
    private float m_percentage = 1;
    private float m_time = 0;
    Animator m_animator;
    RectTransform rect;

    public Transform m_originalPos;
    public Transform m_LerpPos;

	// Use this for initialization
	void Start () {
        m_animator = gameObject.GetComponent<Animator>();
        rect = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        m_tabCloser.onClick.AddListener(SetOpenClosed);
    }

    void OnDisable()
    {
        m_tabCloser.onClick.RemoveListener(SetOpenClosed);
    }

    void Update()
    {
        if(m_lerping)
        {
            m_time += Time.deltaTime;
            float timeInLerp = m_time - m_timeSinceStart;
            m_percentage = timeInLerp / m_lerpSpeed;
            Vector3 temp = rect.position;
            temp.y = Mathf.Lerp(
                                (m_isOpen ? m_LerpPos.position.y : m_originalPos.position.y),
                                (m_isOpen ? m_originalPos.position.y : m_LerpPos.position.y),
                                m_percentage
                                );
            rect.position = temp;
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
        SetMatHandler();
        if (!m_isOpen)
        {
            UISliderKeeper.Instance.ProcessSlideRequest(this);
        }
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

    public MaterialHandler MaterialHandler()
    {
        return m_matHandler;
    }

    public void SetMatHandler()
    {
        m_matHandler.SetOpen(m_isOpen);
    }

    public void TurnOffMatHandler()
    {
        m_matHandler.SetMaterialOff();
    }
}
