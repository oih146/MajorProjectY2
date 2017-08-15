using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
public class TurnBasedScript : MonoBehaviour {

    public BattleMenuScript battleMenu;

    public PointerBounce turnPointer;
    bool m_cancelAttack;
    bool m_moveOn;
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
    public Vector3 attackPos;
    public bool BattleOver = false;
    public bool WonBattleQ;
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
        //SetFleeButton(status);
        SetMagicButton(status);
        SetEndTurnButton(status);
        SetSpellRoot(status);
        //SetSpellCharges(status);
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

    void SetSpellRoot(bool newActive)
    {
        battleMenu.SpellChargeRoot.SetActive(newActive);
    }

    void SetSpellCharges(bool newActive)
    {
        foreach(Image imag in battleMenu.spellCharges)
        {
            imag.gameObject.SetActive(newActive);
        }
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
        yield return new WaitForSeconds(GetCurrentMover().GetAnimatorStateInfo().length);
        if (BattleOver)
        {
            EndBattle(WonBattleQ);
            yield break;
        }
        //attackPos = GetCurrentMover().GetComponentInParent<Transform>().position;
        //attackPos.z = 0;
        //GetCurrentMover().GetComponentInParent<Transform>().position = attackPos;
        //attackPos = m_attackingCharacter.GetComponentInParent<Transform>().position;
        //attackPos.z = 0;
        //m_attackingCharacter.GetComponentInParent<Transform>().position = attackPos;
        m_previousAttacker = GetCurrentMover();
        m_playerCount++;
        turnPointer.gameObject.SetActive(true);
        if (m_playerCount >= GetAttackingTeam().Length)
        {
           PlayerTurn = !PlayerTurn;
            if (PlayerTurn == false)
            {
                SetPlayerButtons(false);
                m_attackingCharacter = null;
            }
        }
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
            EndBattle(WonBattleQ);
        }
        else
            SetTurnPointer();

    }

    public void MeleeButtonPressed()
    {
        StartCoroutine(MeleeButton());
    }

    public IEnumerator MeleeButton()
    {
        m_moveOn = false;
        //Play Animation 
        //Debug.Log("Melee " + m_attackingCharacter.gameObject.name.ToString());
        StartCoroutine(MeleeAttack(GetAttackingTeam()[m_playerCount]));
        yield return new WaitUntil(() => m_moveOn);
        SetAttackButton(false);

        NextPlayerTurn();
    }

    public void MagicButtonPressed()
    {
        bool magicAvaliable = false;
        for(int i = battleMenu.spellCharges.Length - 1; i >= 0; i--)
        {
            if (battleMenu.spellCharges[i].gameObject.activeInHierarchy == true)
            {
                battleMenu.spellCharges[i].gameObject.SetActive(false);
                magicAvaliable = true;
                break;
            }
        }
        if (magicAvaliable)
        {
            //Play Animation 
            //Debug.Log("Magic " + m_attackingCharacter.gameObject.name.ToString());
            StartCoroutine(MagicAttack(GetCurrentMover(), 0));
            //Debug.Log("Magic " + m_attackingCharacter.gameObject.name.ToString());
            SetMagicButton(false);
            NextPlayerTurn();
        }

    }

    IEnumerator MagicAttack(CharacterStatSheet attackingCharacter, int spellIndex)
    {
        if (attackingCharacter.m_spells[spellIndex].m_attackAll == false)
        {
            Debug.Log("Here2");
            yield return new WaitUntil(() => m_attackingCharacter != null);
        }
        GetCurrentMover().m_animator.Play("MagicAttack");
        StartCoroutine(Attacking(attackingCharacter.m_spells[spellIndex]));
        //Attacking(attackingCharacter.m_spells[spellIndex]);
    }

    public IEnumerator MeleeAttack(CharacterStatSheet attackingPlayer)
    {
        if (attackingPlayer.m_weapon.m_attackAll == false)
        {
            Debug.Log("Here2");
            yield return new WaitUntil(() => m_attackingCharacter != null);
        }
        m_moveOn = true;
        animationPlaying = true;
        //StopCoroutine("Attacking");
        Debug.Log("Here");
        GetCurrentMover().m_animator.Play("Stab");
        turnPointer.gameObject.SetActive(false);
        //turnPointer.FindNextPos(Camera.main.WorldToScreenPoint(GetCurrentMover().transform.position));
        //GetCurrentMover().m_animator.Play("MeleeAttack");
        //attackPos = GetCurrentMover().GetComponentInParent<Transform>().position;
        //attackPos.z = -1;
        //GetCurrentMover().GetComponentInParent<Transform>().position = attackPos;
        //attackPos = m_attackingCharacter.GetComponentInParent<Transform>().position;
        //attackPos.z = -1;
        //m_attackingCharacter.GetComponentInParent<Transform>().position = attackPos;
        StartCoroutine(Attacking(attackingPlayer.m_weapon));
        //if (animationPlaying == false)
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
        Debug.Log("Attacking");
        if (weapontoUse.m_attackAll == false)
        {
            attackerBuffer.m_health -= weapontoUse.GetAttack();
            attackerBuffer.ReCheckHealth();
            if (attackerBuffer.DeathCheck())
            {
                if (playerTurnBuffer == true)
                {
                    enemyObjects = ResizeArrayOnDeath(enemyObjects);
                }
                else
                {
                    friendlyObjects = ResizeArrayOnDeath(friendlyObjects);
                    Debug.Log("Battle Over, You Lose");
                    BattleOver = true;
                    WonBattleQ = false;
                }
                if (enemyObjects.Length == 0)
                {
                    Debug.Log("Battle Over, You Win");
                    BattleOver = true;
                    WonBattleQ = true;
                }
            }
        }
        else
        {
            foreach(CharacterStatSheet charSS in GetDefendingTeam())
            {
                charSS.m_health -= weapontoUse.GetAttack();
                charSS.ReCheckHealth();
                if (charSS.DeathCheck())
                {
                    if (playerTurnBuffer == true)
                    {
                        enemyObjects = ResizeArrayOnDeath(enemyObjects);
                    }
                    else
                    {
                        friendlyObjects = ResizeArrayOnDeath(friendlyObjects);
                        Debug.Log("Battle Over, You Lose");
                        BattleOver = true;
                        WonBattleQ = false;
                    }
                    if (enemyObjects.Length == 0)
                    {
                        Debug.Log("Battle Over, You Win");
                        BattleOver = true;
                        WonBattleQ = true;
                    }
                }
            }
        }
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

    }

    public void StartBattle(CharacterStatSheet[] friendlyPlayers, CharacterStatSheet[] enemyPlayers)
    {
        foreach (CharacterStatSheet charSS in friendlyPlayers)
        {
            charSS.GetHealthBar().gameObject.SetActive(true);
            //GameObject healthbarBuffer = Instantiate(healthBarSlider, charSS.transform.GetChild(0).transform);
            Vector3 vecbuffer = Camera.main.WorldToScreenPoint(charSS.transform.position);
            vecbuffer.y += 35;
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
            vecbuffer.y += 85;
            charSS.GetHealthBar().transform.position = vecbuffer;
            charSS.GetHealthBar().GetComponent<Slider>().maxValue = charSS.m_maxHealth;
            charSS.GetHealthBar().GetComponent<Slider>().value = charSS.m_health;
            //healthbarBuffer.transform.SetParent(charSS.transform.GetChild(0).transform);
        }
        m_playerCount = 0;
        SetPlayers(friendlyPlayers);
        SetEnemy(enemyPlayers);
        turnPointer.gameObject.SetActive(true);

        Vector3 buff = friendlyObjects[0].GetHealthBar().transform.position;
        buff.y += 50;
        friendlyObjects[0].GetHealthBar().transform.position = buff;
        PlayerStat dummy = (PlayerStat)friendlyObjects[0];
        for(int i = 0; i < dummy.m_spellsAvaliable; i++)
        {
            battleMenu.spellCharges[i].gameObject.SetActive(true);
        }
        //Meant to reposition ally health bar, follow script throws off placement
        //Vector3 otherbuff = Camera.main.WorldToScreenPoint(friendlyObjects[1].transform.position);
        //otherbuff.y += 35;
        //friendlyObjects[1].GetHealthBar().transform.position = otherbuff;
        SetTurnPointer();
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
            charSS.m_animator.Play("Idle");
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
            charSS.m_animator.Play("Idle");
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
        int playerToAttack = Random.Range(0, friendlyObjects.Length - 1);
        while (friendlyObjects[playerToAttack] == null)
        {
            playerToAttack = Random.Range(0, friendlyObjects.Length - 1);
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
                StartCoroutine(MeleeAttack(GetAttackingTeam()[m_playerCount]));
                return;
            }
            MagicAttack(GetAttackingTeam()[m_playerCount], spellIndex);
        }
        else
        {
            StartCoroutine(MeleeAttack(GetAttackingTeam()[m_playerCount]));
        }
    }

    CharacterStatSheet[] ResizeArrayOnDeath(CharacterStatSheet[] oldArray)
    {
        int playerCounter = 0;
        foreach (CharacterStatSheet charSS in oldArray)
        {
            if (charSS != null && charSS.m_isDead != true)
                playerCounter++;
            else if (charSS.m_isDead == true)
            {
                charSS.GetComponent<BoxCollider>().enabled = false;
                charSS.GetHealthBar().gameObject.SetActive(false);
            }
        }
        CharacterStatSheet[] newArray = new CharacterStatSheet[playerCounter];
        int y = 0;
        for (int i = 0; y < newArray.Length; i++)
        {
            if (oldArray[i] != null && oldArray[i].m_isDead != true)
            {
                newArray[y] = oldArray[i];
                y++;
            }
        }
        return newArray;
    }

    public void EndBattle(bool didWin)
    {
        if (didWin)
        {
            friendlyObjects[0].GetComponentInParent<Rigidbody>().isKinematic = !didWin;
            friendlyObjects[0].GetComponentInParent<PlayerMovement>().enabled = didWin;
            int spellLeft = 0;
            foreach(Image spell in battleMenu.spellCharges)
            {
                if(spell.gameObject.activeInHierarchy == true)
                {
                    spellLeft++;
                }
            }
            PlayerStat playStat = (PlayerStat)friendlyObjects[0];
            playStat.SpellAvaliable = spellLeft;
        }
        SetPlayerButtons(false);
        turnPointer.gameObject.SetActive(false);
        foreach (CharacterStatSheet charSS in friendlyObjects)
        {
            charSS.GetHealthBar().gameObject.SetActive(false);
            charSS.m_animator.Stop();
        }

        //Straight Characters given
        foreach (CharacterStatSheet charSS in enemyObjects)
        {
            charSS.GetHealthBar().gameObject.SetActive(false);
            charSS.m_animator.Stop();
        }

        //if (!didWin)
            
    }

    void SetTurnPointer()
    {
        Vector3 buff = GetCurrentMover().transform.position;
        buff.y += 3;
        turnPointer.FindNextPos(Camera.main.WorldToScreenPoint(buff));
    }
}
