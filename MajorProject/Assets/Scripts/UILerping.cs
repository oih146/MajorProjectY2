using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILerping : MonoBehaviour {

    public Transform m_targetPos;

    private Vector3 m_targetPosition;
    private bool m_lerping = false;
    private float m_timeSinceStart;
    public float m_lerpSpeed;
    private Vector3 m_initalPos;
    private bool m_atInitalPos = true;

	// Use this for initialization
	void Start () {
        m_initalPos = gameObject.transform.position;
        m_targetPosition = m_targetPos.position;
        StartLerp();
	}

    void OnEnable()
    {
        TurnBasedScript.OnBattleStart += StartLerp;
        TurnBasedScript.OnBattleEnd += StartLerp;
    }

    void OnDisable()
    {
        TurnBasedScript.OnBattleStart -= StartLerp;
        TurnBasedScript.OnBattleEnd -= StartLerp;
    }
	
	// Update is called once per frame
	void Update () {
		if(m_lerping)
        {
            float timeInLerp = Time.time - m_timeSinceStart;
            float percentage = timeInLerp / m_lerpSpeed;

            gameObject.transform.position = Vector3.Lerp((m_atInitalPos ? m_initalPos : m_targetPosition), (!m_atInitalPos ? m_initalPos : m_targetPosition), percentage);

            if(percentage >= 1f)
            {
                m_lerping = false;
                m_atInitalPos = !m_atInitalPos;
            }
        }
	}

    public void StartLerp()
    {
        m_timeSinceStart = Time.time;
        m_lerping = true;
    }
}
