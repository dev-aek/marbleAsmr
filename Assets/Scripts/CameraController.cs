using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{

    //[SerializeField] Camera _camera;
    [SerializeField] GameObject target;
    public float speed = 5;
    public float routeMSpeed = 4;


    float minFov = 35f;
    float maxFov = 100f;
    float sensitivity = 17f;


    void Update()
    {
        CameraMovement();
        ClickToPower();
    }

    void CameraMovement()
    {
       /* if (Input.GetMouseButtonDown(0))
        {
            previousPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
        }*/

        if (Input.GetMouseButton(0))
        {

            transform.RotateAround(target.transform.position, Vector3.up, Input.GetAxis("Mouse X") * speed);
            
            //transform.RotateAround(target.transform.position, transform.right, Input.GetAxis("Mouse Y") * speed);

        }
    }

    void ClickToPower()
    {
         if (Input.GetMouseButtonDown(0))
        {
            ClickEffect();
        }
    }

    public void ClickEffect()
    {
        //GameManager.Instance.gameMoney += 100;
    }

    public void CameraRouteMovemenent()
    {
        transform.DOMoveY(transform.position.y- routeMSpeed, 1);
        transform.DOLocalMoveZ(transform.position.z - routeMSpeed, 1);
    }
}
