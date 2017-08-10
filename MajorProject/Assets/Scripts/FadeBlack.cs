using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBlack : MonoBehaviour {

    public PlayerMovement player;
    public UnityEngine.UI.Image blackScreen;
    public bool m_fadeIn = false;
    public bool m_fading = false;
    public float m_fadeSpeed = 5;
    public static FadeBlack instance;
    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_fading && ((m_fadeIn) ? blackScreen.color.a > 0.001f : blackScreen.color.a < 254.999f))
        {
            Color buffCol = blackScreen.color;
            buffCol.a = Mathf.Lerp(blackScreen.color.a, (m_fadeIn) ? 0 : 1, m_fadeSpeed * Time.deltaTime);
            blackScreen.color = buffCol;
        }
        else if (((m_fadeIn) ? blackScreen.color.a <= 0.001f : blackScreen.color.a >= 254.999f))
        {
            player.SetMovement(true);
            gameObject.SetActive(false);
        }
    }

    public static void Activate()
    {
        instance.gameObject.SetActive(true);
    }
}
