using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Animator m_animator;
    public float moveSpeed;
    public bool m_moving;
    bool Moving {
        get { return m_moving; }
        set
        {
            if(value != m_moving)
            {
                if (value == false)
                    m_animator.Play("Idle");
                else
                    m_animator.Play("Walk");
                m_moving = value;
            }
        }
    }
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
            Moving = true;


            Vector3 buff2 = transform.position;
            buff2.x = Mathf.Lerp(transform.position.x, target.position.x, moveSpeed * Time.deltaTime);
            transform.position = buff2;
        }
        else if (target.position.x > transform.position.x - offset && target.position.x < transform.position.x + offset)
        {
            Moving = false;

        }

	}

    public void SetSpeed(string newSpeed)
    {
        float intspeed;
        if(float.TryParse(newSpeed, out intspeed))
        {
            moveSpeed = intspeed;
        }
    }
}
