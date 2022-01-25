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
    public float musicVolume = 1f;
    [Range(0, 1)]
    public float SFXVolume = 1f;
    public Sound[] OST;
    public Sound[] SFX;
    AudioSource _mainspeaker;


    [HorizontalLine(2, EColor.Gray)]
    [Header("GLOBAL INSTANCES")]
    public Transform Playercharacter;
    public Transform Camerapivot;
    public Transform Playergraphic;
    public bool inMenu = false;

    private void Start()
    {
        Camerapivot.parent = GameObject.Find("Cameras").transform;
        DontDestroyOnLoad(gameObject);
        _mainspeaker = GetComponent<AudioSource>();
        if(Application.isEditor == false)
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }








    public void PlayOST(string name)
    {
        Sound s = Array.Find(OST, Sound => Sound.name == name);
        _mainspeaker.clip = s.clip;
        _mainspeaker.loop = s.loop;
        _mainspeaker.pitch = s.pitch;
        _mainspeaker.volume = s.localVolume * musicVolume;
        _mainspeaker.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(SFX, Sound => Sound.name == name);
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;

        s.source.volume = s.localVolume * musicVolume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
    }

}


