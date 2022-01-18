using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine.Audio;
using System;
[System.Serializable]
public class SoundClip
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

[System.Serializable]
public enum gamestates {inProgress,Loss, victory}

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        //singleton setup
        instance = this;


        //Audio manager setup
        foreach(SoundClip s in OST)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.localVolume * MainVolume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    #endregion



    [HorizontalLine(2,EColor.Gray)]
    [Header("MANAGER OPTIONS")]
    public gamestates GameStates;
     




    [HorizontalLine(2, EColor.Gray)]
    [Header("AUDIO OPTIONS")]
    [Range(0,1)]
    public float MainVolume = 1f;
    [Range(0, 1)]
    public float SFXVolume = 1f;
    public SoundClip[] OST;



    [HorizontalLine(2, EColor.Gray)]
    [Header("GLOBAL INSTANCES")]
    public Transform playercharacter;
    public Transform CameraPivot;
    public Transform PlayerGraphic;









    private void Start()
    {
        CameraPivot.parent = GameObject.Find("Cameras").transform;
        DontDestroyOnLoad(gameObject);

        if(Application.isEditor == false)
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayOST(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayOST(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayOST(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayOST(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayOST(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayOST(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayOST(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            PlayOST(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            PlayOST(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            PlayOST(9);
        }
    }





    public void PlayOST(string name)
    {
        SoundClip s = Array.Find(OST, SoundClip => SoundClip.name == name);
        if (s == null)
        {
            Debug.Log("OST " + name + " not found");
            return;
        }
           


        s.source.Play();
    }

    public void PlayOST(int Track)
    {
        if(OST[Track] != null)
        OST[Track].source.Play();
    }
}


