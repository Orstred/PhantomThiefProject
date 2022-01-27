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
        PlayerCharacter = GameManager.instance.playerCharacter;
        if (!Application.isEditor)
        {
            xAxis.m_MaxSpeed /= 2;
            yAxis.m_MaxSpeed /= 2;
        }
    }


    private void Update()
    {
            xAxis.Update(Time.deltaTime);
            yAxis.Update(Time.deltaTime);

            _transform.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);      
    }
    private void LateUpdate()
    {
        _transform.position = PlayerCharacter.transform.position + Vector3.up * 1.79f;
    }


    


}
