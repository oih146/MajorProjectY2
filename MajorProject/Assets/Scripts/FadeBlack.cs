using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBlack : MonoBehaviour {

    public PlayerMovement player;
    public UnityEngine.UI.Image blackScreen;
    static bool m_fadeIn = false;
    static bool m_fading = false;
    public float m_fadeSpeed = 5;
    static float m_timeSinceStart;
    static float m_alphaInit;
    public static FadeBlack instance;
    // Use this for initialization
    void Start () {
        m_alphaInit = blackScreen.color.a;
        m_timeSinceStart = Time.time;
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_fading && ((m_fadeIn) ? blackScreen.color.a > 0.001f : blackScreen.color.a < 0.999f))
        {
            float timeLerping = Time.time - m_timeSinceStart;
            float percentage = timeLerping / m_fadeSpeed;
            Color buffCol = blackScreen.color;
            buffCol.a = Mathf.Lerp(m_alphaInit, (m_fadeIn) ? 0 : 1, percentage);
            blackScreen.color = buffCol;
        }
        else if (m_fading == true && ((m_fadeIn) ? blackScreen.color.a <= 0.001f : blackScreen.color.a >= 0.999f))
        {
            m_fading = false;
            player.SetMovement(true);
            if (m_fadeIn == true)
                gameObject.SetActive(false);
        }
    }

    public static void Activate(bool fadeIn)
    {
        m_fadeIn = fadeIn;
        m_alphaInit = instance.blackScreen.color.a;
        SetTime();
        m_fading = true;
        instance.gameObject.SetActive(true);
        
    }

    public static void SetTime()
    {
        m_timeSinceStart = Time.time;
    }
}
