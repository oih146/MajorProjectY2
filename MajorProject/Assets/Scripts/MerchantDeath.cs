using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class MerchantDeath : CharacterStatSheet {

    bool dying = false;
	// Use this for initialization
	void Start () {
        GetLerpDeath();
        m_renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(DialogueLua.GetVariable("Merch_dead").AsBool.ToString());
        if (DialogueLua.GetVariable("Merch_dead").AsBool == true && dying == false)
        {
            dying = true;
            fadeDeathLerping.StartLerp(2, 1, 0);
            FadeDeath = true;
        }
        if(FadeDeath == true)
        {
            m_animator.SetBool("dead", true);
            FadingDeath();
        }
	}
}
