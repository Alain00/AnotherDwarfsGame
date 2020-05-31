using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSceneScript : MonoBehaviour
{
    public int sceneToLoad;
    public float time = 4;
    void Start(){
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene(){
        yield return new WaitForSeconds(time);
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
    }
}
