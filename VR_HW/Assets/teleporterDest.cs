using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporterDest : MonoBehaviour
{

    //目的地的Script，防止他亂動跑掉
    private void OnCollisionEnter(Collision collision)
    {

        collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log("dest board : set rigid body kinematic to flase then false");

    }
}
