using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour {

    public TurnBasedScript battleStarter;
    public bool m_DoMove;
    public bool m_DoTalk;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    int FindNotBandits()
    {
        int total = 0;
        if (m_DoMove == true)
            total++;
        if (m_DoTalk == true)
            total++;
        return total;
    }

    void OnTriggerEnter(Collider hit)
    {
        if(hit.tag == "Player" && battleStarter.BattleActive == false)
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
            battleStarter.BattleActive = true;
            Debug.Log("BATTLE!");
            int takeFromChidCount = FindNotBandits();
            EnemyBase[] buffer = new EnemyBase[gameObject.transform.childCount - takeFromChidCount];
            for(int i = 0; i < gameObject.transform.childCount - takeFromChidCount; i++)
            {
                buffer[i] = gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<EnemyBase>();
            }
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
            battleStarter.enabled = true;
            MusicSwitcher.Instance.StartLerping();
            battleStarter.StartBattle(buffer2, buffer);
            enabled = false;
        }
    }
}
