using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class MidConversationEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool CheckResponseNum()
    {
        return DialogueLua.GetVariable("ConTrigger").AsBool;
    }
}
