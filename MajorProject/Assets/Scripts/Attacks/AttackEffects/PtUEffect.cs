using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PtUEffect : AnimationEffectScript {

    Transform m_initParent;

	// Use this for initialization
	void Start () {
        m_initParent = m_rootHolder.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void StopEffect()
    {
        m_rootHolder.SetActive(true);
        StartCoroutine(WaitTillDeath());
    }

    IEnumerator WaitTillDeath()
    {
        var variable = m_partSys.emission;
        variable.enabled = false;
        yield return null;
        m_partSys.Stop(true);
        variable.enabled = true;
        m_rootHolder.SetActive(false);
        m_rootHolder.transform.parent = m_initParent;
        FinishedAnimation = true;
    }

    public override void OnUse(CharacterStatSheet character)
    {
        base.OnUse(character);

        PlayerAnimScript anim = (PlayerAnimScript)character.GetAnimScript();

        m_rootHolder.transform.parent = anim.SwordBasePosition;
        m_rootHolder.transform.localPosition = new Vector3(0, 0, 0);
        Debug.Log(m_rootHolder.transform.localPosition);
        //m_rootHolder.transform.localEulerAngles = Vector3.zero;
    }
}
