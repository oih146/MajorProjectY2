using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditMoveScript : MidConversationEvent {

    public EnemyBase[] m_bandits;
    public Transform m_MoveOnXAxis;
    float m_xToMoveTo;
    public float m_moveSpeed;
    public float m_secondarySpeed;
    public float m_offset;
    public bool m_Move = false;
    public bool m_add = false;
    public bool m_stop = false;

    bool m_slowerStart;
    float m_timeSinceStart;
    float m_initalXPos;
	// Use this for initialization
	void Start () {
        m_xToMoveTo = m_MoveOnXAxis.position.x;
	}

    void AttachToEvents()
    {
        ConversationEvents.AfterConversationLineEnd += MakeBanditsMove;
        ConversationEvents.AfterPlayerResponse += MakeBanditsMove;
        ConversationEvents.OnConversationEnd += StartSlowerSpeed;
    }

    void OnDisable()
    {
        ConversationEvents.AfterConversationLineEnd -= MakeBanditsMove;
        ConversationEvents.AfterPlayerResponse -= MakeBanditsMove;
        ConversationEvents.OnConversationEnd -= StartSlowerSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_Move)
        {

            float timeInLerp = Time.time - m_timeSinceStart;
            float percentage = timeInLerp / m_moveSpeed;

            Vector3 buff = gameObject.transform.position;
            buff.x = Mathf.Lerp(m_initalXPos, m_xToMoveTo, percentage);
            gameObject.transform.position = buff;
            if(percentage >= 1f)
            {
                StopBanditsMove();
            }
        }

        if(m_slowerStart)
        {
            gameObject.transform.Translate(Vector3.left * m_secondarySpeed);
        }
    }

    public void MakeBanditsMove()
    {
        if (!CheckResponseNum())
            return;
        SetBanditAnimator("Walk");
        m_xToMoveTo = m_MoveOnXAxis.position.x;
        m_initalXPos = gameObject.transform.position.x;
        m_timeSinceStart = Time.time;
        m_Move = true;
        ConversationEvents.AfterConversationLineEnd -= MakeBanditsMove;
        ConversationEvents.AfterPlayerResponse -= MakeBanditsMove;
    }

    public void StartSlowerSpeed()
    {
        SetBanditAnimator("Walk");
        m_slowerStart = true;
        ConversationEvents.OnConversationEnd -= StartSlowerSpeed;
    }

    public void StopBanditsMove()
    {
        SetBanditAnimator("Idle");
        m_Move = false;
        //ConversationEvents.OnConversationStart -= StopBanditsMove;
    }

    public void SetBanditAnimator(string animationName)
    {
        foreach(EnemyBase eb in m_bandits)
        {
            eb.m_animator.Play(animationName);
        }
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            StopBanditsMove();
            m_slowerStart = false;
        }
    }
}
