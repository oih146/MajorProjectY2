using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScrawl : MonoBehaviour {

    public PlayerMovement player;
    public UnityEngine.UI.Text textBox;
    public UnityEngine.UI.Image blackScreen;
    public bool fadeIn = false;
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
            fadeIn = true;
        }
        if (fadeIn && blackScreen.color.a > 0.001f)
        {
            Color buffCol = blackScreen.color;
            buffCol.a = Mathf.Lerp(blackScreen.color.a, 0, 5 * Time.deltaTime);
            blackScreen.color = buffCol;
        }
        else if (blackScreen.color.a <= 0.001f)
        {
            player.SetMovement(true);
            gameObject.SetActive(false);
        }
    }
}
