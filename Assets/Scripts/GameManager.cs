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
    [Range(0,100)]
    public float MainVolume = 1f;
    [Range(0, 100)]
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayOST("Raid");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayOST("Techno");
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


}


