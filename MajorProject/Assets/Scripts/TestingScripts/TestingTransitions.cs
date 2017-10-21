using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingTransitions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            FadeBlack.OnFadeBlackMidle += SpawnObject;
            FadeBlack.OnFadeBlackMidle += RestartFade;
            FadeBlack.Instance.Activate(false);
        }
    }

    void SpawnObject()
    {
        GameObject newgame = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newgame.transform.parent = transform;
        newgame.transform.localPosition = Vector3.zero;
        FadeBlack.OnFadeBlackMidle -= SpawnObject;
    }

    void RestartFade()
    {

        StartCoroutine(WaitTillActivate());
        FadeBlack.OnFadeBlackMidle -= RestartFade;
    }

    IEnumerator WaitTillActivate()
    {
        yield return new WaitForSeconds(3f);
        FadeBlack.Instance.Activate(true);
    }
}
