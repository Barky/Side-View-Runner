using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    private Text scoreText, healthText, levelText;

    private float score, health, level;

    public static GameplayController instance;

    [SerializeField] private AudioSource audios;

    [SerializeField] private GameObject player;

    [HideInInspector] public bool canCountScore;

    [SerializeField] private float highscore;

    private GameObject pausePanel;

    public GameObject deathParticle;
    private BGScroller bgscroller;

    private void Start()
    {
        if (GameManager.instance.canPlayMusic)
        {
            audios.Play();
        }

        
    }
    private void Awake()
    {
        MakeInstance();
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        //player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
        bgscroller = GameObject.Find(Tags.BACKGROUND_OBJ).GetComponent<BGScroller>();

        pausePanel = GameObject.Find("PausePanel");

        pausePanel.SetActive(false);

        highscore = PlayerPrefs.GetFloat("highScoreKey", 0f);
        levelText.text = highscore.ToString();
    }
    private void Update()
    {
        IncrementScore(1);

        if (score > highscore)
        {
            highscore = score;
            levelText.text = highscore.ToString();
            PlayerPrefs.SetFloat("highScoreKey", highscore);
            PlayerPrefs.Save();
        }
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
        instance = null;
    }
    void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Game")
        {
            if (GameManager.instance.gameStartedfromMainMenu)
            {
                GameManager.instance.gameStartedfromMainMenu = false;
                health = 3;
                score = 0;
                level = 0;
                Debug.Log("gamestartedmainmenu çalýþtý");
            }
            else if (GameManager.instance.gameStartedfromPlayerDied)
            {
                GameManager.instance.gameStartedfromPlayerDied = false;
                health = GameManager.instance.health;
                score = GameManager.instance.score;
               // level = GameManager.instance.level;

            }

            Debug.Log(health);
            
            scoreText.text = score.ToString();
            healthText.text = health.ToString();
            levelText.text = level.ToString();
        }
    }

    public void TakeDamage()
    {
        health--;
        
        if (health>=0)
        {
            healthText.text = health.ToString();
        }
        else
        {
            Debug.Log("öldün");
            StartCoroutine(PlayerDied("MainMenu"));
        }
    }
    public void IncrementHealth()
    {
        health++;
        healthText.text = health.ToString();
        Debug.Log("incrementhealthçalýþtý");
    }

    public void IncrementScore(int scoreValue)
    {
        if (canCountScore)
        {
            score += scoreValue;
            scoreText.text = score.ToString();
        }
    }
    public void PauseGame()
    {
        canCountScore = false;
        bgscroller.canScroll = false;
        pausePanel.SetActive(true);
        Time.timeScale = 0f; //pausing the game
    }

    public void ResumeGame()
    {
        canCountScore = true;
        bgscroller.canScroll = true;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ReplayGame()
    {
        GameManager.instance.gameStartedfromMainMenu = true;
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
        
    }
    IEnumerator PlayerDied(string scenename)
    {
        canCountScore = false;
        GameManager.instance.score = score;
        GameManager.instance.health = health;
        GameManager.instance.gameStartedfromPlayerDied = true;
        bgscroller.canScroll = false;
        Vector3 fxposition = transform.position;
        fxposition.y += 2f;
        Instantiate(deathParticle, fxposition, Quaternion.identity);
        Debug.Log("numeratör çalýþýo");
        //Time.timeScale = 0f;
        Destroy(player);
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(scenename);
    }
}
