using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detecter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collide !!");
    }
}
