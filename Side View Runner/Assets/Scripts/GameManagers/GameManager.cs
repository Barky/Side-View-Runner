using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public bool gameStartedfromMainMenu, gameStartedfromPlayerDied;

    [HideInInspector] public float health, score, level;

    [HideInInspector] public bool canPlayMusic = true;
    private void Awake()
    {
        MakeSingleton();
    }
    private void Update()
    {
        if (canPlayMusic)
        {
            Debug.Log("muzýk calýo");
        }
        else
        {
            Debug.Log("muzýk calmýoooo");
        }
    }
    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }




}
