using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualBackgrounds : MonoBehaviour {

    public GameObject[] m_otherObjects;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CopyOver()
    {
        foreach(GameObject obj in m_otherObjects)
        {
            obj.gameObject.transform.position = new Vector3(obj.gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }
}
