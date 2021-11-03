using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SloanKelly.GameLib; //random dude coroutine library 
public class ElevatorMovement : MonoBehaviour
{
   public float elevatorMoveDuration = 5f;
   public float elevatorWaitDelay = 1.5f;

   public int maxFloors = 4;
   
   public List <GameObject> destinations = new List<GameObject>();

   private Vector3 fisrtFloorPosition;
   private Vector3 currentPositon;
   private GameObject destination;
   
   private bool running;

   public void Awake()
   {
      fisrtFloorPosition = transform.position;
      currentPositon = fisrtFloorPosition;
   }

   public void ButtonIsPressed(GameObject _destination, int floor)
   {
      if (!destinations.Contains(_destination))
      {
         destinations.Add(_destination);
         //Debug.Log(_destination.name + " added to array of floors");
      }
      MoveElevator();
   }

   public void MoveElevator() //move elevator here, remove destination from array
   {
      if (running) return;
      destination = destinations.First();
      StartCoroutine(MoveElevator(destination));
   }

   private IEnumerator MoveElevator(GameObject _destination) //re-do path choosing here
   {
      running = true;

      Vector3 start = new Vector3(fisrtFloorPosition.x, currentPositon.y, fisrtFloorPosition.z);
      Vector3 end = new Vector3(fisrtFloorPosition.x, _destination.transform.position.y, fisrtFloorPosition.z);
      
      return CoroutineFactory.Create(elevatorMoveDuration, time =>
      { 
        transform.position = Vector3.Lerp(start, end, time);
      }, () =>
      {
         destinations.Remove(_destination);
         running = false;
         currentPositon = new Vector3(fisrtFloorPosition.x,_destination.transform.position.y,fisrtFloorPosition.z);
      });
   }
}
