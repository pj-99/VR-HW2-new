﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUIRaycaster : MonoBehaviour
{
    public float distance = 10f;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward * distance + transform.position);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.forward * 5f + transform.position);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            lineRenderer.SetPosition(1, hit.point);
            Debug.Log("now line detect : " + hit.collider.gameObject.name);
            if(hit.collider.gameObject.tag == "Button")
            {
                if(Input.GetMouseButtonDown(0))
                    hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
