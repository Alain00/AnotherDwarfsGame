using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public Animator animator;
    public GameObject brother;
    EnemiesController controller;

    void Start(){
        Vector3 point = new Vector3(0,50,0) + transform.position;
        Vector3 brotherPoint = point + new Vector3(0,0, 10);
        RaycastHit hit;
        if (Physics.Raycast(point, -transform.up, out hit)){
            transform.position = hit.point;
        }

        if (Physics.Raycast(brotherPoint, -transform.up, out hit)){
            brother.transform.position = hit.point;
        }

        controller = EnemiesController.main;
        controller.OnWaveBegins += Leave;
        controller.OnWaveCompleted += Arrive;
    }

    void Arrive(){
        animator.SetBool("onland", true);
    }

    void Leave(){
        animator.SetBool("onland", false);
        HideBrother();
    }

    public void ShowBrother(){
        brother.SetActive(true);
    }

    public void HideBrother(){
        brother.SetActive(false);
    }
}
