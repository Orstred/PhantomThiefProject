using UnityEngine;

public class Interactable : MonoBehaviour
{

    public float Range;
    public Transform InteractionRoot;

    private Transform playercharacter;
    private bool interactabledistance;
   
    protected GameManager Manager;



    protected void Start()
    {
        Manager = GameManager.instance;
        playercharacter = GameManager.instance.Playercharacter.transform;
    }

    protected void Update()
    {
        if(Manager.CurrentInteraction == null || Manager.CurrentInteraction == gameObject)
        {
            if (Vector3.Distance(InteractionRoot.transform.position, playercharacter.position) <= Range)
            {
                if (!interactabledistance)
                {
                    OnInteractionDistanceEnter();
                }
                else
                {
                    OnInteractionDistanceStay();
                }

            }
            else if (interactabledistance)
            {
                OnInteractctionDistanceExit();
            }
        }
    }

    

    public virtual void Interact()
    {
        Debug.Log("Interacted with " + transform.name);
    }

    public virtual void OnInteractionDistanceEnter()
    {
        interactabledistance = true;
        Manager.CurrentInteraction = gameObject;
        Debug.Log("Entered Interaction distance of  " + transform.name);
    }
    public virtual void OnInteractionDistanceStay()
    {
        if (Input.GetButtonDown("Interact"))
            Interact();
    }
    public virtual void OnInteractctionDistanceExit()
    {
        interactabledistance = false;
        Manager.CurrentInteraction = null;
        Debug.Log("Exited Interaction distance of  " + transform.name);
    }




    private void OnDrawGizmosSelected()
    {
        if (InteractionRoot == null)
            InteractionRoot = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(InteractionRoot.position, Range);
    }
}
