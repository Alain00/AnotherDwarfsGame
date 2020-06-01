using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IslandGenerator))]
public class IslandLimitsGenerator : MonoBehaviour
{
    public float angleOffset = 45;
    public float radiusAdd = 0;
    IslandGenerator generator;
    Vector3 center;

    void Start(){
        Initialize();
    }

    /*void OnValidate(){
        // Initialize();
    }
*/
    void Initialize(){
        generator = GetComponent<IslandGenerator>();
        GenerateLimits();
    }

    void GenerateLimits(){
        float r = generator.size;
        // Debug.Log(r);
        center = new Vector3(r, 0, -r);
        r/=2;
        r+=radiusAdd;
        Debug.Log(center);
        float angle = 0;
        BoxCollider[] childs = GetComponentsInChildren<BoxCollider>();
        GameObject save = new GameObject("SaveLimits");
        foreach(BoxCollider bc in childs){
            bc.transform.parent = save.transform;
        }
        save.SetActive(false);

        for (;angle < 360; angle += angleOffset){
            float z = center.z + r*Mathf.Cos(angle * Mathf.Deg2Rad);
            float x = center.x + r*Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 point = new Vector3(x,0,z);
            // Vector3 direction = point - center;
            //Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = point;
            cube.transform.eulerAngles = new Vector3(0,angle, 0);
            cube.transform.parent = transform;
            BoxCollider collider = cube.GetComponent<BoxCollider>();
            collider.size = new Vector3(r,r,1);
        }
    }

     IEnumerator Destroy(GameObject go){
         yield return new WaitForEndOfFrame();
         DestroyImmediate(go);
     }
}
