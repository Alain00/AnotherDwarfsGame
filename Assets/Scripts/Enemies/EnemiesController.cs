using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public static EnemiesController main;
    public int wavesCount = 5;
    public float waveTime = 120;
    public float restTime = 30;
    public int maxEnemiesCountBase = 10;
    public float enemiesSpawDelayBase = 2;
    public float enemiesSpawDelayVariation = 3;
    [Header("Per Wave Settings")]
    public Vector2 increaseWaveTimeRange = new Vector2(10, 15);
    public float enemiesSpawDecreaseFraction = 3;
    public Vector2 increaseEnemiesHealthRange = new Vector2(0,0);
    [Header("Debug")]
    public int aproxSpawersCount = 0;
    [ReadOnly] public int[] aproxEnemiesCountPerWave;
    public bool debug;
    
    int waveProgress = 0;
    List<EnemySpawer> spawers = new List<EnemySpawer>();
    int enemiesSpawed = 0;
    int enemiesAlive = 0;
    float timeToNextWave = 0;
    bool resting = false;
    Transform playerTransform;

    void Awake(){
        main = this;
    }

    void OnValidate(){
        // Debug.Log("Hola");
        aproxEnemiesCountPerWave = new int[wavesCount];
        float delay = enemiesSpawDelayBase;
        for (int i = 0; i < wavesCount; i++){
            int aproxEnemiesCount = Mathf.FloorToInt((waveTime + delay) / delay * aproxSpawersCount);
            aproxEnemiesCountPerWave[i] = aproxEnemiesCount;
            delay -= delay / enemiesSpawDecreaseFraction;
        }
        //aproxEnemiesCountNextWave = (int) waveTime / (int)(enemiesSpawDelayBase - enemiesSpawDelayBase / enemiesSpawDecreaseFraction) * aproxSpawersCount;
    }

    void Start(){
        resting = true;
        timeToNextWave = Time.time + restTime;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update(){
        if (Time.time > timeToNextWave){
            if (enemiesAlive > 0) return;
            if (!resting){
                resting = true;
                timeToNextWave = Time.time + restTime;
            }else{
                NextWave();
                resting = false;
            }
        }else if (!resting){
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
        waveProgress++;
        
        timeToNextWave = Time.time + waveTime;
        waveTime += Random.Range(increaseWaveTimeRange.x, increaseWaveTimeRange.y);
        enemiesSpawDelayBase -= enemiesSpawDelayBase/enemiesSpawDecreaseFraction;
        enemiesSpawDelayVariation = enemiesSpawDelayVariation/enemiesSpawDecreaseFraction;
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
        GUILayout.Label("Resting: " + resting.ToString());
        GUILayout.Label("Wave Count: " + waveProgress);
        GUILayout.Label("Time to next Wave: " + (timeToNextWave - Time.time).ToString());
        GUILayout.Label("Spawers Count: " + spawers.Count);
        GUILayout.Label("Enemies Spawed: " + enemiesSpawed);
        GUILayout.Label("Enemies Alive: " + enemiesAlive);
    }
}
