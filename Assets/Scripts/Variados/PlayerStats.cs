using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStats : MonoBehaviour
{
    public float MaxHealth;
    public float CurHealth;
    public GameObject HitSFX;
    public GameObject GameOverScreen;
    void Start()
    {
        CurHealth = MaxHealth;
        GameOverScreen = GameObject.Find("GameOver");
        GameOverScreen.SetActive(false);
    }
    public void Update(){
        if(CurHealth <= 0)
            Die();

    }
    public void Die(){
        this.gameObject.SetActive(false);
        GameOverScreen.SetActive(true);
        Invoke("LoadScene", 3.5f);
    }
     public void LoadScene(){
        SceneManager.LoadScene(0);
    }
    public void ReciveDamage(float Damage){
        CurHealth -= Damage;
        GameObject Current = Instantiate(HitSFX,transform.position, Quaternion.identity);
        CameraShake.instance.ShouldShake = true;
        Destroy(Current , 2);
    }
}
