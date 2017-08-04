using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public GameObject[] menus;
    public Buttons[] menuButtons;
    public Buttons[] optionButtons;
    public Buttons[] creditButtons;
    int curMenu = 0;


	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        switch (curMenu)
        {
            case 0:
                if (menuButtons[0].ReturnClick()) { SceneManager.LoadScene(1); }
                if (menuButtons[1].ReturnClick()) { GoToOptions(); }
                if (menuButtons[2].ReturnClick()) { GoToCredits(); }
                if (menuButtons[3].ReturnClick())
                {
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #else
                    Application.Quit();
                    #endif
                }
                break;
            case 1:
                if (optionButtons[0].ReturnClick()) { GoToMain(); }
                break;
            case 2:
                if (creditButtons[0].ReturnClick()) { GoToMain(); }
                break;
        }
        Debug.Log(optionButtons[0].ReturnClick());
    }

    void GoToMain()
    {
        for (int i = 0; i < menus.Length; i++) { menus[i].SetActive(false); }
        curMenu = 0;
        menus[curMenu].SetActive(true);
    }

    void GoToOptions() {
        for(int i = 0; i < menus.Length; i++) { menus[i].SetActive(false); }
        curMenu = 1;
        menus[curMenu].SetActive(true);
    }

    void GoToCredits() {
        for (int i = 0; i < menus.Length; i++) { menus[i].SetActive(false); }
        curMenu = 2;
        menus[curMenu].SetActive(true);
    }
}
