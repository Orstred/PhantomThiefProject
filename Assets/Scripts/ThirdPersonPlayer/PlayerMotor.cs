using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{


    #region Instantiation
    CharacterController _controller;
    PlayerCharacter _playercharacter;
    Transform playergraphic;
    Transform Cameraparent;
    Transform _transform;
    private void Start()
    {
        _playercharacter = GetComponent<PlayerCharacter>();
        _controller = GetComponent<CharacterController>();
        playergraphic = GameManager.instance.PlayerGraphic;
        Cameraparent = GameManager.instance.CameraPivot;
        _transform = transform;
    }
    #endregion


    #region Movement Stats

    public float WalkSpeed = 10, RunSpeed = 20, CrouchSpeed = 5;

    public float TurnSpeed = 10;

    public float Weight = 25;

    public float JumpHeight = 3;

    public LayerMask GroundLayers;

    public float GroundDistance = .4f;
    #endregion


    #region Local Variables
    //movement
    float currentspeed;
    bool iscrouching = false;
    bool isrunning = false;

    Vector3 gravityvelocity;
    #endregion




    private void Update()
    {
        #region Gravity
        bool isGrounded = Physics.CheckSphere(transform.position, 0.2f, GroundLayers);

        //Calculates gravity when on ground
        if (isGrounded && gravityvelocity.y < 0)
        {

            gravityvelocity.y = -Weight;

            if (Input.GetButton("Jump"))
            {
                Jump(JumpHeight);
            }


        }


        //Calculates gravity when not on ground increasing momentum
        gravityvelocity.y -= Weight * Time.deltaTime;


        //Aplies gravity after calculations
        _controller.Move(gravityvelocity * Time.deltaTime);

        #endregion

        #region Movement Inputs
        //Switches between running and walking speeds
        if (Input.GetButtonDown("ToggleRun"))
        {
            currentspeed = RunSpeed;
            isrunning = !isrunning;
            iscrouching = false;
        } 
        else if (Input.GetButtonDown("ToggleCrouch"))
        {
            currentspeed = CrouchSpeed;
            isrunning = false;
            iscrouching = !iscrouching;
        }
        else if(!isrunning && !iscrouching || Input.GetKeyDown(KeyCode.Mouse1))
        {
            currentspeed = WalkSpeed;
        }
        else if(!Input.anyKey && !iscrouching)
        {
            currentspeed = WalkSpeed;
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
        float CameraYRotation = Cameraparent.eulerAngles.y;

         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, CameraYRotation + GetKeyboardInputAxisAngle(), 0), Time.deltaTime * TurnSpeed); 

        _controller.Move(transform.forward * Time.deltaTime * currentspeed);
    }

    public void Jump(float jumheight = 0.1f)
    {
        gravityvelocity.y = Mathf.Sqrt(jumheight * 2f * Weight);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * JumpHeight);
        Gizmos.DrawSphere(transform.position + transform.up * JumpHeight, 0.2f);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, GroundDistance);
    }

}
