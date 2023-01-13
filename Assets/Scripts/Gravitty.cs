using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitty : MonoBehaviour
{
    public float gravityValue = 10f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * rb.mass*gravityValue);
    }



}
