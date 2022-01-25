using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum PlayerStates { Walking, Crouching, Running, Grappling, Driving, Falling}

public class PlayerCharacter : MonoBehaviour
{

    public PlayerStates State;

    public Transform PlayerHeadDetector;
    public Transform PlayerChestDetector;


    private float headheight;

    private bool isrunning;
    private bool iscrouching;


    private void Start()
    {
        headheight = PlayerHeadDetector.position.y;
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
            State = PlayerStates.Walking;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }


        if (State == PlayerStates.Crouching)
        {
            PlayerHeadDetector.position = PlayerChestDetector.position;
        }
        else if(State != PlayerStates.Crouching)
        {
            PlayerHeadDetector.position = new Vector3(transform.position.x, headheight, transform.position.z);
        }
    }






}
