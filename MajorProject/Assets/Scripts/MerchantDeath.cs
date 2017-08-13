using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class MerchantDeath : CharacterStatSheet {

	// Use this for initialization
	void Start () {
        m_renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(DialogueLua.GetVariable("Merch_dead").AsBool.ToString());
        if (DialogueLua.GetVariable("Merch_dead").AsBool == true)
        {
            FadeDeath = true;
        }
        if(FadeDeath == true)
        {
            m_animator.SetBool("dead", true);
            FadingDeath();
        }
	}
}
