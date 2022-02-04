using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensetivity = 200f;
    public Transform playerBody;
    public float rayRange = 5f;
    private float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        
        CastRay();
    }

    void CastRay()
    {
         RaycastHit hitInfo = new RaycastHit();
         bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, rayRange);
         if (hit)
         {
             GameObject hitObject = hitInfo.transform.gameObject;
             if (Input.GetKeyDown(KeyCode.E))
             {
                 hitObject.TryGetComponent(out IInteractable objectInteract);
                 if (objectInteract != null)
                 { 
                     objectInteract.Interact();
                 }
             }
         }

    }
}
