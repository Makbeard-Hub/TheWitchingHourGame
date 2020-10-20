using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
    public GameObject mainCanvas;
    public GameObject pauseCanvas;

    private bool gameIsPaused;


	// Use this for initialization
	void Start () {
        gameIsPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel")){
            if (!gameIsPaused)
            {
                GamePause();
                gameIsPaused = true;
            }

            else if (gameIsPaused) {
                GameContinue();
                gameIsPaused = false;
            }
            
            
            
            
        }	
	}

    public void GamePause() {
        Time.timeScale = 0;
        OpenMenu();
    }

    public void GameContinue()
    {
        Time.timeScale = 1;
        CloseMenu();
    }

    void OpenMenu() {
        mainCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    void CloseMenu() {
        mainCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }
}
