using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedScript : MonoBehaviour {

    bool m_playerTurn
    {
        get
        {
            return m_playerTurn;
        }
        set 
        {
            m_playerCount = 0;
            m_playerTurn = value;
        }
    }

    private int m_playerCount;
    public static CharacterStatSheet[] friendlyObjects = new CharacterStatSheet[4];
    public static CharacterStatSheet[] enemyObjects = new CharacterStatSheet[4];
    public CharacterStatSheet m_attackingCharacter;
    RaycastHit hitInfo;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (m_playerTurn == true && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity) && hitInfo.collider.tag == "Enemy")
            {
                m_attackingCharacter = hitInfo.collider.gameObject.GetComponent<CharacterStatSheet>();
            }
        }
    }

    public void SetPlayerTeamTurn()
    {
        m_playerTurn = true;
        SetPlayerButtons(true);
        m_attackingCharacter = null;
    }

    public void SetEnemyTeamTurn()
    {
        m_playerTurn = false;
        SetPlayerButtons(false);
        m_attackingCharacter = friendlyObjects[0];
    }

    void SetPlayerButtons(bool status)
    {
        SetAttackButton(status);
        SetFleeButton(status);
    }

    void SetAttackButton(bool newActive)
    {
        BattleMenuScript.AttackButton.gameObject.SetActive(newActive);
    }

    void SetFleeButton(bool newActive)
    {
        BattleMenuScript.FleeButton.gameObject.SetActive(newActive);
    }

    public void EndTurnPressed()
    {
        NextPlayerTurn();
    }

    void NextPlayerTurn()
    {
        m_playerCount++;
        if(m_playerTurn == true && friendlyObjects[m_playerCount] == null)
        {
            while (friendlyObjects[m_playerCount] == null)
            {
                m_playerCount++;
                if(m_playerCount > friendlyObjects.Length)
                {
                    SetEnemyTeamTurn();
                    return;
                }
            }
        }
        else if (m_playerTurn == false && enemyObjects[m_playerCount] == null)
        {
            while (enemyObjects[m_playerCount] == null)
            {
                m_playerCount++;
                if (m_playerCount > enemyObjects.Length)
                {
                    SetPlayerTeamTurn();
                    return;
                }
            }
        }
    }

    public void AttackButtonPressed()
    {
        if (m_attackingCharacter != null)
        {
            //Play Animation 
            MeleeAttack();
            Debug.Log("Attacked " + m_attackingCharacter.gameObject.name.ToString());
        }
        SetAttackButton(false);
        NextPlayerTurn();
    }

    public void MeleeAttack()
    {
        if(m_playerTurn == true)
            m_attackingCharacter.m_health -= friendlyObjects[m_playerCount].m_weapon.GetAttack();
        else
        {
            m_attackingCharacter.m_health -= enemyObjects[m_playerCount].m_weapon.GetAttack();
        }
    }

    public static void SetEnemy(CharacterStatSheet[] enemyPlayers)
    {
        int i = 0;
        foreach(CharacterStatSheet charSS in enemyPlayers)
        {
            enemyObjects[i] = charSS;
            i++;
        }
    }

    public static void SetPlayers(CharacterStatSheet[] friendlyPlayers)
    {
        int i = 0;
        foreach (CharacterStatSheet charSS in friendlyPlayers)
        {
            friendlyObjects[i] = charSS;
            i++;
        }
    }

    void EnemyAttacks()
    {
        for(;m_playerCount < enemyObjects.Length;)
        {
            MeleeAttack();
            NextPlayerTurn();
        }
    }

    void EnemyTurn()
    {
        EnemyDecideTarget();

    }

    void EnemyDecideTarget()
    {
        int playerToAttack = Random.Range(0, friendlyObjects.Length);
        m_attackingCharacter = friendlyObjects[playerToAttack];
    }

    void EnemyDecideAttack()
    {
        if(enemyObjects[m_playerCount].m_knowMagic == true)
        {

        }
        else
        {

        }
    }
}
