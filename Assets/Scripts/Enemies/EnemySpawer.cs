using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawer : MonoBehaviour
{
    public EnemyAI[] enemiesPrefabs;
    [HideInInspector()]
    public bool isReady = true;
    float nexSpaw = 0;

    public delegate void OnEnemySpaw(EnemyAI enemyAI);
    public event OnEnemySpaw onEnemySpaw;


    void Start(){
        if (!EnemiesController.main) return;
        EnemiesController.main.RegisterSpawer(this);
    }

    public void OrderSpaw(float time){
        if (!isReady) return;
        nexSpaw = time;
        StartCoroutine(ExecuteSpaw());
        isReady = false;
    }

    IEnumerator ExecuteSpaw(){
        if (EnemiesController.main.resting) yield return null;
        yield return new WaitForSeconds(nexSpaw);
        int prefabIndex = Random.Range(0, enemiesPrefabs.Length);
        EnemyAI newEnemy = Instantiate(enemiesPrefabs[prefabIndex], transform.position, Quaternion.identity);
        onEnemySpaw(newEnemy);
        isReady = true;
    }
}
