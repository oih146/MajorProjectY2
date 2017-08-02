using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour {

    public TurnBasedScript battleStarter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider hit)
    {
        if(hit.tag == "Player" && battleStarter.BattleActive == false)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            if (hit.transform.parent != null)
            {
                hit.GetComponentInParent<Rigidbody>().isKinematic = true;
                hit.GetComponentInParent<PlayerMovement>().enabled = false;
            }
            else
            {
                hit.GetComponent<Rigidbody>().isKinematic = true;
                hit.GetComponent<PlayerMovement>().enabled = false;
            }
            battleStarter.BattleActive = true;
            Debug.Log("BATTLE!");
            CharacterStatSheet[] buffer = new CharacterStatSheet[gameObject.transform.childCount];
            for(int i = 0; i < gameObject.transform.childCount; i++)
            {
                buffer[i] = gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<CharacterStatSheet>();
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
            battleStarter.StartBattle(buffer2, buffer);
        }
    }
}
