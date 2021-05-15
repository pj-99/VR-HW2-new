using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Mirror;
using TMPro;
using UnityEngine.InputSystem;

public class LineRaycaster : NetworkBehaviour
{
    [SerializeField] private float distance = 20f;
    [SerializeField] private Transform rightHand;
    private LineRenderer lineRenderer;
    private GameObject wall1, wall2, wall3, wall4;//side of the box
    
    [SyncVar]
    private bool correctness = false;//id get correct answer, open the box
    [SyncVar]
    private GameObject moving_object;//set target object

    //set correctness and moving_object on server
    [Server]
    private void setCorrect() {
        correctness = true;
        Debug.Log("setCorrect()");
    }

    [Server]
    private void setMovingObject(GameObject target) {
        Debug.Log(" [server ] setMovingObject");
        moving_object = target;
        Debug.Log("[server ] now moving object is : " + moving_object);

    }


    private void Start()
    {
        //get lineRenderer, wall1, wall2, wall3, wall4
        lineRenderer = GetComponentInChildren<LineRenderer>();
        wall1 = GameObject.Find("Box/Cube (2)");
        wall2 = GameObject.Find("Box/Cube (3)");
        wall3 = GameObject.Find("Box/Cube (4)");
        wall4 = GameObject.Find("Box/Cube (5)");
    }

    [Client]
    private void Update()
    {
        //get player authority to call command, check Chat(Mirror Example) -> ChatWindow.cs -> OnSend()

        //if press key G, call command hold object, else if key up, call command release object
        if (hasAuthority)
        {

            if (Keyboard.current.gKey.isPressed && moving_object != null) 
            {
                Debug.Log("in update want to hold");
               
                cmd_GetHold(moving_object);
            }
            else if (Keyboard.current.gKey.wasReleasedThisFrame && moving_object!=null) cmd_Release(moving_object);


        Ray ray = new Ray(rightHand.position, rightHand.forward * distance + rightHand.position);
        lineRenderer.SetPosition(0, rightHand.position);
        lineRenderer.SetPosition(1, rightHand.forward * 5f + rightHand.position);
       

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) )
        {
            lineRenderer.SetPosition(1, hit.point);
                
            Debug.Log("hit at " + hit.collider.gameObject.name);
               

            if (hit.collider.gameObject.transform.root.name == "Interactable" && Keyboard.current.gKey.wasPressedThisFrame)
            {

                Debug.Log("in update, call cmd_set_target: "+ hit.collider.gameObject.name  );

                cmd_set_target(hit.collider.gameObject);

            }
            else if (hit.collider.transform.root.name == "Choices" && Keyboard.current.gKey.wasPressedThisFrame)
            {
          

                Debug.Log("pointin at !"+ hit.collider.transform.GetChild(0).gameObject.name );
                if (hit.collider.transform.GetChild(0).gameObject.name == "Canvas (4)")
                {
                    cmd_open();
                    Debug.Log("correct!");
                }

            }
            //if collider object is interactable object, call command to set moving_object to collider gameobject
            //else if the object is those choices and get correct answer, call command to set correctness
        }
            
        }
        if (correctness)//open the box
        {
            if (wall1.transform.localScale.z > 0.1f)
            {
                wall1.transform.localScale -= new Vector3(0f, 0f, 0.1f);
                wall1.transform.position += new Vector3(0f, 0.05f, 0f);

                wall2.transform.localScale -= new Vector3(0f, 0f, 0.1f);
                wall2.transform.position += new Vector3(0f, 0.05f, 0f);

                wall3.transform.localScale -= new Vector3(0f, 0f, 0.1f);
                wall3.transform.position += new Vector3(0f, 0.05f, 0f);

                wall4.transform.localScale -= new Vector3(0f, 0f, 0.1f);
                wall4.transform.position += new Vector3(0f, 0.05f, 0f);
            }
        }
        ///if (moving_object != null) Debug.Log("now moving object : " + moving_object.name);
        ///else { Debug.Log("now has no moving object!!!!!!!!!"); }
    }

    [Command]//set gameobject's kinematic to true and call clientRpc to update gamobject position
    private void cmd_GetHold(GameObject collider)
    {

        if (collider == null) return;
        Debug.Log("cmd_GetHold()");
        Rigidbody rb = collider.GetComponent<Rigidbody>();
        rb.isKinematic = true;

        Move_Object(transform.position + transform.forward*2f);
    }
    
    [Command]//set gameobject's kinematic to false and call command to set moving_object to null
    private void cmd_Release(GameObject collider)

    {
        Debug.Log("cmd_release");
        collider.GetComponent<Rigidbody>().isKinematic = false;

        setMovingObject(null);
        
    }
    
    [Command]//call server function to change correctness value
    public void cmd_open()
    {
        Debug.Log("cmd_open()");
        setCorrect();
    }

    [Command]//call server to set moving_object
    public void cmd_set_target(GameObject gameObject)
    {
        Debug.Log("cmd_set_target()");
        setMovingObject(gameObject);
    }

    [ClientRpc]//Update moving_object's position
    private void Move_Object(Vector3 dest) {
        if (moving_object == null) return;
        moving_object.GetComponent<Rigidbody>().MovePosition(dest);
    }
}
