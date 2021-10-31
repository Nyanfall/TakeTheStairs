using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : MonoBehaviour, IInteractable
{
    public GameObject destinationPoint;

    public void Interact()
    {
        Debug.Log("Button " + destinationPoint.name + " is used");

        //gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        var ElevatorMovement = new ElevatorMovement();
        ElevatorMovement.MoveElevator(destinationPoint.transform.position);
    }
}
