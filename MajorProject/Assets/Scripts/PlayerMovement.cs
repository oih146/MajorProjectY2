using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float maxSpeed = 10f;
    Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float move = Input.GetAxis("Horizontal");

        rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);

	}
}
