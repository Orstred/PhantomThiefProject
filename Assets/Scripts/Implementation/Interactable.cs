using UnityEngine;

public class Interactable : MonoBehaviour
{

    public float interactionRange;
    public Transform InteractionRoot;

    private Transform playercharacter;
    private bool interactabledistance;
   
    protected GameManager manager;



    protected void Start()
    {
        manager = GameManager.instance;
        playercharacter = GameManager.instance.Playercharacter.transform;
    }

    protected void Update()
    {
        if(manager.CurrentInteraction == null || manager.CurrentInteraction == gameObject)
        {
            if (Vector3.Distance(InteractionRoot.transform.position, playercharacter.position) <= interactionRange)
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
        manager.CurrentInteraction = gameObject;
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
        manager.CurrentInteraction = null;
        Debug.Log("Exited Interaction distance of  " + transform.name);
    }




    private void OnDrawGizmosSelected()
    {
        if (InteractionRoot == null)
            InteractionRoot = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(InteractionRoot.position, interactionRange);
    }
}
