using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public GameObject Wave1;
    public GameObject Wave2;
    public GameObject Wave3;
    public GameObject FinalWave;
    public GameObject bigFire;
    public GameObject Torch1;
    public GameObject Torch2;
    public GameObject Torch3;
    public GameObject Torch4;
    public GameObject barrier;
    public GameObject player;
    public Transform rewardSpawnLoc;
    public GameObject rewardPotion;
    public Text counter;
    public GameObject bossUI;
    public Transform respawnLocation;
    public GameObject mainCanvas;
    public GameObject victoryCanvas;

    public int gameLevel;
    public int enemyCount;
    private GameObject[] enemyList;
    private bool gameStarted;
    private bool levelBeat;
    private bool gameBeat;

    void Start ()
    {
        gameLevel = 0;
        enemyList = new GameObject[1];
        enemyCount = 0;
        gameStarted = false;
        levelBeat = false;
        gameBeat = false;
	}
	
	void Update ()
    {
        if (!gameStarted)
        {
            bigFire.SetActive(true);
            barrier.SetActive(false);
        }
        if (gameLevel > 0 && gameLevel <= 3)
        {
            counter.text = ("Targets Remaining: " + enemyCount);
            if (enemyCount == 0 && gameStarted)
            {
                levelBeat = true;
            }
            if (levelBeat && !bigFire.activeInHierarchy)
            {
                player.transform.position = respawnLocation.position;
                Instantiate(rewardPotion, rewardSpawnLoc.position, rewardSpawnLoc.rotation);
                bigFire.SetActive(true);
                barrier.SetActive(false);
            }
        }
        //final level
        if (gameLevel == 4)
        {
            counter.text = ("Targets Remaining: " + enemyCount);
            if (enemyCount == 0 && !bossUI.activeInHierarchy) {
                Victory();
            } 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!gameStarted)
            {
                gameLevel = 1;
                gameStarted = true;
                ManageWaves(gameLevel);
                bigFire.SetActive(false);
                barrier.SetActive(true);
            }
            else if (enemyCount == 0 && gameStarted && levelBeat)
            {
                gameLevel++;
                ManageWaves(gameLevel);
                levelBeat = false;
                bigFire.SetActive(false);
                barrier.SetActive(true);
            }
            else if (gameStarted && !levelBeat)
            {
                ManageWaves(gameLevel);
                bigFire.SetActive(false);
                barrier.SetActive(true);
            }
        }
    }

    void ManageWaves(int gamelvl)
    {
        Array.Clear(enemyList, 0, enemyList.Length);
        enemyCount = 0;

        switch (gamelvl)
        {
            case 1:
                Instantiate(Wave1);
                Torch1.SetActive(true);
                break;
            case 2:
                Instantiate(Wave2);
                Torch2.SetActive(true);
                break;
            case 3:
                Instantiate(Wave3);
                Torch3.SetActive(true);
                break;
            case 4:
                bossUI.SetActive(true);
                Instantiate(FinalWave);
                Torch4.SetActive(true);
                break;
            default:
                Debug.Log("game doesnt exist");
                break;
        }

        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemyList.Length;
    }

    void Victory()
    {
        mainCanvas.SetActive(false);
        victoryCanvas.SetActive(true);
        SceneManager.LoadScene("EndGame");
    }
}