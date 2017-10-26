using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimScript : AnimScript {

    [SerializeField]
    private Transform m_castHandTransform;
    public Vector3 CastHandPosition { get { return m_castHandTransform.position; } }

    [SerializeField]
    private Transform m_swordBaseTransform;
    public Transform SwordBasePosition { get { return m_swordBaseTransform; } }


    public void SetToCastHandPosition(Vector3 position)
    {
        position = CastHandPosition;
    }

    public void SetToSwordPosition(Transform trans)
    {
        trans.parent = SwordBasePosition;
    }

}
