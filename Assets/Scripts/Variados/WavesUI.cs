using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesUI : MonoBehaviour
{
    GameObject OnWaveBeginsText;
    GameObject OnWaveCompletedText;
    void Start()
    {
        OnWaveBeginsText = GameObject.Find("OnWaveBeginsText");
        OnWaveCompletedText = GameObject.Find("OnWaveCompletedText");
        OnWaveCompletedText.SetActive(false);
        OnWaveBeginsText.SetActive(false);
        
        EnemiesController.main.OnWaveBegins += LaunchStartUI;
        EnemiesController.main.OnWaveCompleted += LaunchCompletedUI;
    }
    void LaunchStartUI(){
       OnWaveBeginsText.SetActive(true);
       StartCoroutine(CloseAfterTime(OnWaveBeginsText));
    }
    void LaunchCompletedUI(){
        OnWaveCompletedText.SetActive(true);
        StartCoroutine(CloseAfterTime(OnWaveCompletedText));
    }
    IEnumerator CloseAfterTime( GameObject ToClose){
        yield return new WaitForSeconds(3f);
        ToClose.SetActive(false);
    }

   
}
