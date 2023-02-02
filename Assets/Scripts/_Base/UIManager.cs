using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }
    /* This script provides or mediates all UI operations of the game. */

    /*[Header("Menu")]
    [SerializeField] private GameObject menuObject;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject nextButton;*/

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject moneyImage;
    [SerializeField] private float moneyShakeValue;
    [SerializeField] private float moneyShakeDuration;



    [Header("Buttons")]
    [SerializeField] private Button[] gameButtons; // 0:Ball   , 1:Pin   , 2:Route  , 3:Merge  ,
    [SerializeField] private TextMeshProUGUI[] gameButtonTexts; // 0:Ball   , 1:Pin   , 2:Route  , 3:Merge


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
    }

    private void Start()
    {
        for (int i = 0; i < gameButtonTexts.Length; i++)
        {
            gameButtonTexts[i].text = GameManager.Instance.gameButtonPrices[i].ToString();
        }
    }
    private void Update()
    {
        UpdateMoneyText();
        UpdatePinText();
        UpdateBallText();
        UpdateMergeText();
        UpdateRouteText();
    }


    void UpdateMoneyText()
    {
        if(moneyText != null)
        {
            if (GameManager.Instance.gameMoney <= 1000)
            {
                moneyText.text = GameManager.Instance.gameMoney.ToString();
            }
            else if (GameManager.Instance.gameMoney >= 1000 && GameManager.Instance.gameMoney <= 1000000)
            {
                moneyText.text = (GameManager.Instance.gameMoney / 1000).ToString() + "K";
            }
            else if (GameManager.Instance.gameMoney >= 1000000 && GameManager.Instance.gameMoney <= 1000000000)
            {
                moneyText.text = (GameManager.Instance.gameMoney / 1000000).ToString() + "M";
            }
            else
            {
                moneyText.text = (GameManager.Instance.gameMoney / 1000000000).ToString() + "B";
            }
        }
  
    }

    void UpdatePinText()
    {
        if (GameManager.Instance.routeLevel == 0 && GameManager.Instance.engelCount == 4)
        {
            gameButtons[1].interactable = false;
            gameButtonTexts[1].text = "Maks";
        }
        else if (GameManager.Instance.routeLevel == 1 && GameManager.Instance.engelCount == 13)
        {
            gameButtons[1].interactable = false;
            gameButtonTexts[1].text = "Maks";
        }
        else if (GameManager.Instance.routeLevel == 2 && GameManager.Instance.engelCount == 22)
        {
            gameButtons[1].interactable = false;
            gameButtonTexts[1].text = "Maks";
        }
        else if(GameManager.Instance.gameButtonPrices[1] < GameManager.Instance.gameMoney)
        {
            gameButtons[1].interactable = true;
            gameButtonTexts[1].text = GameManager.Instance.gameButtonPrices[1].ToString();
        }
        else
        {
            gameButtons[1].interactable = false;
            gameButtonTexts[1].text = GameManager.Instance.gameButtonPrices[1].ToString();
        }
    }

    void UpdateBallText()
    {
        if (GameManager.Instance.routeLevel == 0 && GameManager.Instance.balls.Length == 8)
        {
            gameButtons[0].interactable = false;
            gameButtonTexts[0].text = "Maks";
        }
        else if (GameManager.Instance.routeLevel == 1 && GameManager.Instance.balls.Length == 16)
        {
            gameButtons[0].interactable = false;
            gameButtonTexts[0].text = "Maks";
        }
        else if (GameManager.Instance.routeLevel == 2 && GameManager.Instance.balls.Length == 30)
        {
            gameButtons[0].interactable = false;
            gameButtonTexts[0].text = "Maks";
        }
        else if (GameManager.Instance.gameButtonPrices[0] < GameManager.Instance.gameMoney)
        {
            gameButtons[0].interactable = true;
            gameButtonTexts[0].text = GameManager.Instance.gameButtonPrices[0].ToString();
        }
        else
        {
            gameButtons[0].interactable = false;
            gameButtonTexts[0].text = GameManager.Instance.gameButtonPrices[0].ToString();
        }
    }

    void UpdateMergeText()
    {
        if (GameManager.Instance.ballEqualLevel == 0)
        {
            gameButtons[3].interactable = false;
            gameButtonTexts[3].text = "None";
        }
        else if (GameManager.Instance.ballEqualLevel > 0 && GameManager.Instance.gameButtonPrices[3] < GameManager.Instance.gameMoney)
        {
            gameButtons[3].interactable = true;
            gameButtonTexts[3].text = GameManager.Instance.gameButtonPrices[3].ToString();
        }
        else
        {
            gameButtons[3].interactable = false;
            gameButtonTexts[3].text = GameManager.Instance.gameButtonPrices[3].ToString();
        }
    }

    void UpdateRouteText()
    {
        if (GameManager.Instance.routeLevel == 2)
        {
            gameButtons[2].interactable = false;
            gameButtonTexts[2].text = "Max";
        }
        else if (GameManager.Instance.gameButtonPrices[2] < GameManager.Instance.gameMoney)
        {
            gameButtons[2].interactable = true;
            gameButtonTexts[2].text = GameManager.Instance.gameButtonPrices[2].ToString();
        }
        else
        {
            gameButtons[2].interactable = false;
            gameButtonTexts[2].text = GameManager.Instance.gameButtonPrices[2].ToString();
        }
    }

    public void ShakeMoneyImage()
    {
        moneyImage.transform.DOShakeScale(moneyShakeDuration, moneyShakeValue).OnComplete(() => moneyImage.transform.DOScale(new Vector3(1,1,1),0.1f));
    }


}
