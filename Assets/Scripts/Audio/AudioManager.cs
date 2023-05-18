using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Clips")]
    public AudioClip bgmClip;
    public AudioClip jumpClip;
    public AudioClip longJumpClip;
    public AudioClip deadClip;

    [Header("Audio Source")]
    public AudioSource bgmSource;
    public AudioSource fx;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        } else
        {
            Destroy(instance.gameObject);
        }

        DontDestroyOnLoad(this);
        bgmSource.clip = bgmClip;
        PlayMusic();


    }

    private void OnEnable()
    {
        EventHandler.GameOverEvent += OnGameOverEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameOverEvent -= OnGameOverEvent;
    }

    private void OnGameOverEvent()
    {
        fx.clip = deadClip;
        fx.Play();
    }

    public void SetJumpClip(bool isLongJump)
    {
        if (!isLongJump)
        {
            fx.clip = jumpClip;
        } else
        {
            fx.clip = longJumpClip;
        }
    }

    public void PlayJumpFx()
    {
        fx.Play();
    }

    public void PlayMusic()
    {
        if (!bgmSource.isPlaying)
        {
            bgmSource.Play();
        }
    }
}
