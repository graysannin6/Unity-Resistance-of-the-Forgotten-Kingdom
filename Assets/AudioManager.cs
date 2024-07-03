using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   

    [SerializeField] AudioSource bossOneMusic; // Make sure to assign these in the Unity Editor
    [SerializeField] AudioSource bossTwoMusic;
    [SerializeField] AudioSource mainMusic;
    public static AudioManager instance;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        PlayMainMusic();
        StopBossOneMusic();
        StopBossTwoMusic();
    }

    public void PlayMainMusic()
    {
        if (mainMusic != null)
        {
            mainMusic.loop = true;
            mainMusic.Play();
        }
    }

    // Method to play boss one music in a loop
    public void PlayBossOneMusic()
    {
        if (bossOneMusic != null)
        {
            bossOneMusic.loop = true;
            bossOneMusic.Play();
        }
    }

    // Method to play boss two music in a loop
    public void PlayBossTwoMusic()
    {
        if (bossTwoMusic != null)
        {
            bossTwoMusic.loop = true;
            bossTwoMusic.Play();
        }
    }

    // Method to stop main music
    public void StopMainMusic()
    {
        if (mainMusic != null && mainMusic.isPlaying)
        {
            mainMusic.Stop();
        }
    }

    // Method to stop boss one music
    public void StopBossOneMusic()
    {
        if (bossOneMusic != null && bossOneMusic.isPlaying)
        {
            bossOneMusic.Stop();
        }
    }

    // Method to stop boss two music
    public void StopBossTwoMusic()
    {
        if (bossTwoMusic != null && bossTwoMusic.isPlaying)
        {
            bossTwoMusic.Stop();
        }
    }
}
