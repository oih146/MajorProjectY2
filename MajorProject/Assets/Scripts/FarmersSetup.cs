using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmersSetup : MonoBehaviour {

    public GameObject m_postFarmers;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ActivatePostFarmers()
    {
        m_postFarmers.SetActive(true);
        FadeBlack.OnFadeBlackEnd -= ActivatePostFarmers;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            FadeBlack.OnFadeBlackEnd += ActivatePostFarmers;
        }
    }

    void StartFade()
    {
        FadeBlack.Instance.Activate(false);
    }
}
