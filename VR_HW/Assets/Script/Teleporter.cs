using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Teleporter : NetworkBehaviour
{
    public GameObject dest = null;
    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {

        
        Debug.Log("onCollide " + collision.gameObject.name);
        //collision.collider.gameObject.transform.position = dest.transform.position + new Vector3(0, 0.5f, 0);

        teleport(collision.gameObject);
        

    }

   

    void teleport(GameObject target) {
        //target.GetComponent<Rigidbody>().MovePosition(dest.transform.position + new Vector3(0, 0.5f, 0));
        target.GetComponent<Rigidbody>().isKinematic = false;
        target.GetComponent<Rigidbody>().MovePosition(dest.transform.position + new Vector3(0, 0.8f, 0));
        

    }
    private void OnCollisionEnter(Collision collision)
    {

    }




}
