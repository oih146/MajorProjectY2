using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour {

    public AudioSource m_normalAudio;
    public AudioSource m_battleAudio;

    float m_timeSinceStart;
    public float m_LerpSpeed;
    public bool Lerping;
    public bool m_normalAudioOn;

    public bool lerping;

    public static MusicSwitcher Instance;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        //StartLerping();
	}
	
	// Update is called once per frame
	void Update () {
        if(Lerping)
        {
            float timeSinceLerp = Time.time - m_timeSinceStart;
            float percentage = timeSinceLerp / m_LerpSpeed;

            m_normalAudio.volume = Mathf.Lerp(m_normalAudioOn ? 1 : 0, m_normalAudioOn ? 0 : 1, percentage);
            m_battleAudio.volume = 1 - m_normalAudio.volume;
            if(percentage >= 1f)
            {
                Lerping = false;
                m_normalAudioOn = !m_normalAudioOn;
            }
        }
	}

    public void StartLerping()
    {
        m_timeSinceStart = Time.time;
        Lerping = true;
    }
}
