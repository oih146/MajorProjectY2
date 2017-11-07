using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement Instance;

    public int m_InterruptBase;
    public static bool m_amMoving;
    public float maxSpeed = 10f;
    public static float m_speed;
    public bool m_walking = false;
    public bool m_autoMove;
    Rigidbody rigid;

    void OnEnable()
    {
        Instance = this;
        CharacterStatSheet.m_InteruptMultiplier = m_InterruptBase;
    }

    public void RemoveEvents()
    {
        ConversationEvents.OnConversationStart -= SetMovementFalse;
        ConversationEvents.OnConversationEnd -= SetMovementTrue;
    }

    // Use this for initialization
    void Start() {
        ConversationEvents.OnConversationStart += SetMovementFalse;
        ConversationEvents.OnConversationEnd += SetMovementTrue;
        rigid = gameObject.GetComponent<Rigidbody>();
        rigid.useGravity = false;
        SetMovement(false);
    }

    // Update is called once per frame
    void FixedUpdate() {
        float move;
        if (m_autoMove)
            move = 0.5f;
        else
            move = Input.GetAxis("Horizontal");
        if (move != 0 && rigid.isKinematic == false)
        {
            if (!m_walking)
            {
                GetComponentInChildren<PlayerStat>().m_animator.Play("Walk");
                m_walking = true;
            }
            m_amMoving = true;
        }
        else
        {
            m_amMoving = false;
            m_walking = false;
        }
        rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);
        m_speed = move;

    }

    public void SetMovement(bool status)
    {
        if (status)
            SetMovementTrue();
        else
            SetMovementFalse();
    }

    public void SetMovementTrue()
    {
        rigid.isKinematic = false;
        m_amMoving = true;
        enabled = true;
    }

    public void SetMovementFalse()
    {
        GetComponentInChildren<PlayerStat>().SetToOutOfBattle();
        rigid.isKinematic = true;
        m_amMoving = false;
        enabled = false;
        m_walking = false;
    }

    public void SetSpeed(string newSpeed)
    {
        int intspeed;
        if(int.TryParse(newSpeed, out intspeed))
        {
            maxSpeed = intspeed;
        }
    }

    public void DisableMovement()
    {
        enabled = false;
    }

}
