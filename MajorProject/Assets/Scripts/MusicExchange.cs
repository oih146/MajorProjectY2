using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicExchange : MonoBehaviour {

    public enum MusicSource
    {
        Regular,
        Combat
    }

    public MusicSource m_musicSource;


    public AudioClip m_newClip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //ExchangeClip();
        }
    }

    //void ExchangeClip()
    //{
    //    if(m_musicSource == MusicSource.Combat)
    //    {
    //        double time = MusicSwitcher.Instance.m_battleAudio.time;
    //        MusicSwitcher.Instance.m_battleAudio.Stop();
    //        MusicSwitcher.Instance.m_battleAudio.clip = m_newClip;
    //        MusicSwitcher.Instance.m_battleAudio.Play();
               
    //    }
    //    else if(m_musicSource == MusicSource.Regular)
    //    {
    //        MusicSwitcher.Instance.m_normalAudio.clip = m_newClip;
    //    }
    //}
}
