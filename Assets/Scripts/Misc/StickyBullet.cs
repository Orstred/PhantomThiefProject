using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBullet : MonoBehaviour
{

    public float speed;
    bool isMoving = true;
    Transform pla;

    private void Start()
    {
        pla = GameManager.instance.playerCharacter.transform;
    }
    // Update is called once per frame
    void Update()
    {

        if(isMoving)
        {
            transform.position += transform.forward * (Time.deltaTime + speed);
        }
        else
        {
            transform.localScale += -Vector3.one * (Time.deltaTime * Random.Range(0.1f, .01f));
            if(transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            isMoving = false;
            transform.parent = pla;
            GetComponent<Rigidbody>().isKinematic = true;

        }
    }

}
