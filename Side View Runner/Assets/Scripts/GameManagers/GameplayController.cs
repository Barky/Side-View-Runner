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

    //[SerializeField] private AudioSource audios;

    [SerializeField] private GameObject player;

    [HideInInspector] public bool canCountScore;

    [SerializeField] private float highscore;

    private GameObject pausePanel;

    public GameObject deathParticle;
    private BGScroller bgscroller;

    private void Start()
    {
        //if (GameManager.instance.canPlayMusic)
        //{
        //    audios.Play();
        //}
        //else if (!GameManager.instance.canPlayMusic)
        //{
        //    audios.Stop();
        //}

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

        level = PlayerPrefs.GetFloat("highScoreKey");
        levelText.text = level.ToString();
        StartCoroutine(positioncheck());
    }


    IEnumerator positioncheck()
    {
        yield return new WaitForSeconds(4f);
        Debug.Log("gameplaycontroller scriptinden: " + player.transform.position.x);
    }
    private void Update()
    {
        IncrementScore(1);

        if (score > level)
        {
            level = score;
            levelText.text = level.ToString();
            PlayerPrefs.SetFloat("highScoreKey", level);
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
                level = PlayerPrefs.GetFloat("highScoreKey");
            }
            else if (GameManager.instance.gameStartedfromPlayerDied)
            {
                GameManager.instance.gameStartedfromPlayerDied = false;
                health = GameManager.instance.health;
                score = GameManager.instance.score;
               level = GameManager.instance.level;

            }


            
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
            StartCoroutine(PlayerDied("MainMenu"));
        }
    }
    public void IncrementHealth()
    {
        health++;
        healthText.text = health.ToString();
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

        PlayerPrefs.SetFloat("highScoreKey", level);
        PlayerPrefs.Save();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ReplayGame()
    {

        PlayerPrefs.SetFloat("highScoreKey", level);
        PlayerPrefs.Save();
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
        //Time.timeScale = 0f;
        Destroy(player);

        PlayerPrefs.SetFloat("highScoreKey", level);
        PlayerPrefs.Save();
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(scenename);
    }
}
