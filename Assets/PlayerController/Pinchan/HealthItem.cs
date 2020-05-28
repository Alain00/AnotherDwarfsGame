using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public Vector2 moneyMinMax = new Vector2(100, 200);
    void Start(){
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        PlayerInventory pi = gameManager.GetComponent<PlayerInventory>();
        pi.Money += Random.Range((int)moneyMinMax.x, (int)moneyMinMax.y);
        Destroy(this.gameObject);
    }
}
