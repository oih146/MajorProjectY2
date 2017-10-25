using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivineFlameAttack : MagicAttack {

    public AnimationEffectScript m_goodFlame;
    public AnimationEffectScript m_badFlame;

    public int m_SwitchThreshold;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnSelect(CharacterStatSheet character)
    {
        PlayerStat player = (PlayerStat)character;
        if(player.Law >= m_SwitchThreshold)
        {
            m_animEffect = m_goodFlame;
        }
        else
        {
            m_animEffect = m_badFlame;
        }

        m_animEffect.OnSelect(character);
    }
}
