using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Teleporter : NetworkBehaviour
{
    public GameObject dest = null;

    //傳送出發點的script
    private void OnCollisionStay(Collision collision)
    {   
        Debug.Log("onCollide " + collision.gameObject.name);
        teleport(collision.gameObject);
        
    }
    void teleport(GameObject target) {
        target.GetComponent<Rigidbody>().isKinematic = false;
        target.GetComponent<Rigidbody>().MovePosition(dest.transform.position + new Vector3(0, 0.8f, 0));   
    }





}
