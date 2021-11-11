using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private Button musicButton;
    [SerializeField] private Sprite soundOn, soundOff;

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
}
