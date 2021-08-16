using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GrappleCrosshair : MonoBehaviour
{


    Camera cam;
    Transform _transform;
    private void Start()
    {
        _transform = transform;
        cam = Camera.main;
    }


    private void Update()
    {
        float dist = (cam.transform.position - _transform.position).magnitude;

        _transform.localScale = new Vector3(.02f,.02f,.02f) * dist;
    }

}
