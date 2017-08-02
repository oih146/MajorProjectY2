using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DialogueTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SetGold();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            SetGold();
        }
	}

    public void SetGold()
    {
        DialogueLua.SetVariable("Gold", 60);
    }
}
