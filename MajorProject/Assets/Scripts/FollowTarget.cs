using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Animator m_animator;
    public float moveSpeed;
    public bool m_moving;
    public Transform target;
    public float offset;

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if ((target.position.x < transform.position.x - offset || target.position.x > transform.position.x + offset))
        {
            if(m_moving == false)
            {
                m_animator.Play("Walk");
            }
            m_moving = true;


            Vector3 buff = transform.position;
            Vector3 buff2 = transform.position;
            buff2.x = Mathf.Lerp(transform.position.x, target.position.x, moveSpeed * Time.deltaTime);
            float speed = Mathf.Abs(buff.x - buff2.x) / Time.deltaTime;
            transform.position = buff2;
        }
        else if (target.position.x > transform.position.x - offset && target.position.x < transform.position.x + offset)
        {
            if (m_moving == true)
            {
                m_animator.Play("Idle");
            }
            m_moving = false;

        }

	}
}
