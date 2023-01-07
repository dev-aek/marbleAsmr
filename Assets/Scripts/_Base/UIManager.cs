using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{

    /* This script provides or mediates all UI operations of the game. */

    /*[Header("Menu")]
    [SerializeField] private GameObject menuObject;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject nextButton;*/

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Update()
    {
        UpdateMoneyText();
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


}
