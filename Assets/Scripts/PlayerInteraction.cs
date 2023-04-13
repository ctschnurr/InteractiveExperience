using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject currentInterObj = null;

    public Interactable currentInterObjScript = null;

    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && currentInterObj)
        {
            CheckInteraction();
        }
    }

    public void CheckInteraction()
    {
        if(currentInterObjScript.behavior == Interactable.Behavior.blank)
        { currentInterObjScript.Blank(); }

        else if (currentInterObjScript.behavior == Interactable.Behavior.sign)
        { currentInterObjScript.Sign(); }

        else if (currentInterObjScript.behavior == Interactable.Behavior.pickUp)
        { currentInterObjScript.PickUp(); }

        else if (currentInterObjScript.behavior == Interactable.Behavior.npc)
        { currentInterObjScript.Npc(); }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("InterObject") == true)
        {
            currentInterObj = other.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<Interactable>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InterObject") == true)
        {
            currentInterObj = null;
            currentInterObjScript = null;
        }
    }
}
