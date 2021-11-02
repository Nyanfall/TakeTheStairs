using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SloanKelly.GameLib; //random dude coroutine library 
public class ElevatorMovement : MonoBehaviour
{
   // private bool onGroundFloor = true;

   //
   // private Vector3 endPosition; 
   // private Vector3 startPositon;
   //
   // private Vector3 _destination;
   //
   // public float destinationHeight;
   
   public float elevatorMoveDuration = 3f;
   public float elevatorWaitDelay = 1.5f;

   public int maxFloors = 4;
   public List <GameObject> destinations = new List<GameObject>();
   private GameObject destination;
   
   private bool running;

   public void ButtonIsPressed(GameObject _destination)
   {
      if (!destinations.Contains(_destination))
      {
         destinations.Add(_destination);
         Debug.Log(_destination.name + " added to array of floors");
      }
      destination = _destination;
      MoveElevator();
   }
   

   public void MoveElevator()
   {
      //if (running) return;
      //StartCoroutine(MoveElevator(buttonsPressed)); // error is somewhere here, rework onGroundFloor Logic to array of floors
   }
   
   /*private IEnumerator MoveElevator(GameObject[] buttonsPressed) //re-do path choosing here
   {
     //Vector3 start = onGroundCheck ? startPositon : endPosition; //make new conditions
      //Vector3 end = onGroundCheck ? endPosition : startPositon; //make new conditions
      
      running = true;

      return CoroutineFactory.Create(elevatorMoveDuration, time =>
      {
        // transform.position = Vector3.Lerp(start, end, time);
      }, () =>
      {
         running = false;
         
      });
   }*/
}
