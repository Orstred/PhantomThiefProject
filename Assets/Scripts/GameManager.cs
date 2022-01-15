using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public enum gamestates {Loss, victory}

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Transform playercharacter;
    public Transform CameraPivot;
    public Transform PlayerGraphic;

    public gamestates GameStates;

    private void Start()
    {
        CameraPivot.parent = GameObject.Find("Cameras").transform;
       

        if(Application.isEditor == false)
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }


}
