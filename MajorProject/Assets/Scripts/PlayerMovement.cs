using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float maxSpeed = 10f;
    Rigidbody rigid;

	// Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.isKinematic = true;
        enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float move = Input.GetAxis("Horizontal");

        rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);

	}

    public void SetMovement(bool status)
    {
        rigid.isKinematic = !status;
        enabled = status;
    }
}
