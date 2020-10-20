using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public string sceneToLoad;

    void Start() {
        Time.timeScale = 1;
    }

    public void LoadMyLevel(string level) {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void CloseProgram() {
        Application.Quit();
    }
}
