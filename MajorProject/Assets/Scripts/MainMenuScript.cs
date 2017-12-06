using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    [SerializeField]
    private GameObject[] m_MainMenuObjects;
    
    [SerializeField]
    private GameObject m_MainScreen;

    [SerializeField]
    private GameObject m_CreditsScreen;

    [SerializeField]
    private Image m_blackScreen;

    [SerializeField]
    private AudioSource m_audio;

    [SerializeField]
    private float m_lerpSpeed;

    bool m_isLerping;
    float m_timeSinceStart;

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
        m_blackScreen.raycastTarget = true;
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

    public void ToggleCreditsScreen()
    {
        m_CreditsScreen.SetActive(!m_CreditsScreen.activeInHierarchy);
        m_MainScreen.SetActive(!m_CreditsScreen.activeInHierarchy);
        foreach(GameObject game in m_MainMenuObjects)
        {
            game.SetActive(m_MainScreen.activeInHierarchy);
        }
    }
}
