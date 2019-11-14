using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // so u can recall/restart the scense

public class gameController : MonoBehaviour
{
    public GameObject[] hazards;
    //public GameObject hazard1, hazard2, hazard3;
    //public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text winText;
    private int score;

    private bool gameOver;
    private bool restart;

    //======Start===========
    private void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (spawnWave()); //<-- have to use starcoroutine (function()); to call a function.. this is getting more confusing
    }

    //===========Updates============
    private void Update()
    {
        if (restart)
        {
            //if (Input.anyKey) i misread it into ANY key that was pressed down
            if (Input.GetKeyDown (KeyCode.F))
            {
                SceneManager.LoadScene("Challenge3"); //the scense name, restarting the scense after press R
            }
        }
    }
    private void LateUpdate()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    //=====Functions=====
    IEnumerator spawnWave() // <--- cant be void when using waitforsecond v
    {
        yield return new WaitForSeconds(startWait);

        while (true) //so player dont run out of asterod to shot/dodge
        {
            for (int i = 0; i < hazardCount; i++) //make it loops to spawn asteroid 
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)]; //making it pick one random asteroid model from the list to spawn
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z); //having x as random.range so it doesnt spawn in straight line
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);


            if (gameOver) //check to see if gameover is true to get out of loop
            {
                restartText.text = "Press the 'F' Key to Restart";
                restart = true;
                break; // break out of while loop
            }
        }
        
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    } 

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if (score >= 100)
        {
            winText.text = "You win!\nCreated by Hanniee";
            gameOver = true;
            restart = true;
        }
    }
    public void GameOver()
    {
        winText.text = "Game Over :P!";
        gameOver = true;
    }
}
