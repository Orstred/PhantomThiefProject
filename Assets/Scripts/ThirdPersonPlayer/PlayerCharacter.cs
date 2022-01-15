using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum PlayerStates { walking, Crouching, Running, Grappling, Driving}

public class PlayerCharacter : MonoBehaviour
{

    public PlayerStates State;

    public Transform PlayerHeadDetector;
    public Transform PlayerChestDetector;

    float HeadHeight;


    bool isrunning;
    bool iscrouching;

    private void Start()
    {
        HeadHeight = PlayerHeadDetector.position.y;
    }

    private void Update()
    {
        if (Input.GetButtonDown("ToggleRun"))
        {
            State = PlayerStates.Running;
            isrunning = !isrunning;
            iscrouching = false;
            transform.localScale = Vector3.one;
        }
        else if (Input.GetButtonDown("ToggleCrouch"))
        {
            State = PlayerStates.Crouching;
            isrunning = false;
            iscrouching = !iscrouching;
            transform.localScale =new Vector3(1f, .5f, 1f);
        }
        else if (!isrunning && !iscrouching || Input.GetKeyDown(KeyCode.Mouse1))
        {
            State = PlayerStates.walking;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }


        if (State == PlayerStates.Crouching)
        {
            PlayerHeadDetector.position = PlayerChestDetector.position;
        }
        else if(State != PlayerStates.Crouching)
        {
            PlayerHeadDetector.position = new Vector3(transform.position.x, HeadHeight, transform.position.z);
        }
    }






}
