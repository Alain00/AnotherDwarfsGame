using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int[] aproxEnemiesCountPerWave;
    public bool debug;
    
    int waveProgress = 0;
    List<EnemySpawer> spawers = new List<EnemySpawer>();
    int enemiesSpawed = 0;
    int enemiesAlive = 0;
    float timeToNextWave = 0;
    [HideInInspector]
    public bool resting = false;
    Transform playerTransform;
    int levelCount = 1;
    float enemiesHealthIncreased = 0;

    public delegate void OnWaveCompletedEvent();
    public event OnWaveCompletedEvent OnWaveCompleted;
    public delegate void OnWaveBeginsEvent();
    public event OnWaveCompletedEvent OnWaveBegins;
    public delegate void OnLastWaveCompletedEvent();
    public event OnLastWaveCompletedEvent OnLastWaveCompleted;

    void Awake(){
        if (main == null){
            main = this;
            DontDestroyOnLoad(gameObject);
        }else if (main == this) return;
        else{
            Destroy(gameObject);
        }
    }

    /*void OnValidate(){
        // Debug.Log("Hola");
        aproxEnemiesCountPerWave = new int[wavesCount];
        float currentWaveTime = waveTime;
        float delay = enemiesSpawDelayBase;
        for (int i = 0; i < wavesCount; i++){
            int aproxEnemiesCount = Mathf.FloorToInt((currentWaveTime + delay) / delay * aproxSpawersCount);
            aproxEnemiesCountPerWave[i] = aproxEnemiesCount;
            delay -= delay / enemiesSpawDecreaseFraction;
            currentWaveTime += increaseWaveTimeRange.y;
        }
        //aproxEnemiesCountNextWave = (int) waveTime / (int)(enemiesSpawDelayBase - enemiesSpawDelayBase / enemiesSpawDecreaseFraction) * aproxSpawersCount;
    }
*/
    void Start(){
       
        
        SceneManager.sceneLoaded += NewLevelLoaded;
        Initialize();
        
        
    }

    void Initialize(){
        resting = true;
        timeToNextWave = Time.time + restTime;
        waveProgress = 0;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        levelCount = PlayerPrefs.GetInt("LevelsCompleted", 0);
        enemiesHealthIncreased += Random.Range(increaseEnemiesHealthRange.x, increaseEnemiesHealthRange.y);

        OnWaveCompleted += HandleWaveCompleted;
        OnWaveBegins += HandleWaveBegins;
        OnLastWaveCompleted = HandleLastWaveCompleted;
        Debug.Log("Enemies Controller initialized");
    }

    void Clear(){
         waveProgress = 0;
        enemiesAlive = 0;
        enemiesSpawed = 0;

        OnWaveBegins = null;
        OnWaveCompleted = null;
        spawers = new List<EnemySpawer>();
    }

    void Update(){
        if (Time.time > timeToNextWave){
            if (enemiesAlive > 0) return;
            if (!resting){
                if (waveProgress == wavesCount && OnLastWaveCompleted != null){
                    OnLastWaveCompleted();
                }else if (OnWaveCompleted != null)
                    OnWaveCompleted();
                resting = true;
                timeToNextWave = Time.time + restTime;
            }else{
                if (OnWaveBegins != null)
                 OnWaveBegins();
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
        enemyAI.health += enemiesHealthIncreased;
        /*for (int i = 0; i < levelCount; i++){
            enemyAI.health += Random.Range(increaseEnemiesHealthRange.x, increaseEnemiesHealthRange.y);
        }*/
        enemyAI.onDied += HandleEnemyDied;
        enemyAI.target = playerTransform;
    }

    void HandleEnemyDied(){
        enemiesAlive--;
    }

    void HandleLastWaveCompleted(){
        Clear();
    }

    void HandleWaveCompleted(){}

    void HandleWaveBegins(){}

    void NewLevelLoaded(Scene scene, LoadSceneMode mode){
        if (scene.buildIndex == 2)
            Initialize();
    }

    public bool IsLastWave(){
        return waveProgress >= wavesCount;
    }

    public int GetCurrentRestingTime(){
        return Mathf.FloorToInt(Mathf.Max(0, timeToNextWave - Time.time));
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
