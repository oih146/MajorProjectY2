using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class MerchantDeath : MonoBehaviour {

    bool dying = false;
    bool heal = false;
    public bool FadeDeath;
    private Lerping fadeDeathLerping;
    private SpriteRenderer m_renderer;
    public AnimationEffectScript m_heal;
    private Animator m_animator;
	// Use this for initialization
	void Start () {
        m_animator = GetComponent<Animator>();
        fadeDeathLerping = GetComponent<Lerping>();
        m_renderer = GetComponent<SpriteRenderer>();
	}

    //Simply turns current sprite alpha down
    public void FadingDeath()
    {
        Color buffCol = m_renderer.color;
        buffCol.a = fadeDeathLerping.Lerp();
        m_renderer.color = buffCol;
        if (m_renderer.color.a < 0.001f)
        {
            FadeDeath = false;
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
        if(FadeDeath)
        {
            FadingDeath();
        }
	}

    public void DecideMerchantFate()
    {
        //Debug.Log(DialogueLua.GetVariable("Merch_dead").AsBool.ToString());
        if (DialogueLua.GetVariable("Merch_dead").AsBool == true && dying == false)
        {
            dying = true;
            fadeDeathLerping.StartLerp(2, 1, 0);
            FadeDeath = true;
            m_animator.SetBool("dead", true);
            FadingDeath();
            ConversationEvents.AfterPlayerResponse -= DecideMerchantFate;
        } else if (DialogueLua.GetVariable("Merch_heal").AsBool == true && heal == false)
        {
            m_heal.transform.SetParent(gameObject.transform.GetChild(0));
            m_heal.transform.localPosition = Vector3.zero;
            m_heal.PlayEffect();
            heal = true;
            ConversationEvents.AfterPlayerResponse -= DecideMerchantFate;
        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            ConversationEvents.AfterPlayerResponse += DecideMerchantFate;
        }
    }
}
