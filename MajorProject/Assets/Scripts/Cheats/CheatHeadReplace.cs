using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatHeadReplace : Cheat {

    [SerializeField]
    private GameObject m_geraltHead;

    [SerializeField]
    private GameObject m_lambertHead;


    public override void Execute()
    {
        m_geraltHead.SetActive(!m_geraltHead.activeInHierarchy);
        m_lambertHead.SetActive(!m_lambertHead.activeInHierarchy);
    }
}
