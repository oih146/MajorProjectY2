using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookUp : MonoBehaviour {

    [SerializeField]
    private PixelCrushers.DialogueSystem.UIButtonKeyTrigger m_continueTriggerKey;
    [SerializeField]
    private Transform[] m_moveUpWith;
    [SerializeField]
    private GameObject m_rain;
    [SerializeField]
    private GameObject m_camera;
    [SerializeField]
    private float m_lerpSpeed;
    [SerializeField]
    private float m_toYPos;

    float m_initalYPos;
    float m_timeSinceStart;
    bool m_lerping;
    KeyCode m_savedButtonKeyCode;
    string m_savedButtonstring;

    void Start()
    {
    }

    void Update()
    {
        if(m_lerping)
        {
            LerpBody();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //PlayerMovement.Instance.SetMovementFalse();
            BattleMenuScript.Instance.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            StartLerp();
        }
    }

    public void LerpBody()
    {
        float timeInLerp = Time.time - m_timeSinceStart;
        float percentage = timeInLerp / m_lerpSpeed;

        Vector3 temp = m_camera.transform.position;

        temp.y = Mathf.Lerp(m_initalYPos, m_toYPos, percentage);

        m_camera.transform.position = temp;
        if(percentage >= 1f)
        {
            m_lerping = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }


    public void StartLerp()
    {
        foreach(Transform game in m_moveUpWith)
        {
            game.parent = null;
            game.gameObject.SetActive(true);
        }
        PlayerMovement.Instance.RemoveEvents();
        m_initalYPos = m_camera.transform.eulerAngles.y;
        BattleMenuScript.Instance.gameObject.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        m_timeSinceStart = Time.time;
        m_lerping = true;
    }

    public void TurnOffCancelKey()
    {
        m_savedButtonstring = m_continueTriggerKey.buttonName;
        m_savedButtonKeyCode = m_continueTriggerKey.key;
        m_continueTriggerKey.key = KeyCode.None;
    }

    public void TurnOnCancelKey()
    {
        m_continueTriggerKey.buttonName = m_savedButtonstring;
        m_continueTriggerKey.key = m_savedButtonKeyCode;
    }

    public void TurnOffRain()
    {
        foreach(ParticleSystem part in m_rain.GetComponentsInChildren<ParticleSystem>())
        {
            part.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

}
