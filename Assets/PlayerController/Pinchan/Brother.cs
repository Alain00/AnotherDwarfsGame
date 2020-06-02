using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Brother : MonoBehaviour
{
    Transform player;
    public float radius;
    bool Open;
    public GameObject Store;
    FollowPlayer CameraController;
    public PlayerController Player;
    public GameObject CloseText;
    public GameObject ExclamationSign;
    public GameObject ItemWindow;
    EnemiesController controller;

    void Awake(){
        Store = GameObject.Find("Shop");
        CloseText = GameObject.Find("CloseText");
        ItemWindow = GameObject.FindObjectOfType<StoreManager>().ItemWindow;
        Player = GameObject.FindObjectOfType<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        CameraController = GameObject.FindObjectOfType<FollowPlayer>();
         Store.SetActive(false);
         controller = EnemiesController.main;
    }
    void Start()
    {
        CloseText.SetActive(false);
        ExclamationSign.SetActive(false);
        if(SceneManager.GetActiveScene().buildIndex != 3)
        gameObject.SetActive(false);
        else {
            transform.position = SetPosInWorld.instance.SetPos(transform.position);
        }
        EnemiesController.main.OnWaveBegins += OnWaveBegins;
    }

    
    void Update()
    {
        if(Vector3.Distance(transform.position , player.position )  < radius ){
            ExclamationSign.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space)){
                FixStore();
                Open = !Open;
                OpenStore(Open); 
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter)){
                if (controller.IsLastWave()){
                    SceneManager.LoadScene(1, LoadSceneMode.Single);
                    int levelsCompleted = PlayerPrefs.GetInt("LevelsCompleted");
                    if (levelsCompleted < 0) levelsCompleted = 0;
                    PlayerPrefs.SetInt("LevelsCompleted", levelsCompleted+1);
                }
            }
        }
        else ExclamationSign.SetActive(false);
    }
    void OpenStore( bool Open ){
        Cursor.visible = Open;
        if(Open){
            CameraController.Offset += Vector3.up * 15;
            //Cursor.lockState = CursorLockMode.None;
        }else{ 
            CameraController.Offset -= Vector3.up * 15;
            //Cursor.lockState = CursorLockMode.Locked;
        }
        Store.SetActive(Open);
        CloseText.SetActive(Open);
        ExclamationSign.SetActive(!Open);
        Player.enabled = !Open;
       
    }

    void OnWaveBegins(){
        if (Open){
            FixStore();
            Open = false;
            OpenStore(false);
        }
    }
    void FixStore(){
          if(ItemWindow.activeSelf)
                    ItemWindow.SetActive(false);
                BuyableItem Cur = Store.GetComponent<StoreManager>().Current; 
                Store.GetComponent<StoreManager>().IsFocusing = false;   
                if(Cur != null){
                    Cur.Focus = false;
                    Cur.transform.localEulerAngles = Cur.StartRot;
                    Cur.transform.localPosition = Cur.StartPos;
                    Cur = null;
                }   
    }
}
