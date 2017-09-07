using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseMaskDecider : MonoBehaviour {

    public static int m_idCaller;
    public int personalId;

    void OnEnable()
    {
        personalId = m_idCaller;
        UnityEngine.UI.Image sprite = gameObject.GetComponent<UnityEngine.UI.Image>();
        if (gameObject.GetComponentInParent<UnityEngine.UI.Button>().interactable == false)
        {

            m_idCaller++;
            sprite.sprite = MaskHolder.GetMask(personalId);
            sprite.color = new Color(1, 1, 1, 0.5f);
        }
        else
            sprite.color = new Color(1, 1, 1, 0);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
