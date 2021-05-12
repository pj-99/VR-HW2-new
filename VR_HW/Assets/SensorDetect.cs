using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorDetect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "is trigger ! ");
    }
    private void On(Collision collision)
    {
        Debug.Log(collision.gameObject.name + "is Collide ! ");
    }
}
