using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour {

    public EnemyBase[] m_enemies;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider hit)
    {
        if(hit.tag == "Player" && TurnBasedScript.Instance.BattleActive == false)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            if (hit.transform.parent != null)
            {
                hit.GetComponentInParent<PlayerMovement>().SetMovement(false);
            }
            else
            {
                hit.GetComponent<PlayerMovement>().SetMovement(false);
            }
            TurnBasedScript.Instance.BattleActive = true;
            Debug.Log("BATTLE!");
            CharacterStatSheet[] buffer2;
            if (hit.transform.parent == null)
            {
                buffer2 = new CharacterStatSheet[hit.GetComponentInChildren<PlayerStat>().m_allies.Length + 1];
                buffer2[0] = hit.GetComponentInChildren<CharacterStatSheet>();
            }
            else
            {
                buffer2 = new CharacterStatSheet[hit.GetComponent<PlayerStat>().m_allies.Length + 1];
                buffer2[0] = hit.GetComponent<CharacterStatSheet>();
            }
            int t = 1;
            foreach(CharacterStatSheet charSS in hit.GetComponentInChildren<PlayerStat>().m_allies)
            {
                buffer2[t] = charSS;
                t++;
            }
            TurnBasedScript.Instance.enabled = true;
            MusicSwitcher.Instance.StartLerping();
            TurnBasedScript.Instance.StartBattle(buffer2, m_enemies);
            enabled = false;
        }
    }

    public void AddToConversationEnd()
    {
        ConversationEvents.OnConversationEnd += EnableThis;
    }

    public void EnableThis()
    {
        StartCoroutine(StartBattle());
    }

    IEnumerator StartBattle()
    {
        yield return new WaitUntil(() => !FindObjectOfType<PixelCrushers.DialogueSystem.DialogueSystemController>().IsConversationActive);
        GetComponent<Collider>().enabled = true;
        ConversationEvents.OnConversationEnd -= EnableThis;
    }
}
