using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditMoveScript : MidConversationEvent {

    public Transform m_MoveOnXAxis;
    float m_xToMoveTo;
    public float m_moveSpeed;
    public float m_offset;
    public bool m_Move = false;
    public bool m_add = false;
    public bool m_stop = false;

	// Use this for initialization
	void Start () {
        m_xToMoveTo = m_MoveOnXAxis.position.x;
	}

    void OnEnable()
    {
        ConversationEvents.AfterPlayerResponse += MakeBanditsMove;
        ConversationEvents.OnConversationEnd += MakeBanditsMove;
    }

    void OnDisable()
    {
        ConversationEvents.AfterPlayerResponse -= MakeBanditsMove;
        ConversationEvents.OnConversationEnd -= MakeBanditsMove;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_Move && (gameObject.transform.position.x < m_xToMoveTo - 1.0f || gameObject.transform.position.x > m_xToMoveTo + 1.0f))
        {
            //if(!m_add)
            //{
            //    ConversationEvents.OnConversationStart += StopBanditsMove;
            //    m_add = true;
            //}
            Vector3 buff = gameObject.transform.position;
            buff.x = Mathf.Lerp(buff.x, m_xToMoveTo, m_moveSpeed * Time.deltaTime);
            gameObject.transform.position = buff;
        }
        else if(gameObject.transform.position.x > m_xToMoveTo - 0.5f && gameObject.transform.position.x < m_xToMoveTo + 0.5f)
            enabled = false;
        //Mid Point Stop
        if (m_Move && m_stop == false && (gameObject.transform.position.x > m_xToMoveTo - 2 && gameObject.transform.position.x < m_xToMoveTo + 2))
        {
            StopBanditsMove();
            m_stop = true;
        }
    }

    public void MakeBanditsMove()
    {
        if (!CheckResponseNum())
            return;
        m_Move = true;
        ConversationEvents.AfterPlayerResponse -= MakeBanditsMove;
    }

    public void StopBanditsMove()
    {
        m_Move = false;
        //ConversationEvents.OnConversationStart -= StopBanditsMove;
    }

    //void OnTriggerEnter(Collider hit)
    //{
    //    if (hit.tag == "Player")
    //    {
    //        StopBanditsMove();
    //    }
    //}
}
