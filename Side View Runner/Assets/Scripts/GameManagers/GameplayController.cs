using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    private Text scoreText, healthText, levelText;

    private float score, health, level;
    private void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>(); 
        levelText = GameObject.Find("LevelText").GetComponent<Text>();

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
    }
    void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Game")
        {
            if (GameManager.instance.gameStartedfromMainMenu)
            {
                GameManager.instance.gameStartedfromMainMenu = false;
                health = 3f;
                score = 0f;
                level = 0f;
                Debug.Log("alýyo healthi aslýnda");
            }
            else if (GameManager.instance.gameStartedfromPlayerDied)
            {
                GameManager.instance.gameStartedfromPlayerDied = false;
                health = GameManager.instance.health;
                score = GameManager.instance.score;
                level = GameManager.instance.level;

            }
            Debug.Log("yazdýrýo");
            healthText.text = health.ToString();
            scoreText.text = score.ToString();
            levelText.text = level.ToString();
        }
    }
}
