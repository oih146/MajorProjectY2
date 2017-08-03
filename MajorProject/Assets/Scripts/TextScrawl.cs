using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScrawl : MonoBehaviour {

    public PlayerMovement player;
    public UnityEngine.UI.Scrollbar verticalScroller;
    public UnityEngine.UI.Image blackScreen;
    public bool fadeIn = false;
	// Use this for initialization
	void Start () {
        verticalScroller.value = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (verticalScroller.value > 0)
        {
            verticalScroller.value -= 0.001f;
        }
        else
        {
            verticalScroller.transform.parent.gameObject.SetActive(false);
            fadeIn = true;
        }
        if (fadeIn && blackScreen.color.a > 0.001f)
        {
            Color buffCol = blackScreen.color;
            buffCol.a = Mathf.Lerp(blackScreen.color.a, 0, 5 * Time.deltaTime);
            blackScreen.color = buffCol;
        }
        else if (blackScreen.color.a <= 0.001f)
        {
            player.SetMovement(true);
            gameObject.SetActive(false);
        }
    }
}
