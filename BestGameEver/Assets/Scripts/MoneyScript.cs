using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    static public int earnedMoneyStatic = 0;
    public int earnedMoney = 0;

    public int GetMoney()
    {
        earnedMoney = earnedMoneyStatic;
        return earnedMoney;
    }

    public void AddCoin()
    {       
        earnedMoneyStatic++;
        Debug.Log(earnedMoneyStatic.ToString());
    }

    public void EraseMoney(int erase)
    {
        earnedMoneyStatic -= erase;     
    }
}
