using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
 
public class Lives : MonoBehaviour
{
    public Text livesDisplay;
    public GameObject gameOverScreen;
    public Text displayWaves;

    public static Lives instance;

    private PlayerStats playerStats;


    private void Start()
    {
        instance = this;
        playerStats = PlayerStats.instance;
        livesDisplay.text = playerStats.startingLives.ToString();
        playerStats.lives = playerStats.startingLives;
    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            gameOver();
        }
    }

    public void loseLife()
    {
        if (!(playerStats.lives > 0))
        {
            return;
        }
        playerStats.lives--;
        livesDisplay.text = playerStats.lives.ToString();
        if (playerStats.lives == 0)
        {
            gameOver();
        }
    }

    void gameOver()
    {
        gameOverScreen.SetActive(true);
        int waveNum = gameObject.GetComponent<WaveSpawner>().getWaveNum() - 1;
        if (waveNum < 0)
            waveNum = 0;
        displayWaves.text = waveNum.ToString();
        Debug.Log("Game Over!!!");
    }

    public void restart()
    {
        playerStats.restart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void menu()
    {
        SceneManager.LoadScene(0);
    }
}
