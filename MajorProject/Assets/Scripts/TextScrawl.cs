using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScrawl : MonoBehaviour {

    public PlayerMovement player;
    public FadeBlack fadeScreen;
    public UnityEngine.UI.Text textBox;
    public float m_yPosLimit;
    public float m_riseSpeed;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (textBox.gameObject.transform.localPosition.y < m_yPosLimit - 30)
        {
            Vector3 buff = textBox.transform.localPosition;
            buff.y = Mathf.Lerp(buff.y, m_yPosLimit, m_riseSpeed);
            textBox.transform.localPosition = buff;
        }
        else
        {
            textBox.transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false);
            fadeScreen.m_fadeIn = true;
            fadeScreen.m_fading = true;
        }
    }
}
