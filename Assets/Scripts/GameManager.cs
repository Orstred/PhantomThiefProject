using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine.Audio;
using System;
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0,1)]
    public float localVolume = 1;
    [Range(0.1f,3)]
    public float pitch = 1;
    public bool loop;
   [HideInInspector]
    public AudioSource source;
}

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        //singleton setup
        instance = this;
    }
    #endregion



    [HorizontalLine(2, EColor.Gray)]
    [Header("INTERACTION MANAGER OPTIONS")]
    public GameObject CurrentInteraction;
     




    [HorizontalLine(2, EColor.Gray)]
    [Header("AUDIO OPTIONS")]
    [Range(0,1)]
    public float MusicVolume = 1f;
    [Range(0, 1)]
    public float SFXVolume = 1f;
    public Sound[] OST;
    public Sound[] SFX;
    AudioSource MainSpeaker;


    [HorizontalLine(2, EColor.Gray)]
    [Header("GLOBAL INSTANCES")]
    public Transform playercharacter;
    public Transform CameraPivot;
    public Transform PlayerGraphic;


    private void Start()
    {
        CameraPivot.parent = GameObject.Find("Cameras").transform;
        DontDestroyOnLoad(gameObject);
        MainSpeaker = GetComponent<AudioSource>();
        if(Application.isEditor == false)
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }








    public void PlayOST(string name)
    {
        Sound s = Array.Find(OST, Sound => Sound.name == name);
        MainSpeaker.clip = s.clip;
        MainSpeaker.loop = s.loop;
        MainSpeaker.pitch = s.pitch;
        MainSpeaker.volume = s.localVolume * MusicVolume;
        MainSpeaker.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(SFX, Sound => Sound.name == name);
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;

        s.source.volume = s.localVolume * MusicVolume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
    }

}


