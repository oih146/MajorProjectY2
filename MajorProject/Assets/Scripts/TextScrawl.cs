using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScrawl : MonoBehaviour {

    public PlayerMovement player;
    public FadeBlack fadeScreen;
    public UnityEngine.UI.Text textBox;
    public float m_yPosLimit;
    public float m_riseSpeed;
    float m_timeSinceStart;
    float m_initYPos;
	// Use this for initialization
	void Start () {
        m_initYPos = textBox.transform.localPosition.y;
        m_timeSinceStart = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        //if (textBox.gameObject.transform.localPosition.y < m_yPosLimit - 100)
        //{
        //    Vector3 buff = textBox.transform.localPosition;
        //    buff.y = Mathf.Lerp(buff.y, m_yPosLimit, m_riseSpeed);
        //    textBox.transform.localPosition = buff;
        //}
        //else if(textBox.gameObject.transform.localPosition.y > m_yPosLimit - 100 || Input.GetButtonDown("Jump"))
        //{
        //    textBox.transform.parent.gameObject.SetActive(false);

        //    fadeScreen.m_fadeIn = true;
        //    fadeScreen.m_fading = true;
        //    gameObject.SetActive(false);
        //}
    }

    void FixedUpdate()
    {
        if (textBox.gameObject.transform.localPosition.y < m_yPosLimit - 1f)
        {
            float timeSinceLerp = Time.time - m_timeSinceStart;
            float percentageComplete = timeSinceLerp / m_riseSpeed;

            Vector3 buff = textBox.transform.localPosition;
            buff.y = Mathf.Lerp(m_initYPos, m_yPosLimit, percentageComplete);
            textBox.transform.localPosition = buff;
        }
        else if (textBox.gameObject.transform.localPosition.y > m_yPosLimit - 1f || Input.GetButtonDown("Jump"))
        {
            textBox.transform.parent.gameObject.SetActive(false);

            fadeScreen.m_fadeIn = true;
            fadeScreen.m_fading = true;
            gameObject.SetActive(false);
        }
    }
}
