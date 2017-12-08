using UnityEngine;

public class CheatPlayer : Cheat {

    enum ECheat
    {
        FullLaw,
        NoLaw,
        FullLight,
        NoLight,
        FullHeath,
        FullMana,
        None
    }

    private PlayerStat m_player;
    private ECheat m_eCheat = ECheat.None;

    protected override void Awakes()
    {
        m_player = FindObjectOfType<PlayerStat>();
        if (CheatCodes.Length != (int)ECheat.None)
        {
            Debug.Log("Error! CheatLawLight doesn't contain " + (int)ECheat.None + " cheat codes");
        }
    }

    public override void Execute()
    {
        switch (m_eCheat)
        {
            case ECheat.FullLaw:
                m_player.Law = 100;
                break;
            case ECheat.NoLaw:
                m_player.Law = 0;
                break;
            case ECheat.FullLight:
                m_player.Light = 100;
                break;
            case ECheat.NoLight:
                m_player.Light = 0;
                break;
            case ECheat.FullHeath:
                m_player.Health = 100;
                break;
            case ECheat.FullMana:
                m_player.SpellAvaliable = 6;
                break;
            case ECheat.None:
                break;
            default:
                break;
        }

    }

    public override void SetCheatCode(string acceptedCode)
    {
        if(acceptedCode == CheatCodes[0])
        {
            m_eCheat = ECheat.FullLaw;
        } else if (acceptedCode == CheatCodes[1])
        {
            m_eCheat = ECheat.NoLaw;
        } else if (acceptedCode == CheatCodes[2])
        {
            m_eCheat = ECheat.FullLight;
        } else if (acceptedCode == CheatCodes[3])
        {
            m_eCheat = ECheat.NoLight;
        } else if (acceptedCode == CheatCodes[4])
        {
            m_eCheat = ECheat.FullHeath;
        } else if (acceptedCode == CheatCodes[5])
        {
            m_eCheat = ECheat.FullMana;
        } else
        {
            m_eCheat = ECheat.None;
            Debug.Log("Error! Cheat Accepted Yet Not Found!");
        }
    }

}
