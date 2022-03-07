using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public float interactionRange;
    public Transform interactionRoot;

    protected Transform playercharacter;
    protected bool interactabledistance;
   
    protected GameManager manager;



    protected void Start()
    {
        manager = GameManager.instance;
        playercharacter = GameManager.instance.playerCharacter.transform;
    }

    protected void Update()
    {
        if(manager.currentInteraction == null || manager.currentInteraction == gameObject)
        {
            if (Vector3.Distance(interactionRoot.position, playercharacter.position) <= interactionRange)
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
        manager.currentInteraction = gameObject;
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
        manager.currentInteraction = null;
        Debug.Log("Exited Interaction distance of  " + transform.name);
    }




    private void OnDrawGizmosSelected()
    {
        if (interactionRoot == null)
            interactionRoot = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionRoot.position, interactionRange);
    }
}
