using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimScript : AnimScript {

    [SerializeField]
    private Transform m_castHandTransform;

    public Vector3 CastHandPosition { get { return m_castHandTransform.position; } }

    public void SetToCastHandPosition(Vector3 position)
    {
        position = CastHandPosition;
    }

}
