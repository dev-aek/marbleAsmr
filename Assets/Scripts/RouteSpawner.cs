using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform targetPosition;
    public GameObject routePrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddRoute()
    {
        GameObject newRoute = Instantiate(routePrefab, targetPosition.position, Quaternion.identity);
        targetPosition = newRoute.GetComponentInChildren<Transform>();
       // newRoute.transform.parent = targetPosition.parent.parent;
    }
}
