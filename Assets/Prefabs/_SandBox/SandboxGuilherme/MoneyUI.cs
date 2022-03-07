using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class MoneyUI : MonoBehaviour
{
    public Text Cash;

    GameManager manager;

    private void Start()
    {
        manager = GameManager.instance;
    }

    private void Update()
    {
        Cash.text =Convert.ToString(manager.Cash) + "£";
    }
}
