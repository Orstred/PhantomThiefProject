using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fullsteamahead : MonoBehaviour
{
    public float Speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Speed * Time.deltaTime;
    }
}
