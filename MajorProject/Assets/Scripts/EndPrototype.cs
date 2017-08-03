using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPrototype : MonoBehaviour {

    public GameObject endPrototypeScreen;
    public PlayerMovement player;

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
            player.SetMovement(false);
            endPrototypeScreen.SetActive(true);
        }
    }
}
