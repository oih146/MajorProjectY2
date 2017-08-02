using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnBasedScript : MonoBehaviour {

    public BattleMenuScript battleMenu;

    public bool m_playerTurn;
    public bool PlayerTurn
    {
        get
        {
            return m_playerTurn;
        }
        set 
        {
            m_playerCount = 0;
            if (value == false)
                SetEnemyTeamTurn();
            //else
            //    SetPlayerTeamTurn();
            m_playerTurn = value;
        }
    }
    public bool m_isFighting = false;
    public bool BattleActive
    {
        get
        {
            return m_isFighting;
        }
        set
        {
            if(value == true)
            {
                SetPlayerButtons(true);
            }
            else
            {
                SetPlayerButtons(false);
            }
            m_isFighting = value;
        }
    }

    public bool BattleOver = false;
    private int m_playerCount;
    public CharacterStatSheet[] friendlyObjects = new CharacterStatSheet[0];
    public CharacterStatSheet[] enemyObjects = new CharacterStatSheet[0];
    public CharacterStatSheet m_attackingCharacter;
    public CharacterStatSheet m_previousAttacker;
    RaycastHit hitInfo;
    public GameObject healthBarSlider;
    public bool animationPlaying;
    // Use this for initialization
    void Start () {
        //foreach (CharacterStatSheet charSS in friendlyObjects)
        //{
        //    GameObject healthbarBuffer = Instantiate(healthBarSlider, charSS.gameObject.transform);
        //    Vector3 vecbuffer = Camera.main.WorldToScreenPoint(charSS.transform.position);
        //    vecbuffer.y -= 30;
        //    healthbarBuffer.transform.position = vecbuffer;
        //    healthbarBuffer.GetComponent<Slider>().maxValue = charSS.m_maxHealth;
        //    healthbarBuffer.GetComponent<Slider>().value = charSS.m_health;
        //    //healthbarBuffer.transform.parent = gameObject.transform;
        //}

        //foreach (CharacterStatSheet charSS in enemyObjects)
        //{
        //    GameObject healthbarBuffer = Instantiate(healthBarSlider, charSS.gameObject.transform);
        //    Vector3 vecbuffer = Camera.main.WorldToScreenPoint(charSS.transform.position);
        //    vecbuffer.y -= 30;
        //    healthbarBuffer.transform.position = vecbuffer;
        //    healthbarBuffer.GetComponent<Slider>().maxValue = charSS.m_maxHealth;
        //    healthbarBuffer.GetComponent<Slider>().value = charSS.m_health;
        //    //healthbarBuffer.transform.parent = gameObject.transform;
        //}
    }
	
	// Update is called once per frame
	void Update () {
        if (BattleActive == true && PlayerTurn == true && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity) && hitInfo.collider.tag == "Enemy")
            {
                if (hitInfo.collider.gameObject.GetComponent<CharacterStatSheet>().m_isDead == false)
                {
                    m_attackingCharacter = hitInfo.collider.gameObject.GetComponent<CharacterStatSheet>();
                }
            }
        }
    }

    CharacterStatSheet[] GetAttackingTeam()
    {
        if (PlayerTurn == true)
            return friendlyObjects;
        return enemyObjects;
    }

    CharacterStatSheet[] GetDefendingTeam()
    {
        if (PlayerTurn == false)
            return friendlyObjects;
        return enemyObjects;
    }

    CharacterStatSheet GetCurrentMover()
    {
        return GetAttackingTeam()[m_playerCount];
    }

    public void SetPlayerTeamTurn()
    {
        //PlayerTurn = true;
        SetPlayerButtons(true);
        m_attackingCharacter = null;
    }

    public void SetEnemyTeamTurn()
    {
        //PlayerTurn = false;
        SetPlayerButtons(false);
        m_attackingCharacter = friendlyObjects[0];
    }

    void SetPlayerButtons(bool status)
    {
        SetAttackButton(status);
        SetFleeButton(status);
        SetMagicButton(status);
        SetEndTurnButton(status);
    }

    void SetAttackButton(bool newActive)
    {
        battleMenu.AttackButton.gameObject.SetActive(newActive);
    }

    void SetFleeButton(bool newActive)
    {
        battleMenu.FleeButton.gameObject.SetActive(newActive);
    }

    void SetMagicButton(bool newActive)
    {
        battleMenu.MagicButton.gameObject.SetActive(newActive);
    }

    void SetEndTurnButton(bool newActive)
    {
        battleMenu.EndTurnButton.gameObject.SetActive(newActive);
    }

    public void EndTurnPressed()
    {
        NextPlayerTurn();
    }

    void NextPlayerTurn()
    {
        StartCoroutine(nextPlayerTurn());
    }

    IEnumerator nextPlayerTurn()
    {
        m_previousAttacker = GetCurrentMover();
        m_playerCount++;
        if (m_playerCount >= GetAttackingTeam().Length)
        {
           PlayerTurn = !PlayerTurn;
            if (PlayerTurn == false)
            {
                SetPlayerButtons(false);
                m_attackingCharacter = null;
            }
        }
        yield return new WaitForSeconds(GetCurrentMover().GetAnimatorStateInfo().length);
        if (PlayerTurn == false && enemyObjects.Length > 0)
        {
            EnemiesAttack();
        }

        if (PlayerTurn == true && BattleOver != true)
        {
            SetPlayerButtons(true);
            m_attackingCharacter = null;
        }

        if (BattleOver)
        {
            SetPlayerButtons(false);
            if (friendlyObjects.Length > 0)
            {
                friendlyObjects[0].GetComponentInParent<Rigidbody>().isKinematic = false;
                friendlyObjects[0].GetComponentInParent<PlayerMovement>().enabled = true;
            }
        }
    }

    public void MeleeButtonPressed()
    {
        if (m_attackingCharacter != null)
        {

            //Play Animation 
            Debug.Log("Melee " + m_attackingCharacter.gameObject.name.ToString());
            MeleeAttack(GetAttackingTeam()[m_playerCount]);


            SetAttackButton(false);
            //if (animationPlaying == false)
            NextPlayerTurn();
        }
    }

    public void MagicButtonPressed()
    {
        if (m_attackingCharacter != null)
        {
            //Play Animation 
            Debug.Log("Magic " + m_attackingCharacter.gameObject.name.ToString());
            MagicAttack(GetCurrentMover(), 0);
            //Debug.Log("Magic " + m_attackingCharacter.gameObject.name.ToString());
            SetMagicButton(false);
            NextPlayerTurn();
        }

    }

    public void MeleeAttack(CharacterStatSheet attackingPlayer)
    {
        animationPlaying = true;
        //StopCoroutine("Attacking");
        GetCurrentMover().m_animator.Play("MeleeAttack");
        StartCoroutine(Attacking(attackingPlayer.m_weapon));
    }

    IEnumerator Attacking(WeaponBase weapontoUse)
    {
        CharacterStatSheet attacker = GetCurrentMover();
        CharacterStatSheet attackerBuffer = m_attackingCharacter;
        int turnbuffer = m_playerCount;
        bool playerTurnBuffer = PlayerTurn;
        Debug.Log(GetAttackingTeam()[m_playerCount].name + " is attacking ");
        yield return new WaitUntil(() => attacker.m_attacking);
        animationPlaying = false;
        attackerBuffer.m_health -= weapontoUse.GetAttack();
        attackerBuffer.ReCheckHealth();
        Debug.Log("Attack Hit");
        //if(PlayerTurn != playerTurnBuffer)
        //{
        //    if (PlayerTurn == true)
        //        friendlyObjects[turnbuffer].m_health = attackerBuffer.m_health;
        //    else
        //        enemyObjects[turnbuffer].m_health = attackerBuffer.m_health;
        //} else
        //{
        //    if (PlayerTurn == true)
        //        enemyObjects[turnbuffer].m_health = attackerBuffer.m_health;
        //    else
        //        friendlyObjects[turnbuffer].m_health = attackerBuffer.m_health;
        //}


        if(attackerBuffer.DeathCheck())
        {
            if (playerTurnBuffer == true)
                enemyObjects = ResizeArrayOnDeath(enemyObjects);
            else
                friendlyObjects = ResizeArrayOnDeath(friendlyObjects);
            if (enemyObjects.Length == 0)
            {
                Debug.Log("Battle Over, You Win");
                BattleOver = true;
            }
            else if (friendlyObjects.Length == 0)
            {
                Debug.Log("Battle Over, You Lose");
                BattleOver = true;
            }

        }
    }

    public void StartBattle(CharacterStatSheet[] friendlyPlayers, CharacterStatSheet[] enemyPlayers)
    {
        foreach (CharacterStatSheet charSS in friendlyPlayers)
        {
            charSS.GetHealthBar().gameObject.SetActive(true);
            //GameObject healthbarBuffer = Instantiate(healthBarSlider, charSS.transform.GetChild(0).transform);
            Vector3 vecbuffer = Camera.main.WorldToScreenPoint(charSS.transform.position);
            vecbuffer.y += 30;
            charSS.GetHealthBar().transform.position = vecbuffer;
            charSS.GetHealthBar().GetComponent<Slider>().maxValue = charSS.m_maxHealth;
            charSS.GetHealthBar().GetComponent<Slider>().value = charSS.m_health;
            //healthbarBuffer.transform.SetParent(charSS.transform.GetChild(0).transform);
        }

        //Straight Characters given
        foreach (CharacterStatSheet charSS in enemyPlayers)
        {
            charSS.GetHealthBar().gameObject.SetActive(true);
            //GameObject healthbarBuffer = Instantiate(healthBarSlider, charSS.transform.GetChild(0).transform);
            Vector3 vecbuffer = Camera.main.WorldToScreenPoint(charSS.transform.position);
            vecbuffer.y += 30;
            charSS.GetHealthBar().transform.position = vecbuffer;
            charSS.GetHealthBar().transform.localScale = new Vector3(1, 1, 1);
            charSS.GetHealthBar().GetComponent<Slider>().maxValue = charSS.m_maxHealth;
            charSS.GetHealthBar().GetComponent<Slider>().value = charSS.m_health;
            //healthbarBuffer.transform.SetParent(charSS.transform.GetChild(0).transform);
        }

        SetPlayers(friendlyPlayers);
        SetEnemy(enemyPlayers);
    }

    public void SetEnemy(CharacterStatSheet[] enemyPlayers)
    {
        for(int j = 0; j < enemyObjects.Length; j++)
        {
           enemyObjects[j] = null;
        }
        enemyObjects = new CharacterStatSheet[enemyPlayers.Length];
        int i = 0;
        foreach(CharacterStatSheet charSS in enemyPlayers)
        {
            enemyObjects[i] = charSS;
            i++;
        }
    }

    public void SetPlayers(CharacterStatSheet[] friendlyPlayers)
    {
        for (int j = 0; j < friendlyObjects.Length; j++)
        {
            friendlyObjects[j] = null;
        }
        friendlyObjects = new CharacterStatSheet[friendlyPlayers.Length];
        int i = 0;
        foreach (CharacterStatSheet charSS in friendlyPlayers)
        {
            friendlyObjects[i] = charSS;
            i++;
        }
    }

    void EnemiesAttack()
    {
        if (GetAttackingTeam()[m_playerCount].m_isDead == false)
        {
            EnemyTurn();
        }
        NextPlayerTurn();
    }

    void EnemyTurn()
    {
        EnemyDecideTarget();
        EnemyDecideAttack();
    }

    void EnemyDecideTarget()
    {
        int playerToAttack = Random.Range(0, friendlyObjects.Length);
        while (friendlyObjects[playerToAttack] == null)
        {
            playerToAttack = Random.Range(0, friendlyObjects.Length);
        }
        m_attackingCharacter = friendlyObjects[playerToAttack];
    }

    void EnemyDecideAttack()
    {
        if(Random.Range(0, 1) >= 0.5f && enemyObjects[m_playerCount].m_knowMagic == true)
        {
            int spellIndex = Random.Range(0, enemyObjects[m_playerCount].m_spells.Length);
            if(enemyObjects[m_playerCount].m_spells[spellIndex].m_actualCooldown > 0)
            {
                MeleeAttack(GetAttackingTeam()[m_playerCount]);
                return;
            }
            MagicAttack(GetAttackingTeam()[m_playerCount], spellIndex);
        }
        else
        {
            MeleeAttack(GetAttackingTeam()[m_playerCount]);
        }
    }

    void MagicAttack(CharacterStatSheet attackingCharacter, int spellIndex)
    {
        GetCurrentMover().m_animator.Play("MagicAttack");
        StartCoroutine(Attacking(attackingCharacter.m_spells[spellIndex]));
        //Attacking(attackingCharacter.m_spells[spellIndex]);
    }

    CharacterStatSheet[] ResizeArrayOnDeath(CharacterStatSheet[] oldArray)
    {
        //
        int playerCounter = 0;
        foreach (CharacterStatSheet charSS in oldArray)
        {
            if (charSS != null && charSS.m_isDead != true)
                playerCounter++;
        }
        CharacterStatSheet[] newArray = new CharacterStatSheet[playerCounter];
        for (int i = 0; i < newArray.Length; i++)
        {
            if (oldArray[i] != null && oldArray[i].m_isDead != true)
                newArray[i] = oldArray[i];
        }
        return newArray;
    }
}
