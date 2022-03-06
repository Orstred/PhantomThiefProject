using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class SoundWave : MonoBehaviour
{

    public List<GameObject> EnemiesInVicinity;
    public float Intensity;
    SphereCollider col;
    Rigidbody rb;
 



    private void Start()
    {
        col = GetComponent<SphereCollider>();
        col.isTrigger = true;

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void Update()
    {
      

        if(col.radius != Intensity) { col.radius = Mathf.Lerp(col.radius, Intensity, Time.deltaTime * 7); }
        else {GivePositionToEnemies(EnemiesInVicinity);}
      


    }


    private void OnTriggerEnter(Collider other)
    {
      
        if(other.tag == "Enemy" && !EnemiesInVicinity.Contains(other.transform.gameObject))
        {
            EnemiesInVicinity.Add(other.transform.gameObject);
        }
    }
        

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Intensity);
    }

    public void GivePositionToEnemies(List<GameObject> t)
    {
        foreach(GameObject en in t)
        {
            en.GetComponent<IEnemy>().SuspiciousPoint(transform.position);
        }
        Destroy(gameObject);
    }
}
