using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public Image m_blackScreen;
    public AudioSource m_audio;
    bool m_isLerping;
    float m_timeSinceStart;
    public float m_lerpSpeed;

    void Update()
    {
        if (m_isLerping)
        {
            float timeSinceLerp = Time.time - m_timeSinceStart;
            float percentage = timeSinceLerp / m_lerpSpeed;

            Color newColor = m_blackScreen.color;
            m_audio.volume = Mathf.Lerp(1, 0, percentage);
            newColor.a = Mathf.Lerp(0, 1, percentage);
            m_blackScreen.color = newColor;
            if(percentage >= 1f)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    void StartLerp()
    {
        m_timeSinceStart = Time.time;
        m_isLerping = true;
    }

    public void StartGame()
    {
        StartLerp();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
