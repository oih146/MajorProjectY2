using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookUp : MonoBehaviour {

    public GameObject m_camera;
    public float m_lerpSpeed;
    public float m_toYPos;
    float m_initalYPos;

    float m_timeSinceStart;
    bool m_lerping;

    void Start()
    {
        m_initalYPos = m_camera.transform.eulerAngles.y;
    }

    void Update()
    {
        if(m_lerping)
        {
            LerpBody();
        }
    }

    public void LerpBody()
    {
        float timeInLerp = Time.time - m_timeSinceStart;
        float percentage = timeInLerp / m_lerpSpeed;

        Vector3 temp = m_camera.transform.position;

        temp.y = Mathf.Lerp(m_initalYPos, m_toYPos, percentage);

        m_camera.transform.position = temp;
    }


    public void StartLerp()
    {
        m_timeSinceStart = Time.time;
        m_lerping = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            BattleMenuScript.Instance.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            StartLerp();
        }
    }

}
