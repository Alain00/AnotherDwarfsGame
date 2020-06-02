using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCanvas : MonoBehaviour
{
    StoreManager Manager;
    void Awake()
    {
        Manager = GameObject.FindObjectOfType<StoreManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Manager.gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
