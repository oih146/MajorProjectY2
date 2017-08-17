﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
public class TurnBasedScript : MonoBehaviour {

    //Event called at the end of active player's turn
    public delegate void OnTurnEndEvent();
    public static event OnTurnEndEvent OnTurnEnd;

    public delegate void ChosenAttack();
    public static ChosenAttack AttackToPlay;

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
            //if(value == true)
            //{
            //    SetPlayerButtons(true);
            //}
            //else
            //{
            //    SetPlayerButtons(false);
            //}
            m_isFighting = value;
        }
    }
    public Vector3 attackPos;
    public bool m_playerMoving;
    public bool m_playerChoosing;
    public bool BattleOver = false;
    public bool WonBattleQ;
    private int m_playerCount;
    public CharacterStatSheet[] friendlyObjects = new CharacterStatSheet[0];
    public CharacterStatSheet[] enemyObjects = new CharacterStatSheet[0];
    public CharacterStatSheet m_attackingCharacter;
    public CharacterStatSheet m_previousAttacker;
    CharacterStatSheet m_decidingCharacter;
    RaycastHit hitInfo;
    public GameObject healthBarSlider;
    public bool animationPlaying;
    // Use this for initialization
    void Start () {
        OnTurnEnd += FindActivePlayer;
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
                if (hitInfo.collider.gameObject.GetComponent<CharacterStatSheet>().m_isDead == false && m_decidingCharacter != null)
                {
                    m_decidingCharacter.m_playerToAttack = hitInfo.collider.gameObject.GetComponent<CharacterStatSheet>();
                }
            }
        }
        if (m_playerMoving == false && m_playerChoosing == false && BattleActive == true)
        {
            for (int i = 0; i < friendlyObjects.Length; i++)
            {
                if (friendlyObjects[i] != null && friendlyObjects[i].GetCombatBar().GetComponent<CombatSliderScript>().CombatActive == false
                    && friendlyObjects[i].m_decidedAttack == false)
                {
                    friendlyObjects[i].GetCombatBar().GetComponent<CombatSliderScript>().Restart();
                    friendlyObjects[i].GetCombatBar().GetComponent<CombatSliderScript>().CombatActive = true;
                }

                if (friendlyObjects[i].GetCombatBar().value > 0.9 && friendlyObjects[i].m_decidedAttack == false)
                {
                    m_attackingCharacter = null;
                    SetCombatBarMovement(false);
                    m_playerChoosing = true;
                    SetPlayerButtons(true);
                    m_decidingCharacter = friendlyObjects[i];
                    return;
                }

                if (friendlyObjects[i].GetCombatBar().value == friendlyObjects[i].GetCombatBar().maxValue)
                {
                    PlayerTurn = true;
                    SetCombatBarMovement(false);
                    m_playerMoving = true;
                    Attack(friendlyObjects[i]);
                    return;

                }
            }
        }
        if (m_playerMoving == false && m_playerChoosing == false && BattleActive == true)
        {
            for (int i = 0; i < enemyObjects.Length; i++)
            {
                if (enemyObjects[i] != null && enemyObjects[i].GetCombatBar().GetComponent<CombatSliderScript>().CombatActive == false
                    && enemyObjects[i].m_decidedAttack == false)
                {
                    enemyObjects[i].GetCombatBar().GetComponent<CombatSliderScript>().CombatActive = true;
                    enemyObjects[i].GetCombatBar().GetComponent<CombatSliderScript>().Restart();
                }

                if (enemyObjects[i].GetCombatBar().value > 0.9 && enemyObjects[i].m_decidedAttack == false)
                {
                    m_attackingCharacter = null;
                    SetCombatBarMovement(false);

                    m_playerChoosing = true;
                    EnemyDecideTarget(i);
                    return;
                }

                if (enemyObjects[i].GetCombatBar().value == enemyObjects[i].GetCombatBar().maxValue)
                {
                    PlayerTurn = false;
                    SetCombatBarMovement(false);
                    m_playerMoving = true;
                    Attack(enemyObjects[i]);
                    return;

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

    public void SetCombatUI(bool status)
    {
        battleMenu.CombatBar.SetActive(status);
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
        battleMenu.MagicButtonRoot.gameObject.SetActive(newActive);
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
        OnTurnEnd();
    }

    public void FindActivePlayer()
    {
        //    CharacterStatSheet playerToGoNext;
        //    float friendlyCombatBarMax = 0;
        //    PlayerTurn = true;
        //    for (int i = 0; i < friendlyObjects.Length; i++)
        //    {
        //        if (friendlyObjects[i] != null && friendlyObjects[i].GetCombatBar().value > 90 && friendlyObjects[i].GetCombatBar().value > friendlyCombatBarMax && friendlyObjects[i].m_isDead != true)
        //        {
        //            m_playerCount = i;
        //            playerToGoNext = friendlyObjects[i];
        //            friendlyCombatBarMax = friendlyObjects[i].GetCombatBar().value;
        //        }
        //    }

        //    for (int i = 0; i < enemyObjects.Length; i++)
        //    {
        //        if (enemyObjects[i] != null && enemyObjects[i].GetCombatBar().value > 90 && enemyObjects[i].GetCombatBar().value > friendlyCombatBarMax && enemyObjects[i].m_isDead != true)
        //        {
        //            m_playerCount = i;
        //            PlayerTurn = false;
        //            playerToGoNext = enemyObjects[i];
        //            friendlyCombatBarMax = enemyObjects[i].GetCombatBar().value;
        //        }
        //    }

        //NextPlayerTurn();
    }

    void NextPlayerTurn()
    {
        StartCoroutine(nextPlayerTurn());
    }

    IEnumerator nextPlayerTurn()
    {
        SetTurnPointer();
        yield return new WaitForSeconds(GetCurrentMover().GetAnimatorStateInfo().length);
        if (BattleOver)
        {
            EndBattle(WonBattleQ);
            yield break;
        }
        m_previousAttacker = GetCurrentMover();
        turnPointer.gameObject.SetActive(true);
        if (PlayerTurn == false && enemyObjects.Length > 0 && BattleOver != true)
        {
            SetPlayerButtons(false);
            m_attackingCharacter = null;
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

    public void SetCombatBarMovement(bool status)
    {
        foreach(CharacterStatSheet charSS in friendlyObjects)
        {
            charSS.GetCombatBar().GetComponent<CombatSliderScript>().CombatActive = status;
        }

        foreach (CharacterStatSheet charSS in enemyObjects)
        {
            charSS.GetCombatBar().GetComponent<CombatSliderScript>().CombatActive = status;
        }
    }

    public void MeleeButtonPressed(int meleetype)
    {
        MeleeButton(meleetype);
    }

    public void MeleeButton(int meleeType)
    {
        if (m_decidingCharacter.m_playerToAttack != null)
        {
            //Play Animation 
            //Debug.Log("Melee " + m_attackingCharacter.gameObject.name.ToString());
            m_decidingCharacter.m_ActiveWeapon = m_decidingCharacter.m_weapon;
            m_decidingCharacter.m_attackStrength = (AttackStrength)meleeType;
            m_decidingCharacter.m_decidedAttack = true;
            m_playerChoosing = false;
            SetPlayerButtons(false);
            SetCombatBarMovement(true);
            m_decidingCharacter = null;
        }
    }

    public void MagicButtonPressed(int magicSpell)
    {
        if (m_decidingCharacter.m_playerToAttack != null)
        {
            bool magicAvaliable = false;
            for (int i = battleMenu.spellCharges.Length - 1; i >= 0; i--)
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
                m_decidingCharacter.m_ActiveWeapon = m_decidingCharacter.m_spells[magicSpell];
                m_decidingCharacter.m_decidedAttack = true;
                //Play Animation 
                //Debug.Log("Magic " + m_attackingCharacter.gameObject.name.ToString());
                //MagicAttack(GetCurrentMover(), 0);
                //Debug.Log("Magic " + m_attackingCharacter.gameObject.name.ToString());

                SetMagicButton(false);
                SetCombatBarMovement(true);
                m_decidingCharacter = null;
            }
        }

    }
    public void Attack(CharacterStatSheet characterAttacking)
    {
        StartCoroutine(ToAttack(characterAttacking));
    }

    IEnumerator ToAttack(CharacterStatSheet characterAttacking)
    {
        characterAttacking.m_animator.SetFloat("AttackType", (float)characterAttacking.m_attackStrength);
        characterAttacking.m_animator.Play(characterAttacking.m_ActiveWeapon.GetAnimationToPlay().name);
        turnPointer.gameObject.SetActive(false);
        StartCoroutine(Attacking(characterAttacking));
        CharacterStatSheet attacker = characterAttacking;
        yield return new WaitForSeconds(attacker.GetAnimatorStateInfo().length);
        m_playerMoving = false;
        SetCombatBarMovement(true);
        attacker.GetCombatBar().GetComponent<CombatSliderScript>().Restart();
        attacker.m_decidedAttack = false;
    }

    IEnumerator Attacking(CharacterStatSheet characterAttacking)
    {
        CharacterStatSheet attacker = characterAttacking;
        CharacterStatSheet attackerBuffer = characterAttacking.m_playerToAttack;
        bool playerTurnBuffer = PlayerTurn;
        Debug.Log(characterAttacking.name + " is attacking ");
        yield return new WaitUntil(() => attacker.m_attacking);
        animationPlaying = false;
        attacker.GetCombatBar().value = 0;
        Debug.Log("Attacking");
        if (attacker.m_ActiveWeapon.m_attackAll == false)
        {
            attackerBuffer.TakeDamage(attacker.m_ActiveWeapon.GetAttack());
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
                charSS.TakeDamage(attacker.m_ActiveWeapon.GetAttack());
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
        SetCombatUI(true);
        m_playerMoving = false;
        m_playerChoosing = false;
        
        foreach (CharacterStatSheet charSS in friendlyPlayers)
        {
            //Setup HealthBar;
            charSS.GetHealthBar().gameObject.SetActive(true);
            //GameObject healthbarBuffer = Instantiate(healthBarSlider, charSS.transform.GetChild(0).transform);
            Vector3 vecbuffer = Camera.main.WorldToScreenPoint(charSS.transform.position);
            vecbuffer.y += 35;
            charSS.GetHealthBar().transform.position = vecbuffer;
            charSS.GetHealthBar().GetComponent<Slider>().maxValue = charSS.m_maxHealth;
            charSS.GetHealthBar().GetComponent<Slider>().value = charSS.m_health;
            //healthbarBuffer.transform.SetParent(charSS.transform.GetChild(0).transform);

            //Setup CombatBar
            charSS.GetCombatBar().value = 0;
            charSS.GetCombatBar().gameObject.SetActive(true);

            charSS.m_decidedAttack = false;
        }

        //Straight Characters given
        foreach (CharacterStatSheet charSS in enemyPlayers)
        {
            //Setup HealthBar
            charSS.GetHealthBar().gameObject.SetActive(true);
            //GameObject healthbarBuffer = Instantiate(healthBarSlider, charSS.transform.GetChild(0).transform);
            Vector3 vecbuffer = Camera.main.WorldToScreenPoint(charSS.transform.position);
            vecbuffer.y += 85;
            charSS.GetHealthBar().transform.position = vecbuffer;
            charSS.GetHealthBar().GetComponent<Slider>().maxValue = charSS.m_maxHealth;
            charSS.GetHealthBar().GetComponent<Slider>().value = charSS.m_health;
            //healthbarBuffer.transform.SetParent(charSS.transform.GetChild(0).transform);

            //Setup CombatBar
            charSS.GetCombatBar().value = 0;
            charSS.GetCombatBar().gameObject.SetActive(true);

            charSS.m_decidedAttack = false;
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

        //Disable Inactive Spell Buttons
        //for(int i = 0; i < battleMenu.magicButtons.Length; i++)
        //{
        //    if(friendlyPlayers[0].m_spells[i].ca)
        //}

        //Meant to reposition ally health bar, follow script throws off placement
        //Vector3 otherbuff = Camera.main.WorldToScreenPoint(friendlyObjects[1].transform.position);
        //otherbuff.y += 35;
        //friendlyObjects[1].GetHealthBar().transform.position = otherbuff;
        //SetTurnPointer();
        //OnTurnEnd();
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
            //EnemyTurn();
        }
        OnTurnEnd();
    }

    void EnemyDecideTarget(int enemyIndex)
    {
        int playerToAttack = Random.Range(0, friendlyObjects.Length - 1);
        while (friendlyObjects[playerToAttack] == null)
        {
            playerToAttack = Random.Range(0, friendlyObjects.Length - 1);
        }
        enemyObjects[enemyIndex].m_playerToAttack = friendlyObjects[playerToAttack];

        if (Random.Range(0, 1) >= 0.5f && enemyObjects[m_playerCount].m_knowMagic == true)
        {
            int spellIndex = Random.Range(0, enemyObjects[m_playerCount].m_spells.Length);
            if (enemyObjects[m_playerCount].m_spells[spellIndex].m_actualCooldown > 0)
            {
                enemyObjects[enemyIndex].m_ActiveWeapon = enemyObjects[enemyIndex].m_weapon;
                return;
            }
            enemyObjects[enemyIndex].m_ActiveWeapon = enemyObjects[enemyIndex].m_spells[spellIndex];
        }
        else
        {
            enemyObjects[enemyIndex].m_ActiveWeapon = enemyObjects[enemyIndex].m_weapon;
        }
        m_playerChoosing = false;
        enemyObjects[enemyIndex].m_decidedAttack = true;
        SetCombatBarMovement(true);
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
        SetCombatUI(false);
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
