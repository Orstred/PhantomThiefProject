using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;



public class PlayerCharacter : MonoBehaviour
{
    [HorizontalLine(2f, EColor.Gray)]
    [Header("STEALTH")]

    public Transform PlayerHeadDetector;
    public Transform PlayerChestDetector;
    public Transform DarkNotfication;

    public bool inShadow;
    public List<GameObject> LightSources;

    private Vector3 headheight;


    [HorizontalLine(2f, EColor.Gray)]
    [Header("ANIMATION")]

    public bool isIdle;
    public bool isWalking;
    public bool isCrouching;
    public bool isGrappling;
    public float currentSpeed;



    //Privates
    [HideInInspector]
    public PlayerMotor motor;



    private void Start()
    {
        headheight = new Vector3(0,PlayerHeadDetector.position.y,0);
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        //Switches between states
        #region PlayerStates
        ResetStates();
        currentSpeed = motor.speed;

        if (Input.GetButtonDown("ToggleCrouch")) { isCrouching = !isCrouching; PlayerHeadDetector.position = (PlayerHeadDetector.position == PlayerChestDetector.position) ? headheight : PlayerChestDetector.position; }

        if (Input.GetButtonDown("ToggleRun")) { isCrouching = false; }

        if (Input.GetKey(KeyCode.Mouse1)) { isGrappling = true; }

        if (currentSpeed > .5f && !isGrappling && motor.isGrounded) { isWalking = true; }
             
        if (!isWalking && !isGrappling && motor.isGrounded) { isIdle = true; }
        #endregion
        #region Stealth 
        inShadow = (LightSources.Count <= 0);
        DarkNotfication.gameObject.SetActive(inShadow);
        #endregion
    }

    public void ResetStates()
    {
        isIdle = false;
        isWalking = false;
        isGrappling = false; 
    }

    public void RemoveLight(GameObject g)
    {
        LightSources.Remove(g);
    }
}





