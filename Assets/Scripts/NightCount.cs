using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightCount : MonoBehaviour
{
    EnemiesController controller;
    public Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        controller = EnemiesController.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.resting){
            timeText.text = controller.GetCurrentRestingTime().ToString();
        }else{
            Destroy(gameObject);
        }
    }
}
