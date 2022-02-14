using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using NaughtyAttributes;
public class GrappleShot : MonoBehaviour
{






    #region Stats & Options
    [Foldout("Stats & Options")]
    public float reelingSpeed = 100f;
    [Foldout("Stats & Options")]
    public float reelStopDistance = 0.2f;
    [Foldout("Stats & Options")]
    public float jumpableDistance = 0.4f;
    [Foldout("Stats & Options")]
    public float grappleShotRange = 10000;
    [Foldout("Stats & Options")]
    public LayerMask ignoreLayer;
    #endregion

    #region Instances
    //Camera instances
    [Foldout("Instances")]
    public CinemachineVirtualCamera ReelingInCamera;
    private Camera maincamera;
    private Transform maincameratransform;
    private Transform camerapivot;


    //Grapple instances
    [Foldout("Instances")]
    public RectTransform GrappleCrosshair;
    private LineRenderer _ropegraphic;


    //PlayerCharacter Components instantiation
    private Transform PlayerCharacter;
    private PlayerMotor motor;
    private CharacterController controller;
    #endregion

    #region local variables
    private bool isreeling = false;
    private bool isgrappling = false;
    private Vector3 lastrayposition;
    #endregion






    private void Start()
    {
        GrappleCrosshair.SetParent(null);
        

        //Public Instantiation
        camerapivot = GameManager.instance.cameraPivot;
        maincamera = Camera.main;
        maincameratransform = maincamera.transform;

        //Local instantiation
        _ropegraphic = GetComponent<LineRenderer>();
        ReelingInCamera.transform.parent = GameObject.Find("Cameras").transform;

        //Player Character component instantiation
        PlayerCharacter = GameManager.instance.playerCharacter.gameObject.transform;
        motor = PlayerCharacter.GetComponent<PlayerMotor>();
        controller = PlayerCharacter.GetComponent<CharacterController>();

        Ray ray = maincamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GrappleCrosshair.position = hit.point;
        }

        transform.parent = null;
    }

    private void Update()
    {
        //Shoot Grapple hook
        if (Input.GetButtonDown("Fire1"))
            {
                OnGrappleStart();
            }
        if (Input.GetButton("Fire1"))
            {
                if (isreeling)
                {
                    OnGrappleStay();

                }
            }
        if (Input.GetButtonUp("Fire1"))
            {
                OnGrappleExtit();

            }

        
        //Reeel in
        if (isgrappling)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                OnReelStart();
            }
            if (Input.GetButton("Fire2"))
            {
                if (isreeling)
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
        GrappleCrosshair.LookAt(maincamera.transform.position);
        #region Raycast
        Ray ray = maincamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        float dist = (maincameratransform.position - GrappleCrosshair.position).magnitude;
        if (Physics.Raycast(ray, out hit, grappleShotRange, ~ignoreLayer))
        {

            lastrayposition = hit.point;
            if (GrappleCrosshair.parent == null && hit.transform.tag != "Player")
            {
                //Makes the crosshair always have the same screen size
                GrappleCrosshair.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(GrappleCrosshair.sizeDelta.x, dist / 8, Time.deltaTime * 60));
                GrappleCrosshair.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(GrappleCrosshair.sizeDelta.y, dist / 8, Time.deltaTime * 60));

                GrappleCrosshair.position = hit.point;
                GrappleCrosshair.SetParent(maincameratransform, true);

            }
        }
        else
        {

            GrappleCrosshair.SetParent(null, true);
            GrappleCrosshair.position = lastrayposition;

        }
        //Makes crosshair always have the same screen size
        GrappleCrosshair.localScale = new Vector3(.02f, .02f, .02f) * dist;
        #endregion
    }









    public void OnGrappleStart()
    {
        isreeling = true;
        isgrappling = true;
        _ropegraphic.enabled = true;
        _ropegraphic.SetPosition(1, lastrayposition);
        
    }
    public void OnGrappleStay()
    {
        _ropegraphic.SetPosition(0, camerapivot.position);
    
    }
    public void OnGrappleExtit()
    {
        isreeling = false;
        isgrappling = false;
        _ropegraphic.enabled = false;
        OnReelExit();
    }

    public void OnReelStart()
    {
        isreeling = true;
        motor.enabled = false;
        controller.enabled = false;
        PlayerCharacter.LookAt(_ropegraphic.GetPosition(1));
        ReelingInCamera.Priority = 11;
    }
    public void OnReelStay()
    {
        if (Input.GetButton("Jump") && Vector3.Distance(PlayerCharacter.position, _ropegraphic.GetPosition(1)) <= jumpableDistance)
        {
            OnReelExit();
            OnGrappleExtit();
            motor.Jump(5);
        }
        if (Vector3.Distance(PlayerCharacter.position, _ropegraphic.GetPosition(1)) > reelStopDistance) 
        {
            PlayerCharacter.position = Vector3.Lerp(PlayerCharacter.position, _ropegraphic.GetPosition(1), (Time.deltaTime * reelingSpeed) / Vector3.Distance(PlayerCharacter.position, _ropegraphic.GetPosition(1)));
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
      
            isreeling = false;
            motor.enabled = true;
            controller.enabled = true;
            PlayerCharacter.rotation = Quaternion.Euler(0, PlayerCharacter.eulerAngles.y, 0);
            motor.Jump(0.01f);
            ReelingInCamera.Priority = 0;

        if (isgrappling)
        OnGrappleExtit();

    }

}
