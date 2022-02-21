using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsSpawner : MonoBehaviour
{

    
    public GameObject[] Credits;
    public float TimeSpace;
    float rate;
    int cur = 0;
    void Start()
    {
        rate = TimeSpace;
    }

    
    void Update()
    {
        rate -= Time.deltaTime;

        if(rate <= 0)
        {
            if(Credits.Length > cur)
            {
                Instantiate(Credits[cur],transform.position,Quaternion.Euler(0,0,0));
                cur++;
            }
            rate = TimeSpace;
        }
    }
}
