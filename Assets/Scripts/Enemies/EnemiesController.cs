using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public static EnemiesController main;
    public float waveTime = 120;
    public float restTime = 30;
    public int maxEnemiesCountBase = 10;
    public float enemiesSpawDelayBase = 2;
    public float enemiesSpawDelayVariation = 3;
    [Header("Per Wave Settings")]
    public Vector2 increaseWaveTimeRange = new Vector2(10, 15);
    public float enemiesSpawDecreaseFraction = 3;
    
    public Vector2 increaseEnemiesHealthRange = new Vector2(0,0);

    public bool debug;
    
    int waveCount = 0;
    List<EnemySpawer> spawers = new List<EnemySpawer>();
    int enemiesSpawed = 0;
    int enemiesAlive = 0;
    float timeToNextWave = 0;
    Transform playerTransform;

    void Awake(){
        main = this;
    }

    void Start(){
        timeToNextWave = Time.time + restTime;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update(){
        if (Time.time > timeToNextWave){
            if (enemiesAlive > 0) return;
            NextWave();
        }else{
            UpdateWave();
        }
    }

    void UpdateWave(){
        foreach (EnemySpawer spawer in spawers){
            if (!spawer.isReady){
                continue;
            }
            float time = Random.Range(enemiesSpawDelayBase, enemiesSpawDelayBase + enemiesSpawDelayVariation);
            
            spawer.OrderSpaw(time);
        }
    }

    void NextWave(){
        waveCount++;
        
        timeToNextWave = Time.time + waveTime
            + restTime;
        waveTime += Random.Range(increaseWaveTimeRange.x, increaseWaveTimeRange.y);
        enemiesSpawDelayBase /= enemiesSpawDecreaseFraction;
        enemiesSpawDelayVariation /= enemiesSpawDecreaseFraction;
    }

    public void RegisterSpawer(EnemySpawer spawer){
        spawers.Add(spawer);
        spawer.onEnemySpaw += HandleEnemySpaw;
    }

    void HandleEnemySpaw(EnemyAI enemyAI){
        enemiesSpawed++;
        enemiesAlive++;
        enemyAI.onDied += HandleEnemyDied;
        enemyAI.target = playerTransform;
    }

    void HandleEnemyDied(){
        enemiesAlive--;
    }

    void OnGUI(){
        if (!debug) return;

        GUILayout.Label("Wave Count: " + waveCount);
        GUILayout.Label("Time to next Wave: " + (timeToNextWave - Time.time).ToString());
        GUILayout.Label("Spawers Count: " + spawers.Count);
        GUILayout.Label("Enemies Spawed: " + enemiesSpawed);
        GUILayout.Label("Enemies Alive: " + enemiesAlive);
    }
}
