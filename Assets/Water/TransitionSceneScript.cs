using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSceneScript : MonoBehaviour
{
    public int sceneToLoad;
    public float time = 4;
    public Animator animator;
    void Start(){
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene(){
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);
        async.allowSceneActivation = false;
        yield return new WaitForSeconds(time);
        while(async.isDone != true){
            if(async.progress == .9f){
                animator.SetBool("EnterTrans",false);
                yield return new WaitForSeconds(time);
                async.allowSceneActivation = true;
                }
        yield return null;
        }
        
    }
}
