using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour {

    [SerializeField]
    private GameObject m_monster;

    [SerializeField]
    private float m_lerpSpeed;

    float m_timeSinceStart;
    bool m_lerping;
    bool m_setThisActive = false;
    Vector3 m_initPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (m_lerping)
            LerpBody();
	}

    void LerpBody()
    {
        float timeInLerp = Time.time - m_timeSinceStart;
        float percentage = timeInLerp / m_lerpSpeed;

        Vector3 temp = m_monster.transform.position;
        //COllIDER MUST BE OFFSET BY LARGE AMOUNT IN ORDER TO USE GAMEOBJECT POSITION
        temp = Vector3.Lerp(m_initPos, gameObject.transform.position, percentage);
        m_monster.transform.position = temp;
        if(percentage >= 1f)
        {
            m_lerping = false;
        }
    }

    void StartLerp()
    {
        m_initPos = m_monster.transform.position;
        m_timeSinceStart = Time.time;
        m_lerping = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            if (m_setThisActive)
            {
                m_monster.SetActive(true);
                PlayerMovement.Instance.SetMovementFalse();
                StartLerp();
            }
        }
    }

    public void SetToActive()
    {
        m_setThisActive = !m_setThisActive;
    }
}
