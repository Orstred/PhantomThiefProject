using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{


    #region Intantiation
    CharacterController _controller;
    Transform PlayerGraphic;
    Transform Cameraparent;
    Transform _transform;

    private void Start()
    {
        PlayerGraphic = GameManager.instance.PlayerGraphic;
        _controller = GetComponent<CharacterController>();
        Cameraparent = GameManager.instance.CameraPivot;
        _transform = transform;
    }
    #endregion

    #region Options
    [MinMaxSlider(0.0f, 36f)]
    public Vector2 WalkRunSpeed;
    public float TurnSpeed;
    public float Weight;
    public float JumpHeight;
    public LayerMask GroundLayer;
    #endregion


    float CurrentSpeed;
    Vector3 GravityVelocity;



    private void Update()
    {
       


        

        #region Gravity
        bool isGrounded = Physics.CheckSphere(transform.position, 0.2f, GroundLayer);

        if (isGrounded && GravityVelocity.y < 0)
        {

            GravityVelocity.y = -Weight;

            if (Input.GetButton("Jump"))
            {
                Jump(JumpHeight);
            }


        }
        //Applies gravity when not on ground
        GravityVelocity.y -= Weight * Time.deltaTime;

        _controller.Move(GravityVelocity * Time.deltaTime);

        #endregion

        #region Inputs
        //Switches between running and walking speeds
        CurrentSpeed = (CurrentSpeed != WalkRunSpeed.y) && Input.GetButtonDown("ToggleRun") || (Input.anyKey) && (CurrentSpeed == WalkRunSpeed.y) && (!Input.GetButtonDown("ToggleRun"))? WalkRunSpeed.y : WalkRunSpeed.x;

        //WASD Inputs
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {


            Moveonangle(GetKeyboardInputAxisAngle()) ;

        }
        #endregion

  
    }



























    //Moves the player at the provided angle
    void Moveonangle(float A)
    {
       float CameraYRotation = Cameraparent.eulerAngles.y;

       transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, CameraYRotation + A, 0), Time.deltaTime * TurnSpeed);

       _controller.Move(transform.forward  * Time.deltaTime * CurrentSpeed);
 
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


    public void Jump(float jumheight = 0f)
    {
        GravityVelocity.y = Mathf.Sqrt(jumheight * 2f * Weight);
    }


    //Moves the parent to the same position as the player graphic
    public void ResetRootPos()
    {
        _controller.enabled = false;
        PlayerGraphic.parent = null;
        _transform.parent = PlayerGraphic.transform;
        _transform.localPosition = Vector3.zero;
        PlayerGraphic.parent = _transform;
        _controller.enabled = true;
    }


    //Moves the parent to a given position relative to the child
    public void MoveParent(Transform parent, Transform child, Vector3 NewPosition)
    {
        _controller.enabled = false;
        parent.parent = child;
        parent.localPosition = NewPosition;
        child.parent = parent;
        _controller.enabled = true;
    }


    //Sends a Raycast that checks to see if there is a wall in front of it and if there is it returns the measurements
    public Vector3 GetObjectSize(Transform raycenter, float DetectDistance = 1f, string ObjectTag = "Wall")
    {
        RaycastHit hit;

        if (Physics.Raycast(raycenter.position, transform.forward, out hit, DetectDistance))
        {
            if (hit.transform.tag == ObjectTag)
            {
                return Vector3.Scale(hit.transform.GetComponent<MeshFilter>().sharedMesh.bounds.size, hit.transform.localScale);

            }
        }
        return Vector3.zero;
    }
        
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * JumpHeight);
        Gizmos.DrawSphere(transform.position + transform.up * JumpHeight, 0.2f);
    }

}
