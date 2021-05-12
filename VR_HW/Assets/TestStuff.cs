using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class TestStuff : MonoBehaviour
{
    public Button enterNameButton = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Keyboard.current[Key.Enter].wasPressedThisFrame) {
            return;
        }
        enterNameButton.onClick.Invoke();
        Debug.Log("enter is presseed");

       
        
    }
}
