using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
