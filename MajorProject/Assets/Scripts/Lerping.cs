using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerping : MonoBehaviour {

    float m_timeSinceStart;
    public float m_lerpSpeed;
    float m_initalfloat;
    float m_destinationfloat;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	public float Lerp () {
        float timeLerping = Time.time - m_timeSinceStart;
        float percentage = timeLerping / m_lerpSpeed;

        float temp = Mathf.Lerp(m_initalfloat, m_destinationfloat, percentage);
        if(percentage >= 1)
        {
            //enabled = false;
        }
        return temp;
	}

    public void StartLerp(float speed, float current, float destination)
    {
        m_initalfloat = current;
        m_destinationfloat = destination;
        m_lerpSpeed = speed;
        m_timeSinceStart = Time.time;
        //enabled = true;
    }
}
