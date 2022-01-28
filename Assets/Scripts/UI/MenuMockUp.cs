using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMockUp : MonoBehaviour
{

    public RectTransform t;
    public bool optionsopen;
    AudioSource source;
    Vector3 startpos;





    private void Start()
    {
        startpos = t.gameObject.transform.position;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (optionsopen)
        {
            t.rotation = Quaternion.Slerp(t.rotation, Quaternion.identity,Time.deltaTime * 4);
            t.position = Vector3.Lerp(t.position, new Vector3(startpos.x, startpos.y + 94, startpos.z), Time.deltaTime * 4);
      
        }
        else
        {
            t.rotation = Quaternion.Slerp(t.rotation, Quaternion.Euler(0, 0, -7.29f), Time.deltaTime * 4);
            t.position = Vector3.Lerp(t.position, startpos, Time.deltaTime * 4);
        }
    }






    public static void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public  void OptionsMenu()
    {
        optionsopen = !optionsopen;
        source.Play();
    }
}