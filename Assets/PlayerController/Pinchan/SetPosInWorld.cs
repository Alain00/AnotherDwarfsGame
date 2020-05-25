using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosInWorld : MonoBehaviour
{
   public static SetPosInWorld instance; 
   public void Awake(){
       if(instance == null)
            instance = this;
   }
   public Vector3 SetPos(Vector3 ObjPos){
        RaycastHit hit;
       if( Physics.Raycast(ObjPos , Vector3.down , out hit , 100f )){
             ObjPos =  hit.point + new Vector3(0,1,0);
       }
       return ObjPos; 
   }

}
