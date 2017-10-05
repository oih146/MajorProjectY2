using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public enum eAttackColors
{
    Neutral,
    Light,
    Medium,
    Heavy,
    Magic
}

public class TurnBasedScript : MonoBehaviour {

    public float m_IPOnHit;
    public float m_IPOnDeath;
    //Event called at the end of active player's turn
    public delegate void OnTurnEndEvent();
    public static event OnTurnEndEvent OnPlayerDeath;

    public delegate void OnSurrenderEvent(EnemyBase enemy);
    public static event OnSurrenderEvent OnPlayerSurrender;

    public BattleMenuScript battleMenu;

    public List<Button> magicCanBePressed;

    public PointerBounce turnPointer;
    bool m_attackDone;
    public bool m_playerTurn;
    public bool m_WhenAttackingTurn; /*When Attacking, whose turn is it? */
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
    public static bool m_playerMoving;
    public static bool m_playerChoosing;
    public bool BattleOver = false;
    public bool WonBattleQ;
    private bool DidFlee = false;
    bool isAllDisarmed = false;
    private int m_playerCount;
    public CharacterStatSheet[] friendlyObjects = new CharacterStatSheet[0];
    public EnemyBase[] enemyObjects = new EnemyBase[0];
    public CharacterStatSheet m_attackingCharacter;
    public CharacterStatSheet m_previousAttacker;
    CharacterStatSheet m_decidingCharacter;
    RaycastHit hitInfo;
    public GameObject healthBarSlider;
    public bool animationPlaying;
    public static TurnBasedScript Instance;
    public Color[] m_attackColors;

    public IEnumerator co;

    void Awake()
    {
        Instance = this;
        OnPlayerDeath += PlayerDied;
        OnPlayerSurrender += Surrendered;
        EnemyBase.IPonHit = m_IPOnHit;
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        //Raycasting that selects enemies during player's turn
        if (BattleActive == true && PlayerTurn == true && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity) && hitInfo.collider.tag == "Enemy")
            {
                if (hitInfo.collider.gameObject.GetComponent<CharacterStatSheet>().m_isDead == false && m_decidingCharacter != null)
                {
                    m_decidingCharacter.m_playerToAttack = hitInfo.collider.gameObject.GetComponent<CharacterStatSheet>();
                    Vector3 temp  = m_decidingCharacter.m_playerToAttack.GetHealthBar().transform.position;
                    temp.y += 2;
                    turnPointer.transform.position = temp;
                    SetTurnPointer(true);
                    turnPointer.StartBump();
                    m_decidingCharacter.m_playerToAttack.GetCombatBar().m_bumpScript.StartBump();
                }
            }
        }
        if (BattleActive == true)
        {
            //Has the battle finished
            //Think up better way to use
            if (BattleOver)
            {
                StartCoroutine(EndBattle(WonBattleQ));
                enabled = false;
                return;
            }
            //Loops through player checking if their turn to attack
            if (m_playerMoving == false && m_playerChoosing == false)
            {
                for (int i = 0; i < friendlyObjects.Length; i++)
                {

                    if (friendlyObjects[i].GetCombatBar().m_combatSlider.value > 0.73 && friendlyObjects[i].m_decidedAttack == false)
                    {
                        //SetTurnPointer(true);
                        //turnPointer.gameObject.SetActive(true);
                        PlayerTurn = true;
                        m_attackingCharacter = null;
                        SetCombatBarMovement(false);
                        m_playerChoosing = true;
                        SetPlayerButtons(true);
                        m_decidingCharacter = friendlyObjects[i];
                        return;
                    }

                    if (friendlyObjects[i].GetCombatBar().m_combatSlider.value == friendlyObjects[i].GetCombatBar().m_combatSlider.maxValue)
                    {
                        //SetTurnPointer(true);
                        PlayerTurn = true;
                        SetCombatBarMovement(false);
                        m_playerMoving = true;
                        Attack(friendlyObjects[i]);
                        return;

                    }
                }

                //Loops through enemies if their turn to attack
                for (int i = 0; i < enemyObjects.Length; i++)
                {

                    if (enemyObjects[i].GetCombatBar().m_combatSlider.value > 0.73 && enemyObjects[i].m_decidedAttack == false)
                    {
                        if (enemyObjects[i].Disarmed == false)
                        {
                            PlayerTurn = false;
                            m_attackingCharacter = null;
                            SetCombatBarMovement(false);

                            m_playerChoosing = true;
                            EnemyDecideTarget(i);
                        }
                        else
                        {
                            enemyObjects[i].m_decidedAttack = true;
                            enemyObjects[i].GetEffectTimeArray()[(int)eEffects.Disarmed]--;
                            enemyObjects[i].GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Heavy]);
                            enemyObjects[i].GetCombatBar().SlowDown((int)AttackStrength.Heavy);
                        }
                        return;
                    }

                    if (enemyObjects[i].GetCombatBar().m_combatSlider.value == enemyObjects[i].GetCombatBar().m_combatSlider.maxValue && enemyObjects[i].m_surrender != true)
                    {
                        if (enemyObjects[i].Disarmed == false)
                        {
                            //SetTurnPointer(true);
                            PlayerTurn = false;
                            SetCombatBarMovement(false);
                            m_playerMoving = true;
                            Attack(enemyObjects[i]);
                        }
                        else
                        {
                            enemyObjects[i].UpdateEffects();
                            if(enemyObjects[i].GetEffectTimeArray()[(int)eEffects.Disarmed] <= 0)
                                enemyObjects[i].Disarmed = false;
                            enemyObjects[i].GetCombatBar().Restart();
                            enemyObjects[i].m_decidedAttack = false;
                            enemyObjects[i].GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Neutral]);
                        }
                        return;

                    }
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
        //SetEndTurnButton(status);
        //SetSpellRoot(status);
        //SetSpellCharges(status);
    }

    void SetAttackButton(bool newActive)
    {
        foreach(Button button in battleMenu.AttackButton.GetComponentsInChildren<Button>())
        {
            button.interactable = newActive;
        }
    }

    //void SetFleeButton(bool newActive)
    //{
    //    battleMenu.FleeButton.gameObject.SetActive(newActive);
    //}

    void SetMagicButton(bool newActive)
    {
        foreach(Button button in magicCanBePressed)
        {
            button.interactable = newActive;
        }
    }

    void SetSpellRoot(bool newActive)
    {
        battleMenu.SpellChargeRoot.SetActive(newActive);
    }

    void SetSpellCharges(bool newActive)
    {
        foreach(GameObject imag in battleMenu.spellCharges)
        {
            imag.SetActive(newActive);
        }
    }

    //void SetEndTurnButton(bool newActive)
    //{
    //    battleMenu.EndTurnButton.gameObject.SetActive(newActive);
    //}

    public void EndTurnPressed()
    {
        //OnTurnEnd();
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
        //SetTurnPointer();
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
            //SetTurnPointer();

    }

    public void SetCombatBarMovement(bool status)
    {
        foreach(CharacterStatSheet charSS in friendlyObjects)
        {
                charSS.GetCombatBar().CombatActive = status;
        }

        foreach (EnemyBase eb in enemyObjects)
        {
            if(eb.m_surrender != true)
                eb.GetCombatBar().CombatActive = status;
        }
    }

    //Wrapper
    public void MeleeButtonPressed(int meleetype)
    {
        MeleeButton(meleetype);
    }

    //Parameter decides attack strength,
    //must match enum of same name
    public void MeleeButton(int meleeType)
    {
        if (m_decidingCharacter.m_playerToAttack != null)
        {
            //Play Animation 
            //Debug.Log("Melee " + m_attackingCharacter.gameObject.name.ToString());
            m_decidingCharacter.m_weapon.m_strength = (AttackStrength)meleeType;
            m_decidingCharacter.m_weapon.CalculateDamage();
            m_decidingCharacter.m_ActiveWeapon = m_decidingCharacter.m_weapon;
            m_decidingCharacter.m_attackStrength = (AttackStrength)meleeType;
            m_decidingCharacter.GetCombatBar().SlowDown(meleeType);
            switch ((AttackStrength)meleeType)
            {
                case AttackStrength.Light:
                    m_decidingCharacter.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Light]);
                    break;
                case AttackStrength.Normal:
                    m_decidingCharacter.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Medium]);
                    break;
                case AttackStrength.Heavy:
                    m_decidingCharacter.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Heavy]);
                    break;
                default:
                    m_decidingCharacter.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Light]);
                    break;
            }

            m_decidingCharacter.m_decidedAttack = true;
            m_playerChoosing = false;
            SetPlayerButtons(false);
            SetCombatBarMovement(true);
            m_decidingCharacter = null;
            SetTurnPointer(false);
        }
    }

    //Similar to melee,
    //Parameter decides which ability in array to use
    public void AbilityButtonPressed(int abilityIndex)
    {
        if ((!m_decidingCharacter.m_abilities[abilityIndex].DoesAttackAll() && m_decidingCharacter.m_playerToAttack != null) ||
            m_decidingCharacter.m_abilities[abilityIndex].DoesAttackAll())
        {
            m_decidingCharacter.m_attackStrength = 0;
            m_decidingCharacter.m_ActiveWeapon = m_decidingCharacter.m_abilities[abilityIndex];
            bool effectActive = false;
            foreach (WeaponBase.WeaponEffect WE in m_decidingCharacter.m_ActiveWeapon.weapAbility)
            {
                if (WE.effectType == eEffects.InteruptHealthMod)
                {
                    effectActive = true;
                    m_decidingCharacter.GetCombatBar().SlowDown(((m_decidingCharacter.m_playerToAttack.Health - 50) / 10));
                    break;
                }
            }
            if (effectActive == false)
                m_decidingCharacter.GetCombatBar().SlowDown((int)m_decidingCharacter.m_ActiveWeapon.m_strength);
            m_decidingCharacter.m_decidedAttack = true;
            //m_decidingCharacter.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)m_decidingCharacter.m_ActiveWeapon.m_strength]);
            switch (m_decidingCharacter.m_ActiveWeapon.m_strength)
            {
                case AttackStrength.Light:
                    m_decidingCharacter.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Light]);
                    break;
                case AttackStrength.Normal:
                    m_decidingCharacter.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Medium]);
                    break;
                case AttackStrength.Heavy:
                    m_decidingCharacter.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Heavy]);
                    break;
                default:
                    m_decidingCharacter.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Light]);
                    break;
            }


            m_playerChoosing = false;
            SetPlayerButtons(false);
            SetCombatBarMovement(true);
            m_decidingCharacter = null;
            SetTurnPointer(false);
        }

    }

    //Similar to melee,
    //Parameter decides which spell in array to use
    public void MagicButtonPressed(int magicSpell)
    {
        if ((!m_decidingCharacter.m_spells[magicSpell].DoesAttackAll() && m_decidingCharacter.m_playerToAttack != null) ||
            m_decidingCharacter.m_spells[magicSpell].DoesAttackAll())
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
                m_decidingCharacter.m_attackStrength = 0;
                m_decidingCharacter.m_ActiveWeapon = m_decidingCharacter.m_spells[magicSpell];
                bool effectActive = false;
                foreach(WeaponBase.WeaponEffect WE in m_decidingCharacter.m_spells[magicSpell].weapAbility)
                {
                    if (WE.effectType == eEffects.InteruptHealthMod)
                    {
                        effectActive = true;
                        m_decidingCharacter.GetCombatBar().SlowDown(((m_decidingCharacter.m_playerToAttack.Health - 50) / 10) -
                            m_decidingCharacter.GetStatistics().GetWillPowerCastTimeDecrease());
                        break;
                    }
                }
                if (effectActive == false)
                    m_decidingCharacter.GetCombatBar().SlowDown((int)m_decidingCharacter.m_ActiveWeapon.m_strength -
                         m_decidingCharacter.GetStatistics().GetWillPowerCastTimeDecrease());
                m_decidingCharacter.m_decidedAttack = true;
                m_decidingCharacter.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Magic]);
                //Play Animation 
                //Debug.Log("Magic " + m_attackingCharacter.gameObject.name.ToString());
                //MagicAttack(GetCurrentMover(), 0);
                //Debug.Log("Magic " + m_attackingCharacter.gameObject.name.ToString());
                m_playerChoosing = false;
                SetPlayerButtons(false);
                SetCombatBarMovement(true);
                m_decidingCharacter = null;
                SetTurnPointer(false);
            }
        }

    }

    public void Attack(CharacterStatSheet characterAttacking)
    {
        StartCoroutine(ToAttack(characterAttacking));
    }

    //Starts attack but waits till attack finishes
    IEnumerator ToAttack(CharacterStatSheet characterAttacking)
    {
        characterAttacking.m_animator.SetFloat("AttackType", (float)characterAttacking.m_attackStrength);
        //characterAttacking.m_animator.Play(characterAttacking.m_ActiveWeapon.GetAnimationToPlay().name);
        //turnPointer.gameObject.SetActive(false);
        bool playerTurnBuffer = PlayerTurn;
        StartCoroutine(Attacking(characterAttacking));
        CharacterStatSheet attacker = characterAttacking;
        yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_attackFinished);
        attacker.m_ActiveWeapon.m_attackFinished = false;
        isAllDisarmed = false;
        foreach (WeaponBase.WeaponEffect we in attacker.m_ActiveWeapon.weapEffects)
        {
            if(we.effectType == eEffects.Disarmed)
            {
                isAllDisarmed = true;
                foreach(CharacterStatSheet charSS in GetDefendingTeam())
                {
                    if(charSS.Disarmed == false)
                    {
                        isAllDisarmed = false;
                        break;   
                    }
                }
            }
        }
        if (isAllDisarmed)
        {
            Debug.Log("Battle Over, You Win");
            BattleOver = true;
            WonBattleQ = true;
        }
        attacker.GetCombatBar().m_combatSlider.value = 0;
        attacker.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Neutral]);
        //SetTurnPointer(false);
        if (BattleOver == true)
            StopAllCoroutines();
        SetCombatBarMovement(true);
        attacker.UpdateEffects();
        attacker.GetCombatBar().Restart();
        if (attacker.DeathCheck())
        {
            if (playerTurnBuffer == false)
            {
                enemyObjects = ResizeArrayOnDeath(enemyObjects);
                if (enemyObjects.Length == 0)
                {
                    Debug.Log("Battle Over, You Win");
                    BattleOver = true;
                    WonBattleQ = true;
                }
            }
            else
            {
                friendlyObjects = ResizeArrayOnDeath(friendlyObjects);
                Debug.Log("Battle Over, You Lose");
                BattleOver = true;
                WonBattleQ = false;
            }
        }
        attacker.GetCombatBar().CombatActive = true;
        m_playerMoving = false;
        attacker.m_decidedAttack = false;
        attacker.m_playerToAttack = null;
    }

    //Where attack type is decided
    IEnumerator Attacking(CharacterStatSheet characterAttacking)
    {
        CharacterStatSheet attacker = characterAttacking;
        CharacterStatSheet attackerBuffer = characterAttacking.m_playerToAttack;
        m_WhenAttackingTurn = PlayerTurn;
        Debug.Log(characterAttacking.name + " is attacking ");
        #region CommentedCode
        /*Is move Offensive
        //if (!attacker.m_ActiveWeapon.m_defensive)
        //{
        //    //Weapon attacks all enemies
        //    if (attacker.m_ActiveWeapon.m_attackAll == false)
        //    {
        //        //Check if doesn't have multiple hits
        //        if (!attacker.m_ActiveWeapon.m_multipleHits)
        //        {
        //            //Attack on person for single hit
        //            attackerBuffer.TakeDamage(attacker.m_ActiveWeapon.GetAttack() + ((int)attacker.m_attackStrength * 5),
        //                attacker.GetStatistics(),
        //                (attacker.GetEffectTimeArray()[(int)eEffects.InteruptModifier] > 0) ? attacker.GetEffectArray()[(int)eEffects.InteruptModifier] : 1);
        //            attackerBuffer.ReCheckHealth();
        //            if (attackerBuffer.DeathCheck())
        //            {
        //                if (m_WhenAttackingTurn == true)
        //                {
        //                    enemyObjects = ResizeArrayOnDeath(enemyObjects);
        //                    if (enemyObjects.Length == 0)
        //                    {
        //                        Debug.Log("Battle Over, You Win");
        //                        BattleOver = true;
        //                        WonBattleQ = true;
        //                    }
        //                }
        //                else
        //                {
        //                    friendlyObjects = ResizeArrayOnDeath(friendlyObjects);
        //                    Debug.Log("Battle Over, You Lose");
        //                    BattleOver = true;
        //                    WonBattleQ = false;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //Attack one person for multiple hits
        //            for (int i = 0; i < attacker.m_ActiveWeapon.m_howManyHits - 1; i++)
        //            {
        //                yield return new WaitForSeconds(attacker.GetAnimatorStateInfo().length);
        //                attacker.m_animator.Play(attacker.m_ActiveWeapon.GetAnimationToPlay().name);
        //                yield return new WaitUntil(() => attacker.m_attacking);
        //                attackerBuffer.TakeDamage(attacker.m_ActiveWeapon.GetAttack() + ((int)attacker.m_attackStrength * 5),
        //                    attacker.GetStatistics(),
        //                (attacker.GetEffectTimeArray()[(int)eEffects.InteruptModifier] > 0) ? attacker.GetEffectArray()[(int)eEffects.InteruptModifier] : 1);
        //                attackerBuffer.ReCheckHealth();
        //                if (attackerBuffer.DeathCheck())
        //                {
        //                    if (m_WhenAttackingTurn == true)
        //                    {
        //                        enemyObjects = ResizeArrayOnDeath(enemyObjects);
        //                        if (enemyObjects.Length == 0)
        //                        {
        //                            Debug.Log("Battle Over, You Win");
        //                            BattleOver = true;
        //                            WonBattleQ = true;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        friendlyObjects = ResizeArrayOnDeath(friendlyObjects);
        //                        Debug.Log("Battle Over, You Lose");
        //                        BattleOver = true;
        //                        WonBattleQ = false;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    //Weapon Attacks all
        //    else
        //    {
        //        //does weapon have multiple hits
        //        if (!attacker.m_ActiveWeapon.m_multipleHits)
        //        {
        //            //Weapon attacks all enemies with single hit
        //            foreach (CharacterStatSheet charSS in GetDefendingTeam())
        //            {
        //                charSS.TakeDamage(attacker.m_ActiveWeapon.GetAttack() + ((int)attacker.m_attackStrength * 5),
        //                    attacker.GetStatistics(),
        //                    (attacker.GetEffectTimeArray()[(int)eEffects.InteruptModifier] > 0) ? attacker.GetEffectArray()[(int)eEffects.InteruptModifier] : 1);
        //                charSS.ReCheckHealth();
        //                if (charSS.DeathCheck())
        //                {
        //                    if (m_WhenAttackingTurn == true)
        //                    {
        //                        enemyObjects = ResizeArrayOnDeath(enemyObjects);
        //                    }
        //                    else
        //                    {
        //                        friendlyObjects = ResizeArrayOnDeath(friendlyObjects);
        //                        Debug.Log("Battle Over, You Lose");
        //                        BattleOver = true;
        //                        WonBattleQ = false;
        //                    }
        //                    if (enemyObjects.Length == 0)
        //                    {
        //                        Debug.Log("Battle Over, You Win");
        //                        BattleOver = true;
        //                        WonBattleQ = true;
        //                    }
        //                }
        //            }
        //        }
        //        //weapon has multiple hits
        //        else
        //        {
        //            //Weapon attacks all with multiple hits
        //            for (int i = 0; i < attacker.m_ActiveWeapon.m_howManyHits; i++)
        //            {
        //                foreach (CharacterStatSheet charSS in GetDefendingTeam())
        //                {
        //                    yield return new WaitForSeconds(attacker.GetAnimatorStateInfo().length);
        //                    attacker.m_animator.Play(attacker.m_ActiveWeapon.GetAnimationToPlay().name);
        //                    yield return new WaitUntil(() => attacker.m_attacking);
        //                    charSS.TakeDamage(attacker.m_ActiveWeapon.GetAttack() + ((int)attacker.m_attackStrength * 5),
        //                        attacker.GetStatistics(),
        //                        (attacker.GetEffectTimeArray()[(int)eEffects.InteruptModifier] > 0) ? attacker.GetEffectArray()[(int)eEffects.InteruptModifier] : 1);
        //                    charSS.ReCheckHealth();
        //                    if (charSS.DeathCheck())
        //                    {
        //                        if (m_WhenAttackingTurn == true)
        //                        {
        //                            enemyObjects = ResizeArrayOnDeath(enemyObjects);
        //                        }
        //                        else
        //                        {
        //                            friendlyObjects = ResizeArrayOnDeath(friendlyObjects);
        //                            Debug.Log("Battle Over, You Lose");
        //                            BattleOver = true;
        //                            WonBattleQ = false;
        //                        }
        //                        if (enemyObjects.Length == 0)
        //                        {
        //                            Debug.Log("Battle Over, You Win");
        //                            BattleOver = true;
        //                            WonBattleQ = true;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        ////Weapon is defensive
        //else
        //{
        //    //Weapon heals user 
        //    if (!attacker.m_ActiveWeapon.m_attackAll)
        //    {
        //        attacker.HealSelf(attacker.m_ActiveWeapon.GetAttack());
        //        attacker.ReCheckHealth();
        //    }
        //    else
        //    {
        //        //weapon heals all
        //        foreach (CharacterStatSheet charSS in GetAttackingTeam())
        //        {
        //            yield return new WaitForSeconds(attacker.GetAnimatorStateInfo().length);
        //            attacker.m_animator.Play(attacker.m_ActiveWeapon.GetAnimationToPlay().name);
        //            yield return new WaitUntil(() => attacker.m_attacking);
        //            charSS.HealSelf(attacker.m_ActiveWeapon.GetAttack());
        //            charSS.ReCheckHealth();
        //        }
        //    }
        }*/
        #endregion
        m_attackDone = false;
        //attacker.m_ActiveWeapon.ApplyEffects(attacker, attackerBuffer);
        switch (attacker.m_ActiveWeapon.m_attackType)
        {
                //Attack One person once
            case AttackType.SingleOne:
                attacker.m_ActiveWeapon.ApplyEffects(attacker, attackerBuffer);
                co = NormalAttack(attacker, attackerBuffer);
                break;
                //Attack one person multiple times
            case AttackType.MultipleOne:
                attacker.m_ActiveWeapon.ApplyEffects(attacker, attackerBuffer);
                co = MultipleSingle(attacker, attackerBuffer);
                break;
                //Attack all multiple times
            case AttackType.MultipleAll:
                attacker.m_ActiveWeapon.ApplyEffects(attacker, GetDefendingTeam());
                co = MultipleAll(attacker, attackerBuffer);
                break;
                //Attack all once
            case AttackType.SingleAll:
                attacker.m_ActiveWeapon.ApplyEffects(attacker, GetDefendingTeam());
                co = AllSingle(attacker, attackerBuffer);
                break;
                //Heal Self
            case AttackType.HealOne:
                attacker.m_ActiveWeapon.AddAbilities(attacker);
                co = HealOne(attacker);
                break;
            case AttackType.Flee:
                co = Flee(attacker, attackerBuffer);
                break;
            case AttackType.MassAttack:
                attacker.m_ActiveWeapon.ApplyEffects(attacker, GetDefendingTeam());
                co = MassHit(attacker, attackerBuffer);
                break;
            case AttackType.Branching:
                attacker.m_ActiveWeapon.ApplyEffects(attacker, attackerBuffer);
                co = LatchingAttack(attacker, attackerBuffer);
                break;
            case AttackType.Drain:
                attacker.m_ActiveWeapon.ApplyEffects(attacker, attackerBuffer);
                co = DrainHealth(attacker, attackerBuffer);
                break;
            case AttackType.BuffDebuff:
                attacker.m_ActiveWeapon.AddAbilities(attacker);
                co = BuffDebuff(attacker);
                break;
            default:
                attacker.m_ActiveWeapon.ApplyEffects(attacker, attackerBuffer);
                co = NormalAttack(attacker, attackerBuffer);
                break;
        }
        StartCoroutine(co);
        Debug.Log("Attack Hit");
        yield return new WaitUntil(() => m_attackDone);
        switch (attacker.m_ActiveWeapon.DoesAttackAll())
        {
            //Attack One person once
            case true:
                CheckAllPlayers(GetDefendingTeam());
                break;
            //Attack one person multiple times
            case false:
                CheckOnPlayer(attackerBuffer);
                break;
            default:
                CheckAllPlayers(GetDefendingTeam());
                break;
        }
        attacker.m_ActiveWeapon.m_attackFinished = true;
    }

    void CheckAllPlayers(CharacterStatSheet[] defendingTeam)
    {
        foreach (CharacterStatSheet css in defendingTeam)
        {
            if (css.DeathCheck())
            {
                if (m_WhenAttackingTurn == true)
                {
                    enemyObjects = ResizeArrayOnDeath(enemyObjects);
                    if (enemyObjects.Length == 0)
                    {
                        Debug.Log("Battle Over, You Win");
                        BattleOver = true;
                        WonBattleQ = true;
                    }
                }
                else
                {
                    friendlyObjects = ResizeArrayOnDeath(friendlyObjects);
                    Debug.Log("Battle Over, You Lose");
                    BattleOver = true;
                    WonBattleQ = false;
                }
            }
        }
    }

    void CheckOnPlayer(CharacterStatSheet defender)
    {
        if (defender.DeathCheck())
        {
            if (m_WhenAttackingTurn == true)
            {
                enemyObjects = ResizeArrayOnDeath(enemyObjects);
                if (enemyObjects.Length == 0)
                {
                    Debug.Log("Battle Over, You Win");
                    BattleOver = true;
                    WonBattleQ = true;

                }
            }
            else
            {
                friendlyObjects = ResizeArrayOnDeath(friendlyObjects);
                Debug.Log("Battle Over, You Lose");
                BattleOver = true;
                WonBattleQ = false;
            }
        }
    }

    //Setups battles
    public void StartBattle(CharacterStatSheet[] friendlyPlayers, EnemyBase[] enemyPlayers)
    {
        SetCombatUI(true);
        m_playerMoving = false;
        m_playerChoosing = false;
        DidFlee = false;
        magicCanBePressed.Clear();
        
        foreach (CharacterStatSheet charSS in friendlyPlayers)
        {
            //Setup HealthBar;
            charSS.GetHealthBar().gameObject.SetActive(true);
            //GameObject healthbarBuffer = Instantiate(healthBarSlider, charSS.transform.GetChild(0).transform);
            //Vector3 vecbuffer = Camera.main.WorldToScreenPoint(charSS.transform.position);
            //vecbuffer.y += 35;
            //charSS.GetHealthBar().transform.position = vecbuffer;
            //charSS.GetHealthBar().GetComponent<Slider>().maxValue = charSS.MaxHealth;
            //charSS.GetHealthBar().GetComponent<Slider>().value = charSS.Health;
            //healthbarBuffer.transform.SetParent(charSS.transform.GetChild(0).transform);

            //Setup CombatBar
            charSS.GetCombatBar().m_combatSlider.value = 0;
            charSS.GetCombatBar().gameObject.SetActive(true);
            charSS.GetCombatBar().Restart();
            charSS.GetCombatBar().CombatActive = true;

            charSS.m_decidedAttack = false;
        }

        //Straight Characters given
        foreach (CharacterStatSheet echarSS in enemyPlayers)
        {
            //Setup HealthBar
            echarSS.GetHealthBar().gameObject.SetActive(true);
            //GameObject healthbarBuffer = Instantiate(healthBarSlider, charSS.transform.GetChild(0).transform);
            Vector3 vecbuffer = echarSS.transform.position;
            vecbuffer.y += 5;
            echarSS.GetHealthBar().transform.position = vecbuffer;
            echarSS.GetHealthBar().GetComponent<Slider>().maxValue = echarSS.MaxHealth;
            echarSS.GetHealthBar().GetComponent<Slider>().value = echarSS.Health;
            //healthbarBuffer.transform.SetParent(charSS.transform.GetChild(0).transform);

            //Setup CombatBar
            echarSS.GetCombatBar().m_combatSlider.value = 0;
            echarSS.GetCombatBar().gameObject.SetActive(true);
            echarSS.GetCombatBar().Restart();
            echarSS.GetCombatBar().CombatActive = true;

            echarSS.m_decidedAttack = false;
        }
        m_playerCount = 0;
        SetPlayers(friendlyPlayers);
        SetEnemy(enemyPlayers);

        //Vector3 buff = friendlyObjects[0].GetHealthBar().transform.position;
        //buff.y += 50;
        //friendlyObjects[0].GetHealthBar().transform.position = buff;
        PlayerStat dummy = (PlayerStat)friendlyObjects[0];
        for(int i = 0; i < dummy.m_spellsAvaliable; i++)
        {
            battleMenu.spellCharges[i].SetActive(true);
        }
        int spellNum = friendlyPlayers[0].m_spells.Length;
        int abilityNum = friendlyPlayers[0].m_abilities.Length;
        //Disable Inactive Spell Buttons
        for (int t = 0; t < battleMenu.magicButtons.Length; t++)
        {
            if(t < 6)
            {
                if (t < abilityNum)
                    if (friendlyPlayers[0].m_abilities[t].CanUseSpell(friendlyPlayers[0].GetComponent<PlayerStat>().Law, friendlyPlayers[0].GetComponent<PlayerStat>().Light))
                    {
                        //battleMenu.magicButtons[t].interactable = true;
                        magicCanBePressed.Add(battleMenu.magicButtons[t]);
                    }
                    else
                        battleMenu.magicButtons[t].interactable = false;
                else
                    battleMenu.magicButtons[t].interactable = false;
            }
            else
            {
                if (t - 6 < spellNum)
                    if (friendlyPlayers[0].m_spells[t - 6].CanUseSpell(friendlyPlayers[0].GetComponent<PlayerStat>().Law, friendlyPlayers[0].GetComponent<PlayerStat>().Light))
                    {
                        //battleMenu.magicButtons[t].interactable = true;
                        magicCanBePressed.Add(battleMenu.magicButtons[t]);
                    }
                    else
                        battleMenu.magicButtons[t].interactable = false;
                else
                    battleMenu.magicButtons[t].interactable = false;
            }

        }

        //Meant to reposition ally health bar, follow script throws off placement
        //Vector3 otherbuff = Camera.main.WorldToScreenPoint(friendlyObjects[1].transform.position);
        //otherbuff.y += 35;
        //friendlyObjects[1].GetHealthBar().transform.position = otherbuff;
        //SetTurnPointer();
        //OnTurnEnd();
    }

    public void SetEnemy(EnemyBase[] enemyPlayers)
    {
        for(int j = 0; j < enemyObjects.Length; j++)
        {
           enemyObjects[j] = null;
        }
        enemyObjects = new EnemyBase[enemyPlayers.Length];
        int i = 0;
        foreach(EnemyBase charSS in enemyPlayers)
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

    //Not sure if needed
    void EnemiesAttack()
    {
        if (GetAttackingTeam()[m_playerCount].m_isDead == false)
        {
            //EnemyTurn();
        }
        //OnTurnEnd();
    }

    //Enemy decides who to target then how it attacks
    void EnemyDecideTarget(int enemyIndex)
    {
        EnemyBase enemyDeciding = enemyObjects[enemyIndex];
        //if(enemyDeciding.GetEffectTimeArray()[(int)eEffects.Disarmed] > 0)
        //{
        //    enemyDeciding.GetEffectTimeArray()[(int)eEffects.Disarmed]--;
        //    return;
        //}
        int playerToAttack = Random.Range(0, friendlyObjects.Length - 1);
        while (friendlyObjects[playerToAttack] == null)
        {
            playerToAttack = Random.Range(0, friendlyObjects.Length - 1);
        }
        enemyDeciding.m_playerToAttack = friendlyObjects[playerToAttack];

        int attackStrength;
        //decided whether to use magic or not
        if (Random.Range(0, 1) >= 0.5f && enemyDeciding.m_knowMagic == true)
        {
            //using magic
            int spellIndex = Random.Range(0, enemyDeciding.m_spells.Length);          
            enemyDeciding.m_ActiveWeapon = enemyDeciding.m_spells[spellIndex];
            attackStrength = (int)enemyDeciding.m_ActiveWeapon.m_strength;
        }
        else
        {
            //not using magic
            attackStrength = Random.Range(0, 3);
            switch(attackStrength)
            {
                case 0:
                    attackStrength = 3;
                    break;
                case 1:
                    attackStrength = 5;
                    break;
                case 2:
                    attackStrength = 8;
                    break;
                default:
                    attackStrength = 3;
                    break;
            }
            //using melee
            enemyDeciding.m_ActiveWeapon = enemyDeciding.m_weapon;
        }
        //decide enemy attackStrength
        enemyDeciding.GetCombatBar().SlowDown(attackStrength);
        enemyDeciding.m_attackStrength = (AttackStrength)attackStrength;
        switch ((AttackStrength)attackStrength)
        {
            case AttackStrength.Light:
                enemyDeciding.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Light]);
                break;
            case AttackStrength.Normal:
                enemyDeciding.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Medium]);
                break;
            case AttackStrength.Heavy:
                enemyDeciding.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Heavy]);
                break;
            case AttackStrength.Magic:
                enemyDeciding.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Magic]);
                break;
            default:
                enemyDeciding.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Light]);
                break;
        }
        m_playerChoosing = false;
        enemyDeciding.m_decidedAttack = true;
        SetCombatBarMovement(true);
    }

    //For Player
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
                charSS.GetCombatBar().gameObject.SetActive(false);
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

    //For Enemies
    EnemyBase[] ResizeArrayOnDeath(EnemyBase[] oldArray)
    {
        int playerCounter = 0;
        //Enemy with Incap on death
        int infectTime = 0;
        int infectStrength = 0;
        foreach (EnemyBase charSS in oldArray)
        {
            if (charSS != null && charSS.m_isDead != true)
                playerCounter++;
            else if (charSS.m_isDead == true)
            {
                charSS.GetComponent<BoxCollider>().enabled = false;
                charSS.GetHealthBar().gameObject.SetActive(false);
                charSS.GetCombatBar().gameObject.SetActive(false);
                if(charSS.GetEffectTimeArray()[(int)eEffects.BonusIncapPointsOnDeath] > 0)
                {
                    infectTime = (int)charSS.GetEffectTimeArray()[(int)eEffects.BonusIncapPointsOnDeath];
                    infectStrength = (int)charSS.GetEffectArray()[(int)eEffects.BonusIncapPointsOnDeath];
                }
            }
        }
        EnemyBase[] newArray = new EnemyBase[playerCounter];
        int y = 0;
        for (int i = 0; y < newArray.Length; i++)
        {
            if (oldArray[i] != null && oldArray[i].m_isDead != true)
            {
                newArray[y] = oldArray[i];
                newArray[y].IncapacitationPoints += m_IPOnDeath *
                    ((infectTime > 0) ? infectStrength : 1);
                y++;
            }
        }
        return newArray;
    }

    //Quits battle
    public IEnumerator EndBattle(bool didWin)
    {
        BattleActive = false;
        //SetCombatUI(false);
        SetCombatBarMovement(false);
        if (didWin)
        {
            yield return new WaitUntil(() => !friendlyObjects[0].GetAnimatorStateInfo().IsName("Idle"));
            yield return new WaitForSeconds(3f);
            friendlyObjects[0].GetComponentInParent<Rigidbody>().isKinematic = !didWin;
            friendlyObjects[0].GetComponentInParent<PlayerMovement>().enabled = didWin;
            int spellLeft = 0;
            foreach(GameObject spell in battleMenu.spellCharges)
            {
                if(spell.activeInHierarchy == true)
                {
                    spellLeft++;
                }
            }
            PlayerStat playStat = (PlayerStat)friendlyObjects[0];
            playStat.SpellAvaliable = spellLeft;
            //friendlyObjects[0].m_animator.Play("Idle");
        }
        SetPlayerButtons(false);
        turnPointer.gameObject.SetActive(false);
        foreach (CharacterStatSheet charSS in friendlyObjects)
        {
            charSS.ResetEffects();
            //charSS.GetHealthBar().gameObject.SetActive(false);
            charSS.GetCombatBar().gameObject.SetActive(false);
            //charSS.m_animator.Stop();
        }

        //Straight Characters given
        foreach (CharacterStatSheet charSS in enemyObjects)
        {
            charSS.GetCombatBar().gameObject.SetActive(false);
            charSS.GetHealthBar().gameObject.SetActive(false);
            charSS.m_animator.Stop();
            if (DidFlee == true || isAllDisarmed)
                charSS.StartFadeDeath();
        }
        SetCombatUI(false);
        //if (!didWin)

        MusicSwitcher.Instance.StartLerping();

    }

    //Sets pointer
    void SetTurnPointer(bool status)
    {
        //MoveTurnPointer();
        turnPointer.gameObject.SetActive(status);
    }

    //Event for when player dies outside attack
    public static void CallOnOutsideDeath()
    {
        OnPlayerDeath();
    }

    public void PlayerDied()
    {
        enemyObjects = ResizeArrayOnDeath(enemyObjects);
        if (enemyObjects.Length == 0)
        {
            Debug.Log("Battle Over, You Win");
            BattleOver = true;
            WonBattleQ = true;
        }
        friendlyObjects = ResizeArrayOnDeath(friendlyObjects);
        if (friendlyObjects.Length == 0)
        {
            Debug.Log("Battle Over, You Lose");
            BattleOver = true;
            WonBattleQ = false;
        }
    }

    //Event for when player surrenders
    public static void CallOnPlayerSurrender(EnemyBase enemy)
    {
        OnPlayerSurrender(enemy);
    }

    public void Surrendered(EnemyBase enemy)
    {
         enemy.GetCombatBar().SetPortraitBackgroundColor(m_attackColors[(int)eAttackColors.Neutral]);
    }

    //Normal Attack, hits single person for one hit
    public IEnumerator NormalAttack(CharacterStatSheet attacker, CharacterStatSheet defender)
    {
        //Attack on person for single hit
        attacker.m_animator.Play(attacker.m_ActiveWeapon.GetAnimationToPlay().name);
        if (attacker.m_ActiveWeapon.HasEffect)
        {
            if(attacker.m_ActiveWeapon.m_animEffect.m_attachToUser)
                attacker.m_ActiveWeapon.m_animEffect.gameObject.transform.position = attacker.transform.position;
            else
                attacker.m_ActiveWeapon.m_animEffect.gameObject.transform.position = defender.transform.position;
            StartCoroutine(attacker.m_ActiveWeapon.PlayWeaponEffect(attacker));
            attacker.m_animator.SetBool("SpellBreak", false);
            if (attacker.m_ActiveWeapon.m_animEffect.HasAnimation)
                yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.FinishedAnimation);
            else
            {
                yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.m_partSys.isPlaying);
                yield return new WaitUntil(() => !attacker.m_ActiveWeapon.m_animEffect.m_partSys.isEmitting);
            }
            //attacker.m_ActiveWeapon.m_animEffect.StopEffect();

        }
        attacker.m_animator.SetBool("SpellBreak", true);
        yield return new WaitUntil(() => attacker.GetAnimScript().Attacking);
        if (!(defender.GetEffectTimeArray()[(int)eEffects.Invulnerability] > 0))
        {
            attacker.CounterTakeDamage(defender.TakeDamage(attacker.m_ActiveWeapon.GetAttack() + attacker.AdditionalDamage(),
                attacker.GetStatistics(),
                ((attacker.GetEffectTimeArray()[(int)eEffects.InteruptModifier] > 0) ? attacker.GetEffectArray()[(int)eEffects.InteruptModifier] : 1),
                attacker.m_ActiveWeapon.m_strength));
            defender.ReCheckHealth();
            attacker.ReCheckHealth();
        }
        yield return new WaitUntil(() => !attacker.GetAnimatorStateInfo().IsName(attacker.m_ActiveWeapon.GetAnimationToPlay().name));

        m_attackDone = true;

    }

    //Multiple hits on single person
    IEnumerator MultipleSingle(CharacterStatSheet attacker, CharacterStatSheet defender)
    {
        for (int i = 0; i < attacker.m_ActiveWeapon.m_howManyHits; i++)
        {
            attacker.m_animator.Play(attacker.m_ActiveWeapon.GetAnimationToPlay().name);
            if (attacker.m_ActiveWeapon.HasEffect)
            {
                attacker.m_ActiveWeapon.m_animEffect.gameObject.transform.position = defender.transform.position;
                StartCoroutine(attacker.m_ActiveWeapon.PlayWeaponEffect(attacker));
                attacker.m_animator.SetBool("SpellBreak", false);
                if (attacker.m_ActiveWeapon.m_animEffect.HasAnimation)
                    yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.FinishedAnimation);
                else
                {
                    yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.m_partSys.isPlaying);
                    yield return new WaitUntil(() => !attacker.m_ActiveWeapon.m_animEffect.m_partSys.isEmitting);
                }
                attacker.m_ActiveWeapon.m_animEffect.StopEffect();
                attacker.m_animator.SetBool("SpellBreak", true);
            }
            yield return new WaitUntil(() => attacker.GetAnimScript().Attacking);
            if (!(defender.GetEffectTimeArray()[(int)eEffects.Invulnerability] > 0))
            {
                attacker.CounterTakeDamage(defender.TakeDamage(attacker.m_ActiveWeapon.GetAttack() + attacker.AdditionalDamage(),
                attacker.GetStatistics(),
            (attacker.GetEffectTimeArray()[(int)eEffects.InteruptModifier] > 0) ? attacker.GetEffectArray()[(int)eEffects.InteruptModifier] : 1,
                attacker.m_ActiveWeapon.m_strength));
                defender.ReCheckHealth();
                attacker.ReCheckHealth();
            }
            yield return new WaitUntil(() => !attacker.GetAnimatorStateInfo().IsName(attacker.m_ActiveWeapon.GetAnimationToPlay().name));

        }

        m_attackDone = true;
    }

    //Multiple hits on all
    IEnumerator MultipleAll(CharacterStatSheet attacker, CharacterStatSheet defender)
    {
        //Weapon attacks all with multiple hits
        foreach (CharacterStatSheet charSS in GetDefendingTeam())
       {

            for (int i = 0; i < attacker.m_ActiveWeapon.m_howManyHits; i++)
            {
                attacker.m_animator.Play(attacker.m_ActiveWeapon.GetAnimationToPlay().name);
                if (attacker.m_ActiveWeapon.HasEffect)
                {
                    attacker.m_ActiveWeapon.m_animEffect.gameObject.transform.position = defender.transform.position;
                    StartCoroutine(attacker.m_ActiveWeapon.PlayWeaponEffect(attacker));
                    attacker.m_animator.SetBool("SpellBreak", false);
                    if (attacker.m_ActiveWeapon.m_animEffect.HasAnimation)
                        yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.FinishedAnimation);
                    else
                    {
                        yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.m_partSys.isPlaying);
                        yield return new WaitUntil(() => !attacker.m_ActiveWeapon.m_animEffect.m_partSys.isEmitting);
                    }
                    attacker.m_ActiveWeapon.m_animEffect.StopEffect();
                    attacker.m_animator.SetBool("SpellBreak", true);
                }
                yield return new WaitUntil(() => attacker.GetAnimScript().Attacking);
                if (!(defender.GetEffectTimeArray()[(int)eEffects.Invulnerability] > 0))
                {
                    attacker.CounterTakeDamage(defender.TakeDamage(attacker.m_ActiveWeapon.GetAttack()+ attacker.AdditionalDamage(),
                    attacker.GetStatistics(),
                    (attacker.GetEffectTimeArray()[(int)eEffects.InteruptModifier] > 0) ? attacker.GetEffectArray()[(int)eEffects.InteruptModifier] : 1,
                attacker.m_ActiveWeapon.m_strength));
                    defender.ReCheckHealth();
                    attacker.ReCheckHealth();
                }
                yield return new WaitUntil(() => !attacker.GetAnimatorStateInfo().IsName(attacker.m_ActiveWeapon.GetAnimationToPlay().name));

            }
        }
        m_attackDone = true;
    }

    //hit all for single hit
    IEnumerator AllSingle(CharacterStatSheet attacker, CharacterStatSheet defender)
    {
        //Weapon attacks all enemies with single hit
        foreach (CharacterStatSheet charSS in GetDefendingTeam())
        {
            attacker.m_animator.Play(attacker.m_ActiveWeapon.GetAnimationToPlay().name);
            if (attacker.m_ActiveWeapon.HasEffect)
            {
                attacker.m_ActiveWeapon.m_animEffect.gameObject.transform.position = defender.transform.position;
                StartCoroutine(attacker.m_ActiveWeapon.PlayWeaponEffect(attacker));
                attacker.m_animator.SetBool("SpellBreak", false);
                if (attacker.m_ActiveWeapon.m_animEffect.HasAnimation)
                    yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.FinishedAnimation);
                else
                {
                    yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.m_partSys.isPlaying);
                    yield return new WaitUntil(() => !attacker.m_ActiveWeapon.m_animEffect.m_partSys.isEmitting);
                }
                attacker.m_ActiveWeapon.m_animEffect.StopEffect();
                attacker.m_animator.SetBool("SpellBreak", true);
            }
            yield return new WaitUntil(() => attacker.GetAnimScript().Attacking);
            if (!(defender.GetEffectTimeArray()[(int)eEffects.Invulnerability] > 0))
            {
                attacker.CounterTakeDamage(defender.TakeDamage(attacker.m_ActiveWeapon.GetAttack() + attacker.AdditionalDamage(),
                attacker.GetStatistics(),
            (attacker.GetEffectTimeArray()[(int)eEffects.InteruptModifier] > 0) ? attacker.GetEffectArray()[(int)eEffects.InteruptModifier] : 1,
                attacker.m_ActiveWeapon.m_strength));
                defender.ReCheckHealth();
                attacker.ReCheckHealth();
            }
            yield return new WaitUntil(() => !attacker.GetAnimatorStateInfo().IsName(attacker.m_ActiveWeapon.GetAnimationToPlay().name));

        }
        m_attackDone = true;
        yield return null;
    }

    IEnumerator MassHit(CharacterStatSheet attacker, CharacterStatSheet defender)
    {
        attacker.m_animator.Play(attacker.m_ActiveWeapon.GetAnimationToPlay().name);
        if (attacker.m_ActiveWeapon.HasEffect)
        {
            AnimationEffectScript animEffect = attacker.m_ActiveWeapon.m_animEffect;
            Vector3 temp = attacker.m_ActiveWeapon.m_animEffect.GetEffectPosition(attacker, defender, GetDefendingTeam());
            animEffect.gameObject.transform.position = temp;
            StartCoroutine(attacker.m_ActiveWeapon.PlayWeaponEffect(attacker));
            attacker.m_animator.SetBool("SpellBreak", false);
            if (animEffect.HasAnimation)
                yield return new WaitUntil(() => animEffect.FinishedAnimation);
            else
            {
                yield return new WaitUntil(() => animEffect.m_partSys.isPlaying);
                yield return new WaitUntil(() => !animEffect.m_partSys.isEmitting);
            }
            if(animEffect.m_needsToBeStopped)
                animEffect.StopEffect();
            attacker.m_animator.SetBool("SpellBreak", true);
        }
        yield return new WaitUntil(() => attacker.GetAnimScript().Attacking);
        //Weapon attacks all enemies with single hit
        foreach (CharacterStatSheet charSS in GetDefendingTeam())
        {
            if (!(charSS.GetEffectTimeArray()[(int)eEffects.Invulnerability] > 0))
            {
                attacker.CounterTakeDamage(charSS.TakeDamage(attacker.m_ActiveWeapon.GetAttack() + attacker.AdditionalDamage(),
                attacker.GetStatistics(),
            (attacker.GetEffectTimeArray()[(int)eEffects.InteruptModifier] > 0) ? attacker.GetEffectArray()[(int)eEffects.InteruptModifier] : 1,
                attacker.m_ActiveWeapon.m_strength));
                charSS.ReCheckHealth();
                attacker.ReCheckHealth();
            }
        }
        yield return new WaitUntil(() => !attacker.GetAnimatorStateInfo().IsName(attacker.m_ActiveWeapon.GetAnimationToPlay().name));
        m_attackDone = true;
    }

    IEnumerator LatchingAttack(CharacterStatSheet attacker, CharacterStatSheet defender)
    {
        for(int i = 0; i < 1; i++)
        {
            attacker.m_animator.Play(attacker.m_ActiveWeapon.GetAnimationToPlay().name);
            if (attacker.m_ActiveWeapon.HasEffect)
            {
                attacker.m_ActiveWeapon.m_animEffect.gameObject.transform.position = defender.transform.position;
                StartCoroutine(attacker.m_ActiveWeapon.PlayWeaponEffect(attacker));
                attacker.m_animator.SetBool("SpellBreak", false);
                if (attacker.m_ActiveWeapon.m_animEffect.HasAnimation)
                    yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.FinishedAnimation);
                else
                {
                    yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.m_partSys.isPlaying);
                    yield return new WaitUntil(() => !attacker.m_ActiveWeapon.m_animEffect.m_partSys.isEmitting);
                }
                attacker.m_ActiveWeapon.m_animEffect.StopEffect();

            }
            attacker.m_animator.SetBool("SpellBreak", true);
            yield return new WaitUntil(() => attacker.GetAnimScript().Attacking);
           //Weapon attacks all enemies with single hit
            if (!(defender.GetEffectTimeArray()[(int)eEffects.Invulnerability] > 0))
            {
                if (!defender.m_surrender)
                    attacker.CounterTakeDamage(defender.TakeDamage(attacker.m_ActiveWeapon.GetAttack() + attacker.AdditionalDamage(),
                    attacker.GetStatistics(),
                (attacker.GetEffectTimeArray()[(int)eEffects.InteruptModifier] > 0) ? attacker.GetEffectArray()[(int)eEffects.InteruptModifier] : 1,
                    attacker.m_ActiveWeapon.m_strength));
                else
                    defender.Health = 0;
                defender.ReCheckHealth();
                attacker.ReCheckHealth();
                if (defender.Health <= 0)
                {
                    bool foundNextEnemy = false;
                    for (int t = 0; t < GetDefendingTeam().Length; t++)
                    {
                        if (GetDefendingTeam()[t].Health > 0 && !foundNextEnemy)
                        {
                            i--;
                            foundNextEnemy = true;
                            defender = GetDefendingTeam()[t];
                            attacker.m_ActiveWeapon.AddEffects(defender);
                        }
                    }
                }
            }
            yield return new WaitUntil(() => !attacker.GetAnimatorStateInfo().IsName(attacker.m_ActiveWeapon.GetAnimationToPlay().name));

        }
        m_attackDone = true;
    }

    IEnumerator Flee(CharacterStatSheet attacker, CharacterStatSheet defender)
    {
        attacker.m_animator.Play(attacker.m_ActiveWeapon.GetAnimationToPlay().name);
        if (attacker.m_ActiveWeapon.HasEffect)
        {
            attacker.m_ActiveWeapon.m_animEffect.gameObject.transform.position = attacker.transform.position;
            StartCoroutine(attacker.m_ActiveWeapon.PlayWeaponEffect(attacker));
            attacker.m_animator.SetBool("SpellBreak", false);
            if (attacker.m_ActiveWeapon.m_animEffect.HasAnimation)
                yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.FinishedAnimation);
            else
            {
                yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.m_partSys.isPlaying);
                yield return new WaitUntil(() => !attacker.m_ActiveWeapon.m_animEffect.m_partSys.isEmitting);
            }
            attacker.m_ActiveWeapon.m_animEffect.StopEffect();

        }
        attacker.m_animator.SetBool("SpellBreak", true);
        yield return new WaitUntil(() => attacker.GetAnimScript().Attacking);

        yield return new WaitUntil(() => !attacker.GetAnimatorStateInfo().IsName(attacker.m_ActiveWeapon.GetAnimationToPlay().name));

        BattleOver = true;
        DidFlee = true;
        WonBattleQ = true;
        m_attackDone = true;
    }

    IEnumerator DrainHealth(CharacterStatSheet attacker, CharacterStatSheet defender)
    {
        //Attack on person for single hit
        attacker.m_animator.Play(attacker.m_ActiveWeapon.GetAnimationToPlay().name);
        if (attacker.m_ActiveWeapon.HasEffect)
        {
            attacker.m_ActiveWeapon.m_animEffect.gameObject.transform.position = defender.transform.position;
            StartCoroutine(attacker.m_ActiveWeapon.PlayWeaponEffect(attacker));
            attacker.m_animator.SetBool("SpellBreak", false);
            if (attacker.m_ActiveWeapon.m_animEffect.HasAnimation)
                yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.FinishedAnimation);
            else
            {
                yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.m_partSys.isPlaying);
                yield return new WaitUntil(() => !attacker.m_ActiveWeapon.m_animEffect.m_partSys.isEmitting);
            }
            attacker.m_ActiveWeapon.m_animEffect.StopEffect();

        }
        attacker.m_animator.SetBool("SpellBreak", true);
        yield return new WaitUntil(() => attacker.GetAnimScript().Attacking);
        if (!(defender.GetEffectTimeArray()[(int)eEffects.Invulnerability] > 0))
        {
            defender.TakeDamage(attacker.m_ActiveWeapon.GetAttack() + ((int)attacker.m_attackStrength + attacker.AdditionalDamage()),
                attacker.GetStatistics(),
                ((attacker.GetEffectTimeArray()[(int)eEffects.InteruptModifier] > 0) ? attacker.GetEffectArray()[(int)eEffects.InteruptModifier] : 1),
                attacker.m_ActiveWeapon.m_strength);
            defender.ReCheckHealth();
            attacker.HealSelf(25.0f);
            attacker.ReCheckHealth();
        }
        yield return new WaitUntil(() => !attacker.GetAnimatorStateInfo().IsName(attacker.m_ActiveWeapon.GetAnimationToPlay().name));

        m_attackDone = true;
    }

    IEnumerator HealOne(CharacterStatSheet attacker)
    {
        attacker.m_animator.Play(attacker.m_ActiveWeapon.GetAnimationToPlay().name);
        if (attacker.m_ActiveWeapon.HasEffect)
        {
            attacker.m_ActiveWeapon.m_animEffect.gameObject.transform.position = attacker.transform.position;
            StartCoroutine(attacker.m_ActiveWeapon.PlayWeaponEffect(attacker));
            attacker.m_animator.SetBool("SpellBreak", false);
            if (attacker.m_ActiveWeapon.m_animEffect.HasAnimation)
                yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.FinishedAnimation);
            else
            {
                yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.m_partSys.isPlaying);
                yield return new WaitUntil(() => !attacker.m_ActiveWeapon.m_animEffect.m_partSys.isEmitting);
            }

            //attacker.m_ActiveWeapon.m_animEffect.StopEffect();

        }
        attacker.m_animator.SetBool("SpellBreak", true);
        yield return new WaitUntil(() => attacker.GetAnimScript().Attacking);
        attacker.HealSelf(attacker.m_ActiveWeapon.GetAttack());
        attacker.ReCheckHealth();
        yield return new WaitUntil(() => !attacker.GetAnimatorStateInfo().IsName(attacker.m_ActiveWeapon.GetAnimationToPlay().name));

        m_attackDone = true;
    }

    IEnumerator BuffDebuff(CharacterStatSheet attacker)
    {
        attacker.m_animator.Play(attacker.m_ActiveWeapon.GetAnimationToPlay().name);
        if (attacker.m_ActiveWeapon.HasEffect)
        {
            attacker.m_ActiveWeapon.m_animEffect.m_rootHolder.transform.position = attacker.transform.position;
            StartCoroutine(attacker.m_ActiveWeapon.PlayWeaponEffect(attacker));
            attacker.m_animator.SetBool("SpellBreak", false);
            if (attacker.m_ActiveWeapon.m_animEffect.HasAnimation)
                yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.FinishedAnimation);
            else
            {
                yield return new WaitUntil(() => attacker.m_ActiveWeapon.m_animEffect.m_partSys.isPlaying);
                yield return new WaitUntil(() => !attacker.m_ActiveWeapon.m_animEffect.m_partSys.isEmitting);
            }
            if(attacker.m_ActiveWeapon.m_animEffect.m_needsToBeStopped)
                attacker.m_ActiveWeapon.m_animEffect.StopEffect();

        }
        attacker.m_animator.SetBool("SpellBreak", true);
        yield return new WaitUntil(() => attacker.GetAnimScript().Attacking);
        yield return new WaitUntil(() => !attacker.GetAnimatorStateInfo().IsName(attacker.m_ActiveWeapon.GetAnimationToPlay().name));

        m_attackDone = true;
    }
}
