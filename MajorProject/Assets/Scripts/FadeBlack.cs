using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeBlack : MonoBehaviour {

    public delegate void FadeBlackStartEvent();
    public static event FadeBlackStartEvent OnFadeBlackStart;
    public delegate void FadeBlackEndEvent();
    public static event FadeBlackEndEvent OnFadeBlackEnd;
    public delegate void FadeBlackMiddleEvent();
    public static event FadeBlackMiddleEvent OnFadeBlackMidle;

    public PlayerMovement player;
    public UnityEngine.UI.Image blackScreen;
    bool m_fadeIn = false;
    bool m_fading = false;
    public float m_fadeSpeed = 5;
    float m_timeSinceStart;
    float m_alphaInit;
    public static FadeBlack Instance;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        OnFadeBlackStart += TurnGameObjectOn;
        OnFadeBlackEnd += TurnGameObjectOff;

        OnFadeBlackMidle += RestartFadeOnMiddle;

        OnFadeBlackEnd += TurnPlayerMovementOn;
        OnFadeBlackStart += TurnPlayerMovementOff;
    }

    void OnDisable()
    {
        OnFadeBlackStart -= TurnGameObjectOn;
        OnFadeBlackEnd -= TurnGameObjectOff;

        OnFadeBlackMidle -= RestartFadeOnMiddle;

        OnFadeBlackEnd -= TurnPlayerMovementOn;
        OnFadeBlackStart -= TurnPlayerMovementOff;
    }

    // Use this for initialization
    void Start () {
        m_alphaInit = blackScreen.color.a;
        m_timeSinceStart = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_fading)
        {
            float timeLerping = Time.time - m_timeSinceStart;
            float percentage = timeLerping / m_fadeSpeed;
            Color buffCol = blackScreen.color;
            buffCol.a = Mathf.Lerp(m_alphaInit, (m_fadeIn) ? 0 : 1, percentage);
            blackScreen.color = buffCol;
            if(percentage >= 1f)
            {
                m_fading = false;
                //player.SetMovement(true);
                if (m_fadeIn == true)
                {
                    if(OnFadeBlackEnd != null)
                        OnFadeBlackEnd();
                    //gameObject.SetActive(false);
                }
                else
                {
                    if (OnFadeBlackMidle != null)
                        OnFadeBlackMidle();
                    //SceneManager.LoadScene(0);
                }
            }
        }
    }

    public void Activate(bool fadeIn)
    {
        m_fadeIn = fadeIn;
        m_alphaInit = blackScreen.color.a;
        m_timeSinceStart = Time.time;
        m_fading = true;
        gameObject.SetActive(true);
        if(OnFadeBlackStart != null)
            OnFadeBlackStart();
        //gameObject.SetActive(true);
        
    }

    void RestartFadeOnMiddle()
    {
        Activate(true);
    }

    private void TurnGameObjectOff()
    {
        gameObject.SetActive(false);
    }

    private void TurnGameObjectOn()
    {
        gameObject.SetActive(true);
    }

    private void TurnPlayerMovementOn()
    {
        player.SetMovement(true);
    }

    private void TurnPlayerMovementOff()
    {
        player.SetMovement(false);
    }

    public void AddLoadLevel()
    {
        OnFadeBlackMidle += ReturnToMenu;
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
