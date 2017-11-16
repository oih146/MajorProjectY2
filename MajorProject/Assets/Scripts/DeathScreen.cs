using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour {

    public static DeathScreen Instance;

    [SerializeField]
    private UnityEngine.UI.Text m_deathText;

    [SerializeField]
    private UnityEngine.UI.Image m_deathImage;

    [SerializeField]
    private float m_lerpSpeed;

    [SerializeField]
    private float m_hangtime;
    bool m_goingToShow = true;
    float m_timeSinceStart;
    bool m_lerping;
    

    void Awake()
    {
        Instance = this;
    }

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

        Color color = m_deathImage.color;
        color.a = Mathf.Lerp((m_goingToShow ? 1 : 0), (m_goingToShow ? 0 : 1), percentage);
        m_deathImage.color = color;
        if(percentage >= 1f)
        {

            m_lerping = false;
            if (m_goingToShow)
            {
                m_goingToShow = false;
                StartCoroutine(HangTime());
            }
            else
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    IEnumerator HangTime()
    {
        yield return new WaitForSeconds(m_hangtime);
        StartLerp();
    }

    void StartLerp()
    {
        m_timeSinceStart = Time.time;
        m_lerping = true;
    }

    public void ShowDeathScreen()
    {
        FadeBlack.OnFadeBlackMidle -= ShowDeathScreen;
        FadeBlack.Instance.enabled = false;
        m_deathImage.gameObject.SetActive(true);
        m_deathText.enabled = true;
        StartLerp();

    }
}
