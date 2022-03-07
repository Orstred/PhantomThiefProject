using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEditor.SceneManagement;

[CreateAssetMenu(fileName = "SceneGroup", menuName = "Level/SceneGroup", order = 1)]
public class SceneGroup : ScriptableObject
{
    [Scene]
    public string[] scenes;

    
    public void Load()
    {
        SceneManager.LoadScene(scenes[0],LoadSceneMode.Single);
       foreach (string s in scenes)
        {
            if(s != scenes[0])
            SceneManager.LoadScene(s,LoadSceneMode.Additive);
        }
    }

}
