using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    /* The script that covers most of the game management of the game. */

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    /*public enum GameState //Game States
    {
        BeforeStart,
        Normal,
        Failed,
        Final,
        Victory,
        Pause
    }
    [Header("Theme Settings")]
    [SerializeField] private bool showMenuOnNewSceneLoaded = false;
    [SerializeField] private bool giveInputOnFirstClick = false;
    [SerializeField] private bool takeInputCount = false;
    [SerializeField] private bool giveInputToUser = false;
    [SerializeField] private bool isDebug = true;


    public bool GiveInputToUser { get { return giveInputToUser; } set { giveInputToUser = value; } }
    public bool IsDebug { get { return isDebug; } }
    public bool GiveInputOnFirstClick { get { return giveInputOnFirstClick; } }
    public bool TakeInputCount { get { return takeInputCount; } }
    public bool ShowMenuOnNewSceneLoaded { get { return showMenuOnNewSceneLoaded; } } */


    /*
    public static Action onWinEvent;
    public static Action onLoseEvent;
    */

    [Header("Game Settings")]
    public float gameMoney;
    public float moveSpeed = 5f;
    public GameObject topPrefab;
    public Transform topSpawnPoint;
    public int routeLevel;
    public GameObject[] routeObjects;
    public GameObject[] routeEndObjects;
    public GameObject[] routePObjects;
    public int engelCount=1;
    public float[] gameButtonPrices; // 0:Ball   , 1:Pin   , 2:Route  , 3:Merge
    public float ballCount =1;
    public Transform iconTransform;
    public GameObject[] balls;
    public int ballEqualLevel;
    public Transform mergeTarget;





    [Header("Paths")]
    public GameObject[] pathObjects;
    public GameObject[] pathObjects1;
    public GameObject[] pathObjects2;
    public GameObject[] pathObjects3;
    public Vector3[] movePathPoints;
    public GameObject[] engelObjects;
    public bool availableSpawn = true; 




    //public GameObject cameraObject;


    /*
    public GameState currentState = GameState.BeforeStart;
    public const float MAX_X = 90f;
    public const float MIN_X = -90f;
    public float playerSmooth = 8f;
    public float horizontalSpeed = 100f;
    public float forwardSpeed = 10f;
    public Transform resetRotation;
    public GameObject stackableCoin;
    public float explosionForce = 20f;
    public TextMeshProUGUI bonnusText;*/


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;
        //PlayerPrefs.GetFloat("Route Level", routeLevel);
        ChangePathPoints();

    }

    private void Start()
    {
    }

    public void SpawnBall()
    {
        gameMoney -= gameButtonPrices[0];
        Instantiate(topPrefab,topSpawnPoint.position,Quaternion.identity);
        ballCount++;
        gameButtonPrices[0] += (ballCount/4) * 12 * (routeLevel+1) ;
    }

    private void FixedUpdate()
    {
        balls = GameObject.FindGameObjectsWithTag("Ball");
        ExecuteBallEquals();

    }

    public void AddRoute()
    {
        routeLevel++;
        routeObjects[routeLevel-1].active = true;
        routeEndObjects[routeLevel - 1].active = false;
        routePObjects[routeLevel - 1].active = true;
        StartCoroutine(NextRouteWait());
        


    }
    void AwakePoints(GameObject[] pathObjects)
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
            GameObject gameObject = pathObjects[i];
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
    }

    public void ChangePathPoints()
    {

        switch (GameManager.Instance.routeLevel)
        {
            case 0:
                AwakePoints(GameManager.Instance.pathObjects);
                break;
            case 1:
                AwakePoints(GameManager.Instance.pathObjects1);
                moveSpeed *= 3;
                break;
            case 2:
                AwakePoints(GameManager.Instance.pathObjects2);
                moveSpeed *= 3/2;
                break;
            case 3:
                AwakePoints(GameManager.Instance.pathObjects3);
                moveSpeed *= 3/2;
                break;
            default:
                print("Incorrect route level.");
                break;
        }


    }
    IEnumerator NextRouteWait()
    {

        //Print the time of when the function is first called.
        yield return new WaitForSeconds(moveSpeed);
        routePObjects[routeLevel - 1].active = false;
        //After we have waited 5 seconds print the time again.

    }

    void AddEngelObject()
    {
        engelObjects[engelCount].active = true;
        engelCount++;
    }

    public void AddPin()
    {
        gameMoney -= gameButtonPrices[1];
        gameButtonPrices[1] += 15;
        AddEngelObject();
    }

    IEnumerator NextSpawnWait()
    {
        GameManager.Instance.availableSpawn = false;
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.availableSpawn = true;

    }

    public Vector3 GetIconPosition(Vector3 target)
    {
        Vector3 uiPos = iconTransform.position;
        uiPos.z = (target - Camera.main.transform.position).z;
        //uiPos.x -= 2f;
        Vector3 result =Camera.main.ScreenToWorldPoint(uiPos);

        return result;
    }

    public void MergeBalls()
    {
        int a = 0;
        foreach (GameObject ball in balls)
        {
            int b = ballEqualLevel;
            if (ball.GetComponent<BallController>().balLevel == ballEqualLevel)
            {
                a++;
                ball.transform.DOMove(mergeTarget.position, 1).OnComplete(() => MergeBallAnimation(ball));
            }

            if(a >= 3)
            {
                StartCoroutine(SpawnMergeObject(b));
                break;
            }
        }

        gameMoney -= gameButtonPrices[3];
        gameButtonPrices[3] += (ballCount / 4) * 60 * (routeLevel + 1);


    }

    void MergeBallAnimation(GameObject target)
    {
        Destroy(target);
       // GameObject ball = Instantiate(topPrefab,mergeTarget.transform);
       // ball.transform.DOShakePosition(0.5f, 0.5f).OnComplete(() => Destroy(ball));

    }

    void ExecuteBallEquals()
    {
        for (int i = 1; i < 5; i++)
        {
            int a=0;
            foreach(GameObject ball in balls)
            {
                if(ball.GetComponent<BallController>().balLevel == i)
                {
                   a++;

                }
            }
            if(a >=3)
            {
                ballEqualLevel = i;
                print(ballEqualLevel);
                break;
            }
            else
            {
                ballEqualLevel = 0;
            }
        }
    }

    IEnumerator SpawnMergeObject(int b)
    {
        yield return new WaitForSeconds(1.1f);
        GameObject ballObject = Instantiate(topPrefab, mergeTarget.position, Quaternion.identity);
        ballObject.GetComponent<BallController>().balLevel = b + 1;
    }

    public void HileMoney()
    {
        gameMoney += 2000;
    }
}
