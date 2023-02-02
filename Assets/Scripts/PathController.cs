using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PathController : MonoBehaviour
{

    //[SerializeField] Transform[] movePath;
    //[SerializeField] GameObject[] pathObjects;
    //[SerializeField] float moveSpeed = 5f;
    GameObject[] pathObjectsCopy;

    Vector3[] movePathPoints;
    




    private void Awake()
    {

        


    }

    void Start()
    {
        transform.DOShakePosition(0.5f,.2f).OnComplete(() => DoOnMovePath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3[] GetMovePathPoints()
    {
        return GameManager.Instance.movePathPoints;
    }

    void DoOnMovePath()
    {
        // print(movePathPoints.Length);
        transform.DOPath(GetMovePathPoints(), GameManager.Instance.moveSpeed, PathType.CatmullRom, PathMode.Full3D).OnComplete(() => DoOnMovePath());
    }
    IEnumerator NextSpawnWait()
    {
        if (GameManager.Instance.availableSpawn)
        {
            DoOnMovePath();
        }
        else if(!GameManager.Instance.availableSpawn)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(NextSpawnWait());
        }

    }

    /*  void AwakePoints(GameObject[] pathObjects)
      {
          // int movePathLeng = pathObjects[objectCount].transform.childCount * pathObjects.Length ;

          int movePathLeng = 0;
          for (int i = 0; i < pathObjects.Length; i++)
          {
              movePathLeng += pathObjects[i].transform.childCount;
          }

          movePathPoints = new Vector3[movePathLeng];
          int iCount = 0;


          for (int i = 0; i < pathObjects.Length; i++)
          {
              GameObject gameObject= pathObjects[i];
              int cCount = gameObject.transform.childCount;
              for (int j = 0; j < cCount; j++)
              {

                  movePathPoints[iCount] = pathObjects[i].transform.GetChild(j).position;
                  iCount++;
              }
              //Debug.Log(movePathPoints[i]);
          }

         /* for(int i = 0; i < movePathPoints.Length; i++)
          {
              moveAllPathPoints[moveAllPathPoints.Length] = movePathPoints[i];
          }*/
    // }
    /*
        public void ChangePathPoints()
        {

            switch (GameManager.Instance.routeLevel)
            {
                case 0:
                    AwakePoints(GameManager.Instance.pathObjects);
                    break;
                case 1:
                    AwakePoints(GameManager.Instance.pathObjects1);
                    break;
                case 2:
                    AwakePoints(GameManager.Instance.pathObjects2);
                    break;
                case 3:
                    AwakePoints(GameManager.Instance.pathObjects3);
                    break;
                default:
                    print("Incorrect route level.");
                    break;
            }

        } */
}
