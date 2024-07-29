using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public int money;
    public TextMeshProUGUI moneyText;
    
    void Start()
    {
        moneyText.text="Money : "+money;
    }
    public void EarnMoney(int amount)
    {
        money += amount;
        moneyText.text="Money : "+money;
    }

    public void SpendMoney(int amount)
    {
        money -= amount;
        moneyText.text="Money : "+money;
    }
}
