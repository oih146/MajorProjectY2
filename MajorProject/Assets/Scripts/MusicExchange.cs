using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicExchange : MonoBehaviour {

    public enum MusicSource
    {
        Regular,
        Combat
    }

    public AudioSource m_audioSource;
    public MusicSource m_musicSource;
    public AudioClip m_newClip;

    [SerializeField]
    private float m_lerpSpeed;

    float m_keptTime;
    float m_otherAudioVolume;
    float m_timeSinceStart;
    bool m_lerping;

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


        float newVolume = Mathf.Lerp(0, 1, percentage);
        m_audioSource.volume = newVolume;
        switch (m_musicSource)
        {
            case MusicSource.Regular:
                MusicSwitcher.Instance.m_normalAudio.volume = 1 - newVolume;
                break;
            case MusicSource.Combat:
                MusicSwitcher.Instance.m_battleAudio.volume = 1 - newVolume;
                break;
            default:
                break;
        }

        if(percentage >= 1f)
        {
            float m_time = m_audioSource.time;
            switch (m_musicSource)
            {
                case MusicSource.Regular:
                    MusicSwitcher.Instance.m_normalAudio.Stop();
                    MusicSwitcher.Instance.m_normalAudio.clip = m_audioSource.clip;
                    MusicSwitcher.Instance.m_normalAudio.time = m_time;
                    MusicSwitcher.Instance.m_normalAudio.volume = 1;
                    MusicSwitcher.Instance.m_normalAudio.Play();
                    break;
                case MusicSource.Combat:
                    MusicSwitcher.Instance.m_battleAudio.Stop();
                    MusicSwitcher.Instance.m_battleAudio.clip = m_audioSource.clip;
                    MusicSwitcher.Instance.m_battleAudio.time = m_time;
                    MusicSwitcher.Instance.m_battleAudio.volume = 1;
                    MusicSwitcher.Instance.m_battleAudio.Play();
                    break;
                default:
                    break;
            }
            m_audioSource.Stop();
            m_lerping = false;
        }
    }

    void StartLerp()
    {
        switch (m_musicSource)
        {
            case MusicSource.Regular:

                m_keptTime = MusicSwitcher.Instance.m_normalAudio.time;
                break;
            case MusicSource.Combat:

                m_keptTime = MusicSwitcher.Instance.m_battleAudio.time;
                break;
            default:
                break;
        }
        m_audioSource.clip = m_newClip;
        m_audioSource.Play();
        m_timeSinceStart = Time.time;
        m_lerping = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if(enabled)
                ExchangeClip();
        }
    }

    void ExchangeClip()
    {
        m_audioSource.volume = 0;
        m_audioSource.Play();
        if (m_musicSource == MusicSource.Combat)
        {
            m_otherAudioVolume = MusicSwitcher.Instance.m_battleAudio.volume;
            if (MusicSwitcher.Instance.m_battleAudio.volume > 0)
                StartLerp();
            else
            {
                MusicSwitcher.Instance.m_battleAudio.Stop();
                MusicSwitcher.Instance.m_battleAudio.clip = m_newClip;
                MusicSwitcher.Instance.m_battleAudio.Play();
            }
        }
        else if (m_musicSource == MusicSource.Regular)
        {
            m_otherAudioVolume = MusicSwitcher.Instance.m_normalAudio.volume;
            if (MusicSwitcher.Instance.m_normalAudio.volume > 0)
                StartLerp();
            else
            {
                MusicSwitcher.Instance.m_normalAudio.Stop();
                MusicSwitcher.Instance.m_normalAudio.clip = m_newClip;
                MusicSwitcher.Instance.m_normalAudio.Play();
            }
        }
    }
}
