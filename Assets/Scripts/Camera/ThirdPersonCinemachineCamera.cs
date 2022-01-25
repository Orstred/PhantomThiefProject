using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ThirdPersonCinemachineCamera : MonoBehaviour
{

    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;
    Transform _transform;
    Transform PlayerCharacter;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _transform = transform;
        PlayerCharacter = GameManager.instance.Playercharacter;
    }


    private void Update()
    {
            xAxis.Update(Time.fixedDeltaTime);
            yAxis.Update(Time.fixedDeltaTime);

            _transform.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);      
    }
    private void LateUpdate()
    {
        transform.position = PlayerCharacter.transform.position + Vector3.up * 1.79f;
    }


    


}
