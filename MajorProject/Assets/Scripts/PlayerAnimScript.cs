using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimScript : AnimScript {

    [SerializeField]
    private Transform m_castHandTransform;
    public Transform CastHandPosition { get { return m_castHandTransform; } }

    [SerializeField]
    private Transform m_swordBaseTransform;
    public Transform SwordBasePosition { get { return m_swordBaseTransform; } }


    public void SetToCastHandPosition(Vector3 position)
    {
        position = CastHandPosition.position;
    }

    public void SetToSwordPosition(Transform trans)
    {
        trans.parent = SwordBasePosition;
    }

}
