using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public enum PlayerStates {Walking, Crouching, Running, Grappling, Falling}

public class PlayerCharacter : MonoBehaviour
{
    [HorizontalLine(2f, EColor.Gray)]
    [Header("STEALTH")]
    public PlayerStates State;

    public Transform PlayerHeadDetector;
    public Transform PlayerChestDetector;
    public Transform DarkNotfication;

    public bool inShadow;
    public List<GameObject> LightSources;
        
    private float headheight;






    private void Start()
    {
        headheight = PlayerHeadDetector.position.y;
    }

    private void Update()
    {
        #region PlayerStates
        //Switches between states
        if (Input.GetButtonDown("ToggleRun"))
        {
            State = PlayerStates.Running;
        }
        else if (Input.GetButtonDown("ToggleCrouch"))
        {
            if(State != PlayerStates.Crouching)
            {
                State = PlayerStates.Crouching;
            }
            else
            {
                State = PlayerStates.Walking;
            }
        }
        else if (State != PlayerStates.Running && State != PlayerStates.Crouching || Input.GetKeyDown(KeyCode.Mouse1))
        {
            State = PlayerStates.Walking; 
        }


        if (State == PlayerStates.Crouching)
        {
            PlayerHeadDetector.position = PlayerChestDetector.position;
            transform.localScale = new Vector3(1f, .3f, 1f);
        }
        else if(State != PlayerStates.Crouching)
        {
            PlayerHeadDetector.position = new Vector3(transform.position.x, headheight, transform.position.z);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        #endregion
        #region Stealth 
        inShadow = (LightSources.Count <= 0);
        DarkNotfication.gameObject.SetActive(inShadow);
        #endregion
    }






}
