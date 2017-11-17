using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatTextFlash : MonoBehaviour {

    public static CheatTextFlash Instance;

    [SerializeField]
    float m_fadeTime;

    UnityEngine.UI.Text m_text;
    float m_timeSinceStart;
    bool m_lerping;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        m_text = GetComponent<UnityEngine.UI.Text>();
        m_text.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (m_lerping)
            LerpBody();
    }

    void LerpBody()
    {
        float timeInLerp = Time.time - m_timeSinceStart;
        float percentage = timeInLerp / m_fadeTime;

        Color color = m_text.color;
        color.a = Mathf.Lerp(1, 0, percentage);
        m_text.color = color;
        if(percentage >= 1f)
        {
            m_lerping = false;
        }
    }

    public void StartLerp(string newText)
    {
        m_text.text = newText;
        Color color = m_text.color;
        color.a = 1;
        m_text.color = color;
        m_timeSinceStart = Time.time;
        m_lerping = true;
    }
}
