using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effector3d : MonoBehaviour
{
    public float radius;
    public float force;


    void Update()
    {
        Vector3 effectorPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(effectorPos, radius);

        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(force * Time.deltaTime, effectorPos, radius);
            }
        }
    }
}
