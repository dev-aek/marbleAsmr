using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float moveSpeed = 10f; // topun hareket hýzý

    private Rigidbody rb; // topun rigidbody'i

    void Start()
    {
        // topun rigidbody'ini al
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // topun hareketi için bir kuvvet uygula
        rb.AddForce(new Vector3(-moveSpeed, 0, 0));
       // rb.AddExplosionForce(100*moveSpeed * Time.deltaTime, transform.position, 1);
        rb.drag = 0.5f; // topun hareketini yumuþatmak için sürükleme deðeri
        

    }
}
