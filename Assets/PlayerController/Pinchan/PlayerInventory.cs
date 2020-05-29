using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInventory : MonoBehaviour
{
    
    public int Money;
    public Text MoneyField;
    void Start()
    {
        MoneyField = GameObject.Find("MoneyText").GetComponent<Text>();
    }
    void Update()
    {
        MoneyField.text = Money.ToString();
    }
}
