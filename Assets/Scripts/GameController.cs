using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    [HideInInspector]public int waveCount;
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int basicHazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text WaveText;
    public Text scoreText;
    //public Text restartText;
    public GameObject restartButton;
    public Text gameOverText;

    private bool gameOver;

    private int score;
    private int hazardCount;
    
    //每超过700分，basicHazardCount加1
    private int phaseScore=700;

    void Start()
    {
        waveCount = 0;
        restartButton.SetActive(false);
        gameOver = false;
        //restartText.text = "";
        gameOverText.text = "";
        score = 0;
        StartCoroutine(SpawnWaves());
        UpdateScore();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (gameOver == false)
        {
            waveCount++;
            UpdateWave();
            hazardCount = (int)Mathf.Log(waveCount, 2) + basicHazardCount;
            Debug.Log(hazardCount);
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
                if (gameOver)
                    yield break;
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        if(score>=phaseScore)
        {
            basicHazardCount++;
            phaseScore += 700;
        }
        UpdateScore();
    }

    public void UpdateWave()
    {
        WaveText.text = "Wave:" + waveCount;
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        if (restartButton != null)
            restartButton.SetActive(true);
        //restartText.text = "Press 'R' for Restart";
        gameOver = true;
    }

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
