using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDivider : MonoBehaviour {

    public bool m_AlreadyPassed = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider hit)
    {
        if(hit.tag == "Player")
        {

        }
    }
}
