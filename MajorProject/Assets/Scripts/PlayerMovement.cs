using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement Instance;

    public enum EWalk
    {
        Normal,
        Chicken,
        Crab
    }


    public int m_InterruptBase;
    public bool m_amMoving;
    public float maxSpeed = 10f;
    public float m_speed;
    public bool m_walking = false;
    public bool m_autoMove;
    Rigidbody rigid;
    private EWalk m_walkType = EWalk.Normal;

    void OnDestroy()
    {
        RemoveEvents();
    }

    void OnEnable()
    {
        Instance = this;
        CharacterStatSheet.m_InteruptMultiplier = m_InterruptBase;
    }

    // Use this for initialization
    void Start() {
        ConversationEvents.OnConversationStart += SetMovementFalse;
        ConversationEvents.OnConversationEnd += SetMovementTrue;
        rigid = gameObject.GetComponent<Rigidbody>();
        rigid.useGravity = false;
        SetMovement(false);
    }

    void Update()
    {
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
                switch (m_walkType)
                {
                    case EWalk.Normal:
                        GetComponentInChildren<PlayerStat>().m_animator.Play("Walk");
                        Debug.Log("Using Normal Walk");
                        break;
                    case EWalk.Chicken:
                        GetComponentInChildren<PlayerStat>().m_animator.Play("Walk2");
                        Debug.Log("Using Chicken Walk");
                        break;
                    case EWalk.Crab:
                        GetComponentInChildren<PlayerStat>().m_animator.Play("Walk3");
                        Debug.Log("Using Crab Walk");
                        break;
                    default:
                        GetComponentInChildren<PlayerStat>().m_animator.Play("Walk");
                        break;
                }
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

    public void RemoveEvents()
    {
        ConversationEvents.OnConversationStart -= SetMovementFalse;
        ConversationEvents.OnConversationEnd -= SetMovementTrue;
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

    public void SwitchWalk(EWalk walk)
    {
        m_walkType = walk;
        if(m_walking)
        {
            switch (m_walkType)
            {
                case EWalk.Normal:
                    GetComponentInChildren<PlayerStat>().m_animator.Play("Walk");
                    Debug.Log("Using Normal Walk");
                    break;
                case EWalk.Chicken:
                    GetComponentInChildren<PlayerStat>().m_animator.Play("Walk2");
                    Debug.Log("Using Chicken Walk");
                    break;
                case EWalk.Crab:
                    GetComponentInChildren<PlayerStat>().m_animator.Play("Walk3");
                    Debug.Log("Using Crab Walk");
                    break;
                default:
                    GetComponentInChildren<PlayerStat>().m_animator.Play("Walk");
                    break;
            }
        }
    }

}
