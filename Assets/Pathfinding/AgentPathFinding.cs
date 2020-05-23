using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPathFinding : MonoBehaviour
{
    public int Speed;
    public float RotationSpeed;
    public int radius;
    public Transform target;
    
    private int WayPointIndex;
    private Vector3[] Path;
    private Vector3 CurTargetPos;
    public bool IsUnderFire;
    float timer;
    public GameObject InFireVFX;
    GameObject CurrentVFX;

    private void Start()
    {
        timer = 2;
        CurTargetPos = transform.position;
       // PathRequester.RequestPath( transform.position , target.position , OnPathFound );
    }

    private void Update()
    {
        if(CurTargetPos != target.position && Vector3.SqrMagnitude(CurTargetPos- transform.position) < radius * radius){
            PathRequester.RequestPath( new PathRequest ( transform.position , target.position , OnPathFound) );
            CurTargetPos = target.position;
        }
        if(IsUnderFire){
            Speed = 3;
            if(CurrentVFX == null)
                CurrentVFX = Instantiate(InFireVFX, transform.position , Quaternion.identity , transform);
        }
        else Speed = 5;  
        timer -= Time.deltaTime;
        if(timer <= 0){
            Destroy(CurrentVFX);
            IsUnderFire = false;
            CurrentVFX = null;
            timer = 2;
        }  
    }

    public void OnPathFound(Vector3[] Waypoints, bool Success)
    {
        if(Success && Waypoints.Length > 0){
            WayPointIndex = 0;
            Path = Waypoints;
            StopCoroutine("Walk");
            StartCoroutine("Walk");
        }
    }

    IEnumerator Walk()
    {
        Vector3 CurTarget = Path[0];
        while (true)
        {
            if (Vector3.SqrMagnitude(CurTarget - transform.position) < 1  )
            {
        
                WayPointIndex++;
                if (WayPointIndex >= Path.Length)
                {
                    break;
                }
                CurTarget = Path[WayPointIndex];
            }

            //Rotacion
            Vector3 targetDir = CurTarget - transform.position;
            float step = RotationSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);

           
            transform.position = Vector3.MoveTowards(transform.position, CurTarget , Speed * Time.deltaTime);
            yield  return null; 
        }
    }
    
    public void OnDrawGizmos() {
        if (Path != null) {
            for (int i = WayPointIndex ; i < Path.Length; i ++) {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(Path[i], Vector3.one);

                if (i == WayPointIndex) {
                    Gizmos.DrawLine(transform.position, Path[i]);
                }
                else {
                    Gizmos.DrawLine(Path[i-1],Path[i]);
                }
            }
        }
    }
    
}
