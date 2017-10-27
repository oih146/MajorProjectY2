using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicsHolder : MonoBehaviour {

    //SendMessage(FlipPlayer, , Cinematics);

    public PlayerStat m_player;
    public FollowTarget m_empress;

    public BanditStat[] m_bandits;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FlipPlayer()
    {
        m_player.transform.localEulerAngles = new Vector3(m_player.transform.localEulerAngles.x, (m_player.transform.localEulerAngles.y == 0 ? 180 : 0), m_player.transform.localEulerAngles.z);
        m_player.gameObject.transform.localScale = new Vector3(m_player.transform.localScale.x, m_player.transform.localScale.y, -m_player.transform.localScale.z);
    }
}
