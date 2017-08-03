using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public float moveSpeed;
    public Transform target;
    public float offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(target.position.x < transform.position.x - offset || target.position.x > transform.position.x + offset)
        {
            Vector3 buff = transform.position;
            buff.x = Mathf.Lerp(transform.position.x, target.position.x, moveSpeed * Time.deltaTime);
            transform.position = buff;
        }
	}
}
