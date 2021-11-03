using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SloanKelly.GameLib;
using UnityEngine.Serialization; //random dude coroutine library 
public class ElevatorMovement : MonoBehaviour
{
   public float move1FloorDuration = 2.5f;
   public float waitDelay = 2f;
   public float floorHeight = 5f;
   
   /*private*/ public List <GameObject> _destinations = new List<GameObject>();

   private Vector3 fisrtFloorPosition;
   private Vector3 currentPositon;
   private GameObject _destination;
   
   private bool running;

   public void Awake()
   {
      fisrtFloorPosition = transform.position;
      currentPositon = fisrtFloorPosition;
   }
   
   public void ButtonIsPressed(GameObject destination)
   {
      if (!_destinations.Contains(destination))
      {
         _destinations.Add(destination);
         //Debug.Log(_destination.name + " added to array of floors");
      }
      if (destination.transform.position.y > currentPositon.y) //current position lie sometimes, need make something in-between
      {
         _destinations = _destinations.OrderBy(go => go.transform.position.y).ToList();
         MoveElevator();
      }
      else if (destination.transform.position.y < currentPositon.y)
      {
         _destinations = _destinations.OrderBy(go => go.transform.position.y).ToList();
         _destinations.Reverse();
         MoveElevator();
      }
   }
   
   public void MoveElevator() 
   {
      if (running) return;
      StartCoroutine(MoveElevator(_destinations));
   }

   private IEnumerator MoveElevator(List<GameObject> destinations) 
   {
      running = true;
      _destination = destinations.First(); //todo: 
      destinations.Remove(_destination);

      Vector3 start = new Vector3(fisrtFloorPosition.x, currentPositon.y, fisrtFloorPosition.z);
      Vector3 end = new Vector3(fisrtFloorPosition.x, _destination.transform.position.y, fisrtFloorPosition.z);
      
      float dif = (end.y - start.y)/floorHeight;
      return CoroutineFactory.Create(move1FloorDuration*Math.Abs(dif), waitDelay, time =>
      { 
        transform.position = Vector3.Lerp(start,end, time);
      }, () =>
      {
         running = false;
         currentPositon = new Vector3(fisrtFloorPosition.x,_destination.transform.position.y,fisrtFloorPosition.z);
         if (destinations.Count > 0)
         {
            MoveElevator();
         }
      });
   }
}
