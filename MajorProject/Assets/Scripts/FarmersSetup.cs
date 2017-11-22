using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmersSetup : MonoBehaviour {

    [SerializeField]
    private GameObject[] m_stageOn;

    [SerializeField]
    private GameObject[] m_stageTwo;

    [SerializeField]
    private GameObject[] m_stageThree;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ActivateStageTwo()
    {
        foreach (GameObject game in m_stageOn)
            game.SetActive(false);
        foreach (GameObject game in m_stageTwo)
            game.SetActive(true);
        foreach (GameObject game in m_stageThree)
            game.SetActive(false);
        FadeBlack.OnFadeBlackMidle -= ActivateStageTwo;
    }

    void ActivateStageThree()
    {
        foreach (GameObject game in m_stageOn)
            game.SetActive(false);
        foreach (GameObject game in m_stageTwo)
            game.SetActive(false);
        foreach (GameObject game in m_stageThree)
            game.SetActive(true);
        FadeBlack.OnFadeBlackMidle -= ActivateStageThree;
    }

    void StartStageTwoFade()
    {
        FadeBlack.OnFadeBlackMidle += ActivateStageTwo;
        FadeBlack.Instance.Activate(false);
    }

    void StartStageThreeFade()
    {
        FadeBlack.OnFadeBlackMidle += ActivateStageThree;
        FadeBlack.Instance.Activate(false);
    }
}
