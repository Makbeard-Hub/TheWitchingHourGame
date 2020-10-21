using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameIgnition : MonoBehaviour
{
    public Button myBtn;

	void Start ()
    {
        Button btn = myBtn.GetComponent<Button>();
        btn.onClick.AddListener(StartGame);
	}
	
    void StartGame()
    {
        SceneManager.LoadScene("Main Scene");
    }
}