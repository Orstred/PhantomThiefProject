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
        playergraphic = GameManager.instance.playerGraphic;
        cameraparent = GameManager.instance.cameraPivot;
        _transform = transform;
    }
    #endregion



    //Movement Stats
    [HorizontalLine(2f, EColor.Gray)]
    [Header("MOVEMENT")]
    public float crouchSpeed = 5;
    public float walkSpeed = 10;
    public float runSpeed = 20;
    public float turnSpeed = 10;
    public float Acceleration = 5;

    [HorizontalLine(2f, EColor.Gray)]
    [Header("GRAVITY")]
    public float playerWeight = 50;
    public float jumpHeight = 3;
    public LayerMask groundLayers;
    public float groundDistance = .4f;


    [HideInInspector]
    public float speed;
    [HideInInspector]
    public bool isGrounded;

    //Local Vars
    private bool iscrouching = false;
    private bool isrunning = false;
    private Vector3 gravityvelocity;
    private float currentspeed;
   

        


    private void Update()
    {
        

        //Accelerates and decelerates the player using lerps
        if (!(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0))
        {
            
             speed = Mathf.Lerp(speed, currentspeed, Acceleration * Time.deltaTime);

        }
        else
        {
            
            speed = Mathf.Lerp(speed, 0, Time.deltaTime * Acceleration);
        }

        #region Gravity and Jumping
        isGrounded = Physics.CheckSphere(transform.position, 0.2f, groundLayers);

        //Calculates gravity when on ground
        if (isGrounded && gravityvelocity.y < 0)
        {
            gravityvelocity.y = -15;

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

        #region Movement Inputs and Run/Crouching Toggles

        
        //Switches between running and walking speeds
        if (Input.GetButtonDown("ToggleRun"))
        {
            currentspeed = runSpeed;
            isrunning = !isrunning;
            iscrouching = false;
        } 
        else if (Input.GetButtonDown("ToggleCrouch"))
        {
            if(!iscrouching)
            currentspeed = crouchSpeed;
            isrunning = !isrunning;
            iscrouching = !iscrouching;
        }
        //Goes back to walking if no inputs are being received or the player stops walking and is not crouching
        else if(!isrunning && !iscrouching || Input.GetKeyDown(KeyCode.Mouse1))
        {
            currentspeed = walkSpeed;
        }
        //Same as above but for controller
        else if(!Input.anyKey && !iscrouching ||!iscrouching && Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
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




    //Returns angle based on WASD inputs and current angle of the camera pivot
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

        _controller.Move(transform.forward * Time.deltaTime * speed);
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
