using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAcross : MonoBehaviour {

    bool m_lerping;
    float m_timeSinceStart;

    public float m_lerpSpeed;
    public GameObject m_player;
    float m_originalX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (m_lerping)
            LerpBody();
	}

    public void StartLerp()
    {
        m_originalX = Camera.main.transform.position.x;
        m_timeSinceStart = Time.time;
        m_lerping = true;
    }

    public void LerpBody()
    {
        float timeInLerp = Time.time - m_timeSinceStart;
        float percentage = timeInLerp / m_lerpSpeed;

        Vector3 temp = Camera.main.transform.position;
        temp.x = Mathf.Lerp(m_originalX, m_player.transform.position.x, percentage);
        Camera.main.transform.position = temp;

        if (percentage >= 1f)
        {
            m_lerping = false;
        }
    }
}
