using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Camera _camera;
    [SerializeField] Transform target;

    private Vector3 previousPosition;
    private Vector3 translatePosition;


    void Awake()
    {
        translatePosition = _camera.transform.position;
    }

    void Update()
    {
        CameraMovement();
        ClickToPower();
    }

    void CameraMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = previousPosition - _camera.ScreenToViewportPoint(Input.mousePosition);

            _camera.transform.position = target.position; //new Vector3();

            // _camera.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            _camera.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
            _camera.transform.Translate(new Vector3(translatePosition.x,translatePosition.y-14,translatePosition.z));

            previousPosition = _camera.ScreenToViewportPoint(Input.mousePosition);

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
        GameManager.Instance.gameMoney += 100;
    }
}
