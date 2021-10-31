using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SloanKelly.GameLib; //random dude coroutine library 
public class ElevatorMovement : MonoBehaviour
{
   private bool onGroundFloor = true;
   private bool running;
   
   private Vector3 endPosition; 
   private Vector3 startPositon;
   private Vector3 _destination;

   public float destinationHeight;

   //public GameObject[] destinationArr;
   //public GameObject destination;
   
   public float duration = 3f;

   public void MoveElevator(Vector3 destination)
   {
      _destination = destination;
      if (running) return;
      StartCoroutine(MoveElevator(onGroundFloor));
   }
   private void Awake()
   {
      startPositon = transform.position;
      endPosition = startPositon + new Vector3(0,_destination.y,0); // Make here from current position
   }
   private IEnumerator MoveElevator(bool onGroundCheck)
   {
      Vector3 start = onGroundCheck ? startPositon : endPosition;
      Vector3 end = onGroundCheck ? endPosition : startPositon;
      
      running = true;

      return CoroutineFactory.Create(duration, time =>
      {
         transform.position = Vector3.Lerp(start, end, time);
      }, () =>
      {
         running = false;
         onGroundFloor = !onGroundFloor;
      });
   }
}
