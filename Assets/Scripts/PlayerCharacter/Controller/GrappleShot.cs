using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using NaughtyAttributes;
public class GrappleShot : MonoBehaviour
{
    //Instances
    Camera MainCamera;
    Transform MainCameraTransform;
    Transform CameraPivot;
    [Foldout("Instances")]
    public RectTransform GrappleCrosshair;
    [Foldout("Instances")]
    public CinemachineVirtualCamera ReelingInCamera;


    //Stats & Options
    [Foldout("Stats & Options")]
    public float ReelingSpeed;
    [Foldout("Stats & Options")]
    public float ReelStopDistance;
    [Foldout("Stats & Options")]
    public float GrappleShotRange;


    //PlayerCharacter Components instantiation
    Transform PlayerCharacter;
    PlayerMotor motor;
    CharacterController controller;

    //Local Variables
    bool isReeling = false;
    bool isGrappling = false;
    LineRenderer _ropegraphic;
    Vector3 LastRaycastPosition;







    private void Start()
    {

        //Public Instantiation
        CameraPivot = GameManager.instance.CameraPivot;
        MainCamera = Camera.main;
        MainCameraTransform = MainCamera.transform;


        //Local instantiation
        _ropegraphic = GetComponent<LineRenderer>();


        //Player Character component instantiation
        PlayerCharacter = GameManager.instance.playercharacter.gameObject.transform;
        motor = PlayerCharacter.GetComponent<PlayerMotor>();
        controller = PlayerCharacter.GetComponent<CharacterController>();

        Ray ray = MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            GrappleCrosshair.position = hit.point;
            GrappleCrosshair.parent.transform.parent = MainCameraTransform;
        }
    }

    private void Update()
    {


        #region Raycast
        Ray ray = MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

          
            LastRaycastPosition = hit.point;
            if (GrappleCrosshair.parent == null && hit.transform.tag != "Player")
            {
                //Makes the crosshair always have the same screen size
                float dist = (MainCameraTransform.position - GrappleCrosshair.position).magnitude;
                GrappleCrosshair.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(GrappleCrosshair.sizeDelta.x, dist / 8, Time.deltaTime * 60));
                GrappleCrosshair.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(GrappleCrosshair.sizeDelta.y, dist / 8, Time.deltaTime * 60));

                GrappleCrosshair.position = hit.point;
                GrappleCrosshair.SetParent(MainCameraTransform, true); 

            }
            

       

        }
        else
        {

            GrappleCrosshair.SetParent(null, true);  
            GrappleCrosshair.position = LastRaycastPosition;

        }
        #endregion






        if (Input.GetButtonDown("Fire1"))
            {
                OnGrappleStart();

            }
        if (Input.GetButton("Fire1"))
            {
                if (isReeling)
                {
                    OnGrappleStay();

                }

            }
        if (Input.GetButtonUp("Fire1"))
            {
                OnGrappleExtit();

            }

        if (isGrappling)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                OnReelStart();
            }
            if (Input.GetButton("Fire2"))
            {
                if (isReeling)
                {
                    OnReelStay();
                }

            }
            if (Input.GetButtonUp("Fire2"))
            {
                OnReelExit();
            }
        }
   
    }


    private void LateUpdate()
    {

        GrappleCrosshair.LookAt(MainCamera.transform.position);
    }











    public void OnGrappleStart()
    {
        isReeling = true;
        isGrappling = true;
        _ropegraphic.enabled = true;
        _ropegraphic.SetPosition(1, LastRaycastPosition);
        
    }
    public void OnGrappleStay()
    {
        _ropegraphic.SetPosition(0, CameraPivot.position);
    
    }
    public void OnGrappleExtit()
    {
        isReeling = false;
        isGrappling = false;
        _ropegraphic.enabled = false;
        OnReelExit();
    }

    public void OnReelStart()
    {
        isReeling = true;
        motor.enabled = false;
        controller.enabled = false;
        PlayerCharacter.LookAt(_ropegraphic.GetPosition(1));
        ReelingInCamera.Priority = 11;
    }
    public void OnReelStay()
    {
        if(Vector3.Distance(PlayerCharacter.position, _ropegraphic.GetPosition(1)) > ReelStopDistance) 
        {
            PlayerCharacter.position = Vector3.Lerp(PlayerCharacter.position, _ropegraphic.GetPosition(1), (Time.deltaTime * ReelingSpeed) / Vector3.Distance(PlayerCharacter.position, _ropegraphic.GetPosition(1)));
        }
        else if(Input.GetButtonDown("Jump"))
        {
            OnReelExit();
            OnGrappleExtit();
            motor.Jump(5);
        }
       
    }
    public void OnReelExit()
    {
      
            isReeling = false;
            motor.enabled = true;
            controller.enabled = true;
            PlayerCharacter.rotation = Quaternion.Euler(0, PlayerCharacter.eulerAngles.y, 0);
            motor.Jump(0.01f);
        if(isGrappling)
        OnGrappleExtit();
        ReelingInCamera.Priority = 0;

    }

}
