using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmersSetup : MonoBehaviour {

    [SerializeField]
    private GameObject[] m_turnOffs;

    [SerializeField]
    private GameObject[] m_turnOns;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ActivatePostFarmers()
    {
        foreach (GameObject game in m_turnOffs)
            game.SetActive(false);
        foreach (GameObject game in m_turnOns)
            game.SetActive(true);
        FadeBlack.OnFadeBlackMidle -= ActivatePostFarmers;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            FadeBlack.OnFadeBlackMidle += ActivatePostFarmers;
        }
    }

    void StartFade()
    {
        FadeBlack.Instance.Activate(false);
    }
}
