using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DialogueManager : MonoBehaviour {

    private Text textComponent;

    public string[] dialogueStrings;

    public float secondsBetweenCharacters = 0.15f;
    public float characterRateMultiplier = 0.5f;

    public KeyCode DialogueInput = KeyCode.Space;

    private bool isStringBeingRevealed = false;

	// Use this for initialization
	void Start () {
        textComponent = GetComponent<Text>();
        textComponent.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isStringBeingRevealed)
            {
                isStringBeingRevealed = true;
                StartCoroutine(DisplayString(dialogueStrings[0]));
            }
        }
	}

    private IEnumerator DisplayString(string stringToDisplay)
    {
        int stringLength = stringToDisplay.Length;
        int currentCharacterIndex = 0;

        textComponent.text = "";

        while(currentCharacterIndex < stringLength)
        {
            textComponent.text += stringToDisplay[currentCharacterIndex];
            currentCharacterIndex++;

            if(currentCharacterIndex < stringLength)
            {
                if (Input.GetKey(DialogueInput))
                {
                    yield return new WaitForSeconds(secondsBetweenCharacters * characterRateMultiplier);
                }
                else
                {
                    yield return new WaitForSeconds(secondsBetweenCharacters);
                }
            }
            else
            {
                break;
            }
        }

        while (true)
        {
            if (Input.GetKeyDown(DialogueInput))
            {
                break;
            }

            yield return 0;
        }

        isStringBeingRevealed = false;
        textComponent.text = "";
    }
}
