using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorDetect : MonoBehaviour
{
    public GameObject door1 = null;
    public GameObject door2 = null;
    public GameObject door3 = null;
    public GameObject door4 = null;

    private Animation doorLeft = null;
    private Animation doorRight = null;

    private Animation doorLeft2 = null;
    private Animation doorRight2 = null;
    private bool isUsed = false;
    public float radius = 1f;
    // Start is called before the first frame update
    void Start()
    {
        doorLeft = door1.GetComponent<Animation>();
        doorRight = door2.GetComponent<Animation>();
        doorLeft2 = door3.GetComponent<Animation>();
        doorRight2 = door4.GetComponent<Animation>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
    // Update is called once per frame
    void Update()
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position,radius);
        foreach (var hitCollider in hitColliders)
        {
            
            if (hitCollider.gameObject.tag == "Player" && !isUsed){
                Debug.Log(hitCollider.gameObject.name + " this one hit me!");
                doorLeft.Play();
                doorRight.Play();
                //doorLeft2.Play();
                //doorRight2.Play();
                isUsed = true;
                break;
            }


        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "is trigger ! ");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Sphere") {
            doorLeft2.Play();
            doorRight2.Play();
        }
        Debug.Log(collision.gameObject.name + "is Collide ! ");
    }

}
