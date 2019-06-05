using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    [SerializeField] AudioClip bossClip;
    AudioSource audioPlayer;
    AudioClip standartClip;

	void Awake ()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        audioPlayer.Play();
        standartClip = GetComponent<AudioSource>().clip;
    }

    public void TurnOnBossTheme ()
    {
        audioPlayer.clip = bossClip;
        audioPlayer.Play();
    }

    public void TurnOffBossTheme()
    {
        audioPlayer.clip = standartClip;
    }
}
