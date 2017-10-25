using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardBackground : MonoBehaviour {

    public static ForwardBackground Instance;

    [Tooltip("Backgrounds that will be moving forward, have them in the order that you want them to descend")]
    public GameObject[] m_backgrounds;
    public int m_currentfrontBackground = 0;
    public int CurrentFrontBackground
    {
        get
        {
            return m_currentfrontBackground;
        }

        set
        {
            m_currentfrontBackground = value;
            if(m_currentfrontBackground > m_backgrounds.Length - 1)
            {
                m_currentfrontBackground = 0;
            }
        }
    }

    public float m_lerpSpeed;
    float m_timeSinceStart;
    public bool m_lerping;
    float m_initalYPos;
    public float m_lerpAmount;
    float m_toYPos;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (m_lerping)
        {
            float timeSinceLerp = Time.time - m_timeSinceStart;
            float percentage = timeSinceLerp / m_lerpSpeed;

            Vector3 temp = m_backgrounds[m_currentfrontBackground].transform.position;
            temp.y = Mathf.Lerp(m_initalYPos, m_toYPos, percentage);
            m_backgrounds[m_currentfrontBackground].transform.position = temp;
            m_backgrounds[m_currentfrontBackground].GetComponent<DualBackgrounds>().CopyOver();
            if (percentage >= 1f)
            {
                m_lerping = false;
                MoveForward();
                temp.z += m_backgrounds.Length * 0.1f;
                temp.y = m_initalYPos;
                m_backgrounds[m_currentfrontBackground].transform.position = temp;
                m_backgrounds[m_currentfrontBackground].GetComponent<DualBackgrounds>().CopyOver();
                CurrentFrontBackground++;

            }
        }
	}

    public void StartLerp()
    {
        m_initalYPos = m_backgrounds[m_currentfrontBackground].transform.position.y;
        m_toYPos = m_backgrounds[m_currentfrontBackground].transform.position.y + m_lerpAmount;
        m_timeSinceStart = Time.time;
        m_lerping = true;
    }

    public void MoveForward()
    {
        foreach(GameObject game in m_backgrounds)
        {
            Vector3 temp = game.transform.position;
            temp.z -= 0.1f;
            game.transform.position = temp;
        }
    }
}
