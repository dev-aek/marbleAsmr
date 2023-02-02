using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour
{
    public int balLevel = 1;
    public float moneyValue = 1;
    public Material[] materials; // 0:White , 1:Blue , 2:Pembe , 3: Mor, 4:Yeþil

    void Start()
    {

    }

    void Update()
    {
        switch (balLevel)
        {

            case 1:
                moneyValue= 1;
                for(int i = 0;i < gameObject.GetComponent<Renderer>().materials.Length; i++)
                {

                    gameObject.GetComponent<Renderer>().materials[i].color = materials[0].color;
                }
                break;
            case 2:
                moneyValue = 5;
                for (int i = 0; i < gameObject.GetComponent<Renderer>().materials.Length; i++)
                {

                    gameObject.GetComponent<Renderer>().materials[i].color = materials[0].color;
                }
                gameObject.GetComponent<Renderer>().materials[3].color = materials[1].color;
                gameObject.GetComponent<Renderer>().materials[2].color = materials[1].color;

                break;
            case 3:
                moneyValue = 10;
                for (int i = 0; i < gameObject.GetComponent<Renderer>().materials.Length; i++)
                {

                    gameObject.GetComponent<Renderer>().materials[i].color = materials[0].color;
                }
                gameObject.GetComponent<Renderer>().materials[2].color = materials[2].color;
                break;
                case 4:
                for (int i = 0; i < gameObject.GetComponent<Renderer>().materials.Length; i++)
                {

                    gameObject.GetComponent<Renderer>().materials[i].color = materials[0].color;
                }
                gameObject.GetComponent<Renderer>().materials[1].color = materials[3].color;

                moneyValue = 50;
                break;
                default:
                moneyValue = 1;
                break;

        }


        transform.RotateAroundLocal(new Vector3(90, 90, 90), .1f);
        ClickToPower();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "StartPoint")
        {
            GameManager.Instance.availableSpawn = false;

        }

        if (other.tag == "StartPoint2")
        {
            GameManager.Instance.availableSpawn = true;

        }
    }



    IEnumerator NextSpawnWait()
    {
        GameManager.Instance.availableSpawn = false;
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.availableSpawn = true;

    }

    void ClickToPower()
    {
        

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            if(gameObject.GetComponent<TrailRenderer>().enabled == false)
            StartCoroutine(ClickWaitEffect());
        }
    }
    IEnumerator ClickWaitEffect()
    {
        DOTween.timeScale = 1.3f;
        gameObject.GetComponent<TrailRenderer>().enabled = true;
        //GameManager.Instance.moveSpeed *= 3/4f;
        moneyValue *= 2;
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<TrailRenderer>().enabled = false;
        //GameManager.Instance.moveSpeed *= 4/3f;
        moneyValue *= 1/2;
        DOTween.timeScale = 1f;

    }
}
