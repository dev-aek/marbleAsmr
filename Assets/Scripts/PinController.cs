using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PinController : MonoBehaviour
{
    public GameObject moneyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "Ball")
        {
            GameManager.Instance.gameMoney += other.GetComponent<BallController>().moneyValue;

            GameObject money = Instantiate(moneyPrefab, transform.GetChild(0).transform.position,Quaternion.identity);
            Vector3 targetPos = GameManager.Instance.GetIconPosition(money.transform.position);
            targetPos.x -= 2f;

            money.transform.DOMove(targetPos, 1f).OnComplete(() => UIManager.Instance.ShakeMoneyImage());
            //money.transform.position = Vector3.Lerp(money.transform.position, targetPos, Time.deltaTime * 1f);
            StartCoroutine(DestroWait(money));   
        }
    }
    IEnumerator DestroWait(GameObject target)
    {
        yield return new WaitForSeconds(1f);
        Destroy(target);
    }

    }
