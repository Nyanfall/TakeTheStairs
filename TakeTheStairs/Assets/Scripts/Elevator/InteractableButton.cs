using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractableButton : MonoBehaviour, IInteractable
{
    public GameObject destinationPoint;
    public ElevatorMovement ElevatorMovement;
    
    public void Interact()
    {
        Debug.Log("Button " + destinationPoint.name + " is used");

        //gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        ElevatorMovement.ButtonIsPressed(destinationPoint);
        
    }
}
