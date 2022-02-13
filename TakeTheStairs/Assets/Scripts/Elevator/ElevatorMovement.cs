using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Coroutine.randomdudelib;

public class ElevatorMovement : MonoBehaviour
{
   public float move1FloorDuration = 2.5f;
   public float waitDelay = 2f;

   public List<GameObject> AllFloors = new List<GameObject>();

   private float height;

   private List <GameObject> _destinations = new List<GameObject>();
   private List <int> floorsQue = new List<int>(); // is it need? 

   private Vector3 fisrtFloorPosition;
   private Vector3 currentPositon;
   private GameObject _destination;
   
   private bool running;

   public void Awake()
   {
      fisrtFloorPosition = transform.position;
      currentPositon = fisrtFloorPosition;
   }
   
   public void ButtonIsPressed(int floorNumber)
   {
      floorsQue.Add(floorNumber); // is it need? 
      GameObject nextFloor = AllFloors.ElementAt(floorNumber-1);
      
      if (!_destinations.Contains(nextFloor))
      {
         _destinations.Add(nextFloor);
         //Debug.Log(nextFloor.name + " added to array of floors");
      }
      if (nextFloor.transform.position.y > currentPositon.y)
      {
         _destinations = _destinations.OrderBy(go => go.transform.position.y).ToList();
         MoveElevator();
      }
      else if (nextFloor.transform.position.y < currentPositon.y)
      {
         _destinations = _destinations.OrderBy(go => go.transform.position.y).ToList();
         _destinations.Reverse();
         MoveElevator();
      }

   }

   public float GetHeight()
   {
      AllFloors.OrderBy(go => go.transform.position.y).ToList();

      foreach (var floor in AllFloors)
      {
         float allHeights;
         allHeights =+ floor.transform.position.y;
         height = allHeights / AllFloors.Count;
      }
      
      return height;
   }
   
   public void MoveElevator() 
   {
      if (running) return;
      StartCoroutine(MoveElevator(_destinations));
   }

   private IEnumerator MoveElevator(List<GameObject> destinations) 
   {
      running = true;
      _destination = destinations.First(); 
      destinations.Remove(_destination);

      Vector3 start = new Vector3(fisrtFloorPosition.x, currentPositon.y, fisrtFloorPosition.z);
      Vector3 end = new Vector3(fisrtFloorPosition.x, _destination.transform.position.y, fisrtFloorPosition.z);
      
      float dif = (end.y - start.y)/GetHeight();
      return CoroutineFactory.Create(move1FloorDuration*Math.Abs(dif), waitDelay, time =>
      {
         transform.position = Vector3.Lerp(start,end, time );
         //transform.position = Vector3.up * Time.deltaTime;
         
      }, () =>
         {
            //here doors are moving for future
         },
      () =>
      {
         running = false;
         currentPositon = new Vector3(fisrtFloorPosition.x,_destination.transform.position.y,fisrtFloorPosition.z); //update it with coroutine
         if (destinations.Count > 0)
         {
            StartCoroutine(MoveElevator(_destinations));
         }
      });
   }
}
