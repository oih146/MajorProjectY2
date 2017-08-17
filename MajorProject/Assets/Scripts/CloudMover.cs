using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMover : MonoBehaviour {

    //target should always be camera
    public GameObject m_target;
    //cloud 0 is always cloud currently on screen
    public GameObject[] clouds;
    public float m_resetPoint;
    public float m_tillPoint;
    public float m_cloudCounter;
    public float m_cloudMoveSpeed;
    public bool m_firstCloud;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 newPos = clouds[0].transform.position;
        //newPos.x = Mathf.Repeat(Time.deltaTime * m_cloudMoveSpeed, m_tillPoint);
        //clouds[0].transform.position = newPos;
        clouds[0].transform.Translate(Vector3.right * m_cloudMoveSpeed);
        clouds[1].transform.Translate(Vector3.right * m_cloudMoveSpeed);
        LeapFrog();
	}

    public void LeapFrog()
    {
        if (m_target.transform.position.x > ((m_firstCloud) ? clouds[0].transform.position.x : clouds[1].transform.position.x))
        {
            float rain1 = clouds[0].transform.position.x;
            float rain2 = clouds[1].transform.position.x;
            float distance = (m_firstCloud) ? rain1 - rain2 : rain2 - rain1;
            distance = Mathf.Abs(distance);
            if (m_firstCloud)
            {
                Vector3 temp = clouds[0].transform.position;
                temp.x = clouds[0].transform.position.x + distance;
                clouds[1].transform.position = temp;
            }
            else
            {
                Vector3 temp = clouds[1].transform.position;
                temp.x = clouds[1].transform.position.x + distance;
                clouds[0].transform.position = temp;
            }
            m_firstCloud = !m_firstCloud;
        } else if(m_target.transform.position.x < ((!m_firstCloud) ? clouds[0].transform.position.x : clouds[1].transform.position.x))
        {
            Vector3 backCloudTemp = ((!m_firstCloud) ? clouds[0].transform.position : clouds[1].transform.position);
            Vector3 frontCloudTemp = ((m_firstCloud) ? clouds[0].transform.position : clouds[1].transform.position);
            float rain1 = backCloudTemp.x;
            float rain2 = frontCloudTemp.x;
            Vector3 temp = backCloudTemp;
            temp.x -= 1;
            frontCloudTemp = temp;

            float distance = (m_firstCloud) ? rain1 - rain2 : rain2 - rain1;
            distance = Mathf.Abs(distance);
            temp.x = backCloudTemp.x + distance;
            backCloudTemp = temp;
            if(!m_firstCloud)
            {
                clouds[0].transform.position = backCloudTemp;
                clouds[1].transform.position = frontCloudTemp;
            }
            else
            {
                clouds[0].transform.position = frontCloudTemp;
                clouds[1].transform.position = backCloudTemp;
            }
        }
    }
}
