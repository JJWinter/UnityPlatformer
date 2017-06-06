using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    GameObject mainMenu;
    GameObject levelSelect;
    GameObject controlList;

    // Use this for initialization
    void Start () {
        mainMenu = GameObject.Find("Main");
        levelSelect = GameObject.Find("Level Select");
        controlList = GameObject.Find("Control List");

        levelSelect.SetActive(false);
        controlList.SetActive(false);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1Scene");
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2Scene");
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3Scene");
    }

    public void LevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void ControlList()
    {
        mainMenu.SetActive(false);
        controlList.SetActive(true);
    }


    public void BackButton()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
        controlList.SetActive(false);
    }
}
