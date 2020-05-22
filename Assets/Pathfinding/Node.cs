using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{

    public bool IsWalkable;
    public Vector3 WorldPos;
    public int eDist;
    public int sDist;
    public Vector2 GridPos;
    public Node Parent;
    public int treeIndex;

    public Node ( bool _IsWalkable , Vector3 _WorldPos , Vector2 _GridPos){
         IsWalkable =  _IsWalkable;
         WorldPos =  _WorldPos;
         GridPos = _GridPos;

    }

    public int tCost
    {
        get
        {
            return eDist + sDist;
        }
    }
    public int TreeIndex {
        get {
            return treeIndex;
        }
        set {
            treeIndex = value;
        }
    }

}
