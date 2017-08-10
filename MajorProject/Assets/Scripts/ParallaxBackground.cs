using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    public Transform m_parallaxTarget;
    public float m_moveSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(PlayerMovement.m_amMoving)
        {
            Vector3 buff = gameObject.transform.position;
            buff.x = Mathf.Lerp(buff.x, m_parallaxTarget.position.x, m_moveSpeed * Time.deltaTime);
            gameObject.transform.position = buff;
        }
	}
}
