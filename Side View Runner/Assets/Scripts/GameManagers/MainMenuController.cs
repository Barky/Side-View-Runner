using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private Button musicButton;
    [SerializeField] private Sprite soundOn, soundOff;
    public GameObject copyrightpanel;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    public void playGame()
    {
        GameManager.instance.gameStartedfromMainMenu = true;
        SceneManager.LoadScene("Game");
    }
    public void controlMusic()
    {
        if (GameManager.instance.canPlayMusic)
        {
            musicButton.image.sprite = soundOn;
            GameManager.instance.canPlayMusic = false;
        }
        else
        {
            musicButton.image.sprite = soundOff;
            GameManager.instance.canPlayMusic = true;
        }
    }

    public void CopyrightPanel()
    {
        player.SetActive(false);
        copyrightpanel.SetActive(true);
    }

    public void CloseCopyrightPanel()
    {
        copyrightpanel.SetActive(false);
        player.SetActive(true);
    }
}
