using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerBounce : MonoBehaviour {

    public float m_bounceSpeed;
    public float m_bouncePullBack;
    float m_nextYPos;
    float initYPos;
    bool goUp;

	// Use this for initialization
	void Start () {
        m_nextYPos = gameObject.transform.localPosition.y + m_bouncePullBack;
        initYPos = gameObject.transform.localPosition.y; 
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.localPosition.y < ((goUp) ? m_nextYPos : initYPos) - 0.001f || gameObject.transform.localPosition.y > ((goUp) ? m_nextYPos : initYPos) + 0.001f)
        {
            Vector3 buff = gameObject.transform.localPosition;
            buff.y = Mathf.Lerp(gameObject.transform.localPosition.y, (goUp) ? m_nextYPos : initYPos, m_bounceSpeed * Time.deltaTime);
            gameObject.transform.localPosition = buff;
        }
        else
            goUp = !goUp;
	}

    public void FindNextPos(Vector3 position)
    {
        gameObject.transform.position = position;
        m_nextYPos = gameObject.transform.localPosition.y + m_bouncePullBack;
        initYPos = gameObject.transform.localPosition.y;
    }
}
