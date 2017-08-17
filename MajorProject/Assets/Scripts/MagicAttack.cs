using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Aliginment
{
    Law,
    Light
}

public enum DoWithAlignNum
{
    GreaterThan,
    LessThan,
    EqualTo
}

public delegate bool DoWithFuncs(int Is, int To);
public class MagicAttack : WeaponBase {

    public string m_AttackName;
    public int m_CooldownTimeChecker;
    [HideInInspector]
    public int m_actualCooldown;
    public Aliginment m_magicAlignment;
    public DoWithAlignNum m_doWith;
    public DoWithFuncs[] dowithFunc =
    {
        GreaterThan,
        LessThan,
        EqualTo
    };
    [Range(0, 100)]
    public int alignmentRange;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static bool GreaterThan(int Is, int Than)
    {
        return Is > Than;
    }

    public static bool LessThan(int Is, int Than)
    {
        return Is < Than;
    }

    public static bool EqualTo(int Is, int To)
    {
        return Is == To;
    }
    public bool CanUseSpell(PlayerStat player)
    {
        DoWithFuncs funcToDo = dowithFunc[(int)m_doWith];
        if (m_magicAlignment == Aliginment.Law)
            return funcToDo(alignmentRange, (int)player.Law);
        else
            return funcToDo(alignmentRange, (int)player.Light);
    }
}
