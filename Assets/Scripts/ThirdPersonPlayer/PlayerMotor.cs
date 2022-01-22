using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{


    #region Instantiation
    private CharacterController _controller;
    private PlayerCharacter playercharacter;
    private Transform playergraphic;
    private Transform cameraparent;
    private Transform _transform;
    private void Start()
    {
        playercharacter = GetComponent<PlayerCharacter>();
        _controller = GetComponent<CharacterController>();
        playergraphic = GameManager.instance.Playergraphic;
        cameraparent = GameManager.instance.Camerapivot;
        _transform = transform;
    }
    #endregion


  
    //Movement Stats
    public float walkSpeed = 10, runSpeed = 20, crouchSpeed = 5;
    public float turnSpeed = 10;
    public float playerWeight = 50;
    public float jumpHeight = 3;
    public LayerMask groundLayers;
    public float groundDistance = .4f;
   


    
    //Local Vars
    private float currentspeed;
    private bool iscrouching = false;
    private bool isrunning = false;
    private Vector3 gravityvelocity;
 




    private void Update()
    {
        #region Gravity
        bool isGrounded = Physics.CheckSphere(transform.position, 0.2f, groundLayers);

        //Calculates gravity when on ground
        if (isGrounded && gravityvelocity.y < 0)
        {

            gravityvelocity.y = -playerWeight;

            if (Input.GetButton("Jump"))
            {
                Jump(jumpHeight);
            }


        }


        //Calculates gravity when not on ground increasing momentum
        gravityvelocity.y -= playerWeight * Time.deltaTime;


        //Aplies gravity after calculations
        _controller.Move(gravityvelocity * Time.deltaTime);

        #endregion

        #region Movement Inputs
        //Switches between running and walking speeds
        if (Input.GetButtonDown("ToggleRun"))
        {
            currentspeed = runSpeed;
            isrunning = !isrunning;
            iscrouching = false;
        } 
        else if (Input.GetButtonDown("ToggleCrouch"))
        {
            currentspeed = crouchSpeed;
            isrunning = false;
            iscrouching = !iscrouching;
        }
        else if(!isrunning && !iscrouching || Input.GetKeyDown(KeyCode.Mouse1))
        {
            currentspeed = walkSpeed;
        }
        else if(!Input.anyKey && !iscrouching)
        {
            currentspeed = walkSpeed;
            isrunning = false;    
        }

        //WASD Inputs
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Moveonangle(GetKeyboardInputAxisAngle());
        }
        #endregion
    }




    //Returns angle based on WASD inputs
    float GetKeyboardInputAxisAngle()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        return targetangle;
    }

    //Moves the player at the provided angle based on the cameras current angle
    void Moveonangle(float A)
    {
        float CameraYRotation = cameraparent.eulerAngles.y;

         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, CameraYRotation + GetKeyboardInputAxisAngle(), 0), Time.deltaTime * turnSpeed); 

        _controller.Move(transform.forward * Time.deltaTime * currentspeed);
    }

    public void Jump(float jumheight = 0.1f)
    {
        gravityvelocity.y = Mathf.Sqrt(jumheight * 2f * playerWeight);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * jumpHeight);
        Gizmos.DrawSphere(transform.position + transform.up * jumpHeight, 0.2f);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, groundDistance);
    }

}
