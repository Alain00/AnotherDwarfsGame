using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool DisplayGizmos;
    public LayerMask NotWalkableMask;
    public Vector2 GridWorldSize;
    private Node[,] nodos;
    public  float nodoRadius;
    
    private float nodoDiametre;
    private int GridXSize, GridZSize;
    public int MaxGridSize;
    public void Start()
    {
        nodoDiametre = nodoRadius * 2;
        GridXSize = Mathf.RoundToInt( GridWorldSize.x / nodoDiametre);
        GridZSize = Mathf.RoundToInt(GridWorldSize.y / nodoDiametre);
        MaxGridSize = GridXSize * GridZSize;
        GenerateGrid();
    }
    void GenerateGrid()
    {
        nodos = new Node[GridXSize,GridZSize];
        Vector3 BottomLeftGridPos = transform.position - Vector3.right * (GridWorldSize.x / 2) -
                                    Vector3.forward * (GridWorldSize.y / 2); 
        for (int i = 0; i < GridXSize; i++){
            for (int j = 0; j < GridZSize; j++)
            {
                
                Vector3 CurNodWorldPos = BottomLeftGridPos + Vector3.right * (i * nodoDiametre + nodoRadius)
                                                           + Vector3.forward * (j * nodoDiametre + nodoRadius)
                                                          ;
                 RaycastHit hit;
                  if( Physics.Raycast(CurNodWorldPos , Vector3.down , out hit , 100f )){
                      CurNodWorldPos =  hit.point + new Vector3(0,1,0);
                    }
                                          
                bool Walkable = !(Physics.CheckSphere(CurNodWorldPos, nodoRadius , NotWalkableMask) );
                nodos[i, j] = new Node(Walkable, CurNodWorldPos, new Vector2(i, j));
                
            }
            
        }                            
    }

    public Node NodePosFromWorldPos(Vector3 ToConvertPos)
    {
        float xporcent = (ToConvertPos.x + GridWorldSize.x / 2) / GridWorldSize.x;
        float yporcent = (ToConvertPos.z + GridWorldSize.y / 2) / GridWorldSize.y;
        xporcent = Mathf.Clamp01(xporcent);
        yporcent = Mathf.Clamp01(yporcent);
        int x = Mathf.RoundToInt(xporcent * (GridXSize - 1));
        int y = Mathf.RoundToInt(yporcent * (GridZSize - 1));
        return  nodos[x,y];

    }

    public List<Node> FindNeighbours(Node nodo)
    {
        List<Node>Neighbours = new List<Node>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if(i == 0 && j == 0)
                    continue;
                int CurrNodX = i + (int)nodo.GridPos.x;
                int CurrNodY = j + (int)nodo.GridPos.y;
                if (CurrNodX >= 0 && CurrNodY >= 0 && CurrNodX < GridXSize && CurrNodY < GridZSize)
                {
                    Neighbours.Add(nodos[CurrNodX,CurrNodY]);
                } 
            }
        }

        return Neighbours;
    }
    
    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position,new Vector3(GridWorldSize.x,1,GridWorldSize.y));

        if (DisplayGizmos) {
            if (nodos != null) {
               
                foreach (Node n in nodos) {
                    Gizmos.color = (n.IsWalkable)?Color.white:Color.red;
                    Gizmos.DrawCube(n.WorldPos, Vector3.one * (nodoDiametre-.7f));
                }
            }
        }
    }
}
