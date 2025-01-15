using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour{
    public bool gameOver { get; private set; }

    private Text gameoverText;
    private Button restartButton;

    private Button resetButton;

    public GameObject[] enemyPrefabs;
    public GameObject floatingPrefab;
    private float spawnInterval1;
    private float spawnInterval2;
    private float initialSpawnInterval; 
    private float minSpawnInterval;
    
    public GameObject[] spawnPoints;
    public GameObject floatingSpawnPoint;
    

    private int score;
    private Text scoreText;

    private int highscore;
    private Text highscoreText;

    private AudioSource bgmAudio;
    private AudioSource gameOverAudio;

    void Start(){
        gameOver = false;
        gameoverText = GameObject.Find("gameoverText").GetComponent<Text>();
        gameoverText.gameObject.SetActive(false);

        restartButton = GameObject.Find("RestartButton").GetComponent<Button>(); 
        restartButton.onClick.AddListener(RestartGame); 
        restartButton.gameObject.SetActive(false); 

        score = 0;
        highscore = PlayerPrefs.GetInt("HighScore", 0);
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
        highscoreText = GameObject.Find("highscoreText").GetComponent<Text>();
        StartCoroutine(UpdateScore());

        resetButton = GameObject.Find("ResetButton").GetComponent<Button>();
        resetButton.onClick.AddListener(ResetScore);
        resetButton.gameObject.SetActive(false);

        StartCoroutine(EnemySpawn());
        StartCoroutine(FloatingEnemySpawn());
        spawnInterval1 = 3.0f;
        spawnInterval2 = 4.3f;
        initialSpawnInterval = spawnInterval1;
        minSpawnInterval = 1f;

        bgmAudio = GameObject.Find("BackGroundMusic").GetComponent<AudioSource>();
        gameOverAudio = GameObject.Find("GameOverSound").GetComponent<AudioSource>();
        bgmAudio.Play();
        gameOverAudio.Pause();
    }

    IEnumerator EnemySpawn(){ 
        while (!gameOver){
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject selectedEnemyPrefab = enemyPrefabs[randomEnemyIndex];

            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject selectedSpawnPointObject = spawnPoints[randomSpawnPointIndex];
            Transform selectedSpawnPoint = selectedSpawnPointObject.transform;

            if (selectedSpawnPoint != null){
                GameObject newEnemy = Instantiate(selectedEnemyPrefab, selectedSpawnPoint.position, Quaternion.identity);
            }

            spawnInterval1 = Mathf.Max(minSpawnInterval, spawnInterval1 - 0.15f);
            Debug.Log("spawnInterval1 : " + spawnInterval1);

            yield return new WaitForSeconds(spawnInterval1);
        }
    }

    IEnumerator FloatingEnemySpawn(){ 
        while (!gameOver){

            if (floatingSpawnPoint != null){
                GameObject newfloatingEnemy = Instantiate(floatingPrefab, floatingSpawnPoint.transform.position, Quaternion.identity);
            }

            spawnInterval2 = Mathf.Max(minSpawnInterval, spawnInterval2 - 0.05f);
            Debug.Log("spawnInterval2 : " + spawnInterval2);

        
            yield return new WaitForSeconds(spawnInterval2);
        }
    }


    public void RestartGame(){
        Time.timeScale = 1f; 
        gameOver = false;
        score = 0;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        BackGroundScrolling bg = FindObjectOfType<BackGroundScrolling>();
        bg.ResetSpeed(0.25f);
    }


    public void SetGameOver(bool isGameOver){
        gameOver = isGameOver;
        if (gameOver){
            gameoverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            resetButton.gameObject.SetActive(true);

            //Debug.Log("SetGameOver is called");

            bgmAudio.Pause();
            gameOverAudio.Play();
            Time.timeScale = 0f; 
            
        }
    }


    IEnumerator UpdateScore(){
        while(!gameOver){
            score++;
            UpdateScoreText();

            if(score >= highscore){
                highscore = score+1;
                PlayerPrefs.SetInt("HighScore", highscore);
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    void UpdateScoreText(){
        scoreText.text = "SCORE: " + score.ToString();
        highscoreText.text = "HIGH SCORE: " + highscore.ToString();
    }

    public void ResetScore(){
        score = 0;
        highscore = 0;
        PlayerPrefs.SetInt("HighScore", highscore);
        PlayerPrefs.Save();
        scoreText.text = "SCORE: " + score.ToString();
        highscoreText.text = "HIGH SCORE: " + highscore.ToString();
    }
}

