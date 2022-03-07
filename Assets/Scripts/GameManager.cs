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
    [Range(0, 100)]
    public float Propagation = 0;
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
    [Header("GAME STATE OPTIONS")]
    [Scene]
    public string Victoryscreen;
    [Scene]
    public string LooseScreen;
    public int Cash;

    [HorizontalLine(2, EColor.Gray)]
    [Header("INTERACTION MANAGER OPTIONS")]
    public GameObject currentInteraction;


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
    public Transform playerCharacter;
    public Transform cameraPivot;
    public Transform playerGraphic;







    private void Start()
    {
        cameraPivot.parent = GameObject.Find("Cameras").transform;
       _mainspeaker = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlaySFX("Wind");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayOST(1);
        }
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

    public void PlayOST(int id)
    {
        Sound s = OST[id];
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
        s.source.Play();
    }

    public void PlaySFX(int id)
    {
        Sound s = SFX[id];
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;

        s.source.volume = s.localVolume * musicVolume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.Play();
    }

    public void VictoryScreen()
    {
        SceneManager.LoadScene(Victoryscreen);
    }

    public void LooseMission()
    {
        SceneManager.LoadScene(LooseScreen);
    }
}


