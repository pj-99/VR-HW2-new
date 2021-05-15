using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoxColliderOnclick : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collide with" + collision.gameObject.name);
        if (collision.gameObject.name == "Line")
        {
           gameObject.GetComponent<Button>().onClick.Invoke();
        }
    }
}
