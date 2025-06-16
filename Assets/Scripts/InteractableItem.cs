using UnityEngine;
using System.Collections;
using System;

public class InteractableItem : MonoBehaviour
{
    public Action A_InteractionExecuted;

    public void InitiateInteraction()
    {
        //Debug.Log("Initiate interaction function of object by name " + transform.name);
        A_InteractionExecuted?.Invoke();
    }
}
