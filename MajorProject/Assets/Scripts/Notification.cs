using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {

    private Text text;
    public float m_upPoint;
    public float m_upSpeed;
    private float m_nextPos;
    private float m_prevPos;
    public bool m_lerping = false;
    float m_timeSinceStart;

    void OnEnable()
    {
        gameObject.transform.localScale = Vector3.one;
    }

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        //gameObject.transform.localPosition = new Vector3(-220, 40, 1);


	}
	
	// Update is called once per frame
	void Update () {
        if(m_lerping)
        {
            float timeSinceLerpStart = Time.time - m_timeSinceStart;
            float percentage = timeSinceLerpStart / m_upSpeed;

            Vector3 temp = transform.position;
            temp.y = Mathf.Lerp(m_prevPos, m_nextPos, percentage);
            transform.position = temp;
            if(percentage >= 1f)
            {
                m_lerping = false;
                NotificationManager.Instance.DestroyCurrentNotification();
            }
        }
	}

    public void StartLerping()
    {
        m_prevPos = transform.position.y;
        m_nextPos = transform.position.y + m_upPoint;
        m_timeSinceStart = Time.time;
        m_lerping = true;
    }

    public void SetText(string newText)
    {
        gameObject.GetComponent<Text>().text = newText;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }
}
