using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{


    #region Instantiation
    CharacterController _controller;
    Transform playergraphic;
    Transform Cameraparent;
    Transform _transform;
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        playergraphic = GameManager.instance.PlayerGraphic;
        Cameraparent = GameManager.instance.CameraPivot;
        _transform = transform;
    }
    #endregion


    #region Movement Stats

    public float WalkSpeed = 5, RunSpeed = 15;

    public float TurnSpeed = 10;

    public float Weight = 25;

    public float JumpHeight = 3;

    public LayerMask GroundLayers;

    public float GroundDistance = .4f;
    #endregion


    #region Local Variables
    float currentspeed;
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
        currentspeed = (currentspeed != RunSpeed) && Input.GetButtonDown("ToggleRun") || (Input.anyKey) && (currentspeed == RunSpeed) && (!Input.GetButtonDown("ToggleRun")) ? RunSpeed : WalkSpeed;

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

        transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, CameraYRotation + A, 0), Time.deltaTime * TurnSpeed);

        _controller.Move(transform.forward * Time.deltaTime * currentspeed);
    }

    public void Jump(float jumheight = 0.1f)
    {
        gravityvelocity.y = Mathf.Sqrt(jumheight * 2f * Weight);
    }

    //Moves the parent to the same position as the player graphic
    public void ResetRootPos()
    {
        _controller.enabled = false;
        _transform.SetParent(playergraphic, true);
        _transform.localPosition = Vector3.zero;
        playergraphic.SetParent(_transform);
        _controller.enabled = true;
    }

    //Moves the parent to a given position relative to the child
    public void MoveParent(Transform parent, Transform child, Vector3 NewPosition)
    {
        _controller.enabled = false;
        parent.parent.SetParent(child);
        parent.localPosition = NewPosition;
        child.parent.SetParent(parent);
        _controller.enabled = true;
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
