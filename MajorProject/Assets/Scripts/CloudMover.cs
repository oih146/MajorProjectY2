using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMover : MonoBehaviour {

    //cloud 0 is always cloud currently on screen
    public GameObject[] clouds;
    public float m_resetPoint;
    public float m_tillPoint;
    public float m_cloudCounter;
    public float m_cloudMoveSpeed;

	// Use this for initialization
	void Start () {
        m_resetPoint = clouds[0].transform.localPosition.x;
        m_tillPoint = clouds[0].transform.localPosition.x + Mathf.Abs(clouds[clouds.Length - 1].transform.position.x);

	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 newPos = clouds[0].transform.position;
        //newPos.x = Mathf.Repeat(Time.deltaTime * m_cloudMoveSpeed, m_tillPoint);
        //clouds[0].transform.position = newPos;
        clouds[0].transform.Translate(Vector3.right * m_cloudMoveSpeed);
        if(clouds[0].transform.localPosition.x > m_tillPoint)
        {
            Vector3 buff = clouds[0].transform.position;
            buff.x = m_resetPoint;
            clouds[0].transform.position = buff;
        }
	}
}
