using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    [HideInInspector] public AudioSource audios;

    private void Awake()
    {
        audios = GetComponent<AudioSource>();
    }
    private void Start()
    {
       // DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        Debug.Log(GameManager.instance.canPlayMusic);
            if (GameManager.instance.canPlayMusic)
            {
                audios.Play();
            }
            else if (!GameManager.instance.canPlayMusic)
            {
                audios.Stop();
            }
        

    }
}
