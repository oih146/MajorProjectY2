using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseMaskDecider : MonoBehaviour {

    public static int m_idCaller;
    public int personalId;

    void Awake()
    {
        personalId = m_idCaller;
        m_idCaller++;
        if(gameObject.GetComponent<UnityEngine.UI.Button>().interactable == false)
        {
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = MaskHolder.GetMask(personalId);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
