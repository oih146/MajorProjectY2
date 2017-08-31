﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public int m_InterruptBase;
    public static bool m_amMoving;
    public float maxSpeed = 10f;
    public static float m_speed;
    public bool m_autoMove;
    Rigidbody rigid;

    void Awake()
    {
        CharacterStatSheet.m_InteruptMultiplier = m_InterruptBase;
    }

	// Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.isKinematic = true;
        enabled = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float move;
        if (m_autoMove)
            move = 0.5f;
        else
            move = Input.GetAxis("Horizontal");
        if (move != 0 && rigid.isKinematic == false)
        {
            m_amMoving = true;
        }
        else
            m_amMoving = false;
        rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);
        m_speed = move;

	}

    public void SetMovement(bool status)
    {
        rigid.isKinematic = !status;
        m_amMoving = status;
        enabled = status;
    }
}
