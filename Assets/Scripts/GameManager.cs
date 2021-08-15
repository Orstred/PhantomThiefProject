using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
  

    private void Start()
    {
      if(Application.isEditor == false)
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }


}
