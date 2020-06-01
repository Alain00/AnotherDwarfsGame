using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHolder : MonoBehaviour
{
    Transform bullet;
    Rigidbody Rb;
    public float speed;
    public float AutoDestruct =5;
    public LayerMask layerMask;
    public GameObject ImpactVFX;
    public GameObject EnemyImpactVFX;
    void Start(){
        bullet = GetComponentInChildren<Transform>();
        StartCoroutine(PerformHit());
    }

    IEnumerator PerformHit(){
        RaycastHit hit;
        if (Physics.Raycast(bullet.position, bullet.forward, out hit, 100, layerMask)){
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //Instantiate(go, hit.point, Quaternion.identity);
            go.name = "HitPoint";
            go.transform.position = hit.point;
            go.GetComponent<Collider>().enabled = false;
            go.GetComponent<MeshRenderer>().material.color = Color.red;
            //Debug.Log("Hit in something");
            Collider col = hit.collider;
            if (!col) yield return null;

            if(col.gameObject.tag != "Player"){
                float time = (hit.point - transform.position).magnitude / speed;
                yield return new WaitForSeconds(time);
                if (!col) yield return null;
                if(col.gameObject.tag == "Enemy"){
                    col.gameObject.GetComponent<Destructible>().OnDamage(50);
                    Instantiate(EnemyImpactVFX , hit.point , Quaternion.identity);

                }else Instantiate(ImpactVFX , hit.point , Quaternion.identity);
                
                Destroy(gameObject);
            }
            //Debug.Log(col.gameObject.name);
        } 
    }

    void Update()
    {

        AutoDestruct-= Time.deltaTime;
        if(AutoDestruct<= 0){
            Destroy(gameObject);
        }
        transform.position +=bullet.forward * speed * Time.deltaTime;
    }

}
