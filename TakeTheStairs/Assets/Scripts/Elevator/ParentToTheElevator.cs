using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentToTheElevator : MonoBehaviour
{
    public Transform parentObject;
    public GameObject player;
    public void OnTriggerEnter(Collider other)
    {
         Debug.Log("Entered to the elevator");
         player.transform.SetParent(parentObject);
        
    } 
    public void OnTriggerExit(Collider other)
    {
         Debug.Log("Exited from the elevator");
         player.transform.SetParent(null);
        
    }
}
