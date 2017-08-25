using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;

public class DialogueTest : MonoBehaviour {

    public PlayerStat player;
    public static int childnum = 0;
    public GameObject responseContent;
    public static bool readyToRecount;
    private GameObject[] childArray;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void RecountChildren()
    {
        //PixelCrushers.DialogueSystem.Response[] responses = PixelCrushers.DialogueSystem.DialogueManager.CurrentConversationState.pcResponses;
        //responses[1
        //yield return new WaitUntil(() => readyToRecount);
        childnum = 0;
        //childArray = new GameObject[responseContent.transform.childCount];
        //for (int i = 1; i < responseContent.transform.childCount; i++)
        //{
        //    childArray[i] = responseContent.transform.GetChild(i).gameObject;
        //}
    }

    public void LawGreaterThan(string amount)
    {
        if(player.Law <= int.Parse(amount))
        {
            //responseContent.transform.GetChild(childnum).GetComponent<UnityEngine.UI.Button>().interactable = false;
            responseContent.transform.GetChild(childnum).gameObject.SetActive(true);

            childnum++;
        }
    }
}
