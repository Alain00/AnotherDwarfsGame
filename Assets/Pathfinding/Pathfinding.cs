using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    
    private Grid grid;
    BinaryTree tree;
    private PathRequester _pathRequester;
    void Awake()
    {
       tree = new BinaryTree();
        // tree = GetComponent<BinaryTree>();
        grid = GetComponent<Grid>();
        _pathRequester = GetComponent<PathRequester>();
    }
    
    public void PathFinding(PathRequest pathRequest , Action<PathResult>callback)
    {
        tree = new BinaryTree();
        Vector3[] Waypoints = new Vector3[0];
        bool WaypointSucess = false;
        Node startNode = grid.NodePosFromWorldPos(pathRequest.pathStart);
        Node endNode = grid.NodePosFromWorldPos(pathRequest.pathEnd);
        if(startNode.IsWalkable && endNode.IsWalkable){
            tree.Tree(grid.MaxGridSize);
            HashSet<Node> Visited = new HashSet<Node>();
            tree.AddNode(startNode);
            while (tree.currentNodoCount > 0)
            {
                Node CurrentNode = tree.RemoveFirst();
                Visited.Add(CurrentNode);
                
                if (CurrentNode == endNode) 
                {
                    WaypointSucess = true;
                    break;
                }

                foreach (Node vecino in grid.FindNeighbours(CurrentNode))
                {
                    if(!vecino.IsWalkable || Visited.Contains(vecino))
                        continue;
                    int CurDistToNeighbour = CurrentNode.sDist + NodeDist(CurrentNode, vecino);
                    if (CurDistToNeighbour < vecino.sDist || !tree.Contains(vecino))
                    {
                        vecino.sDist = CurDistToNeighbour;
                        vecino.eDist = NodeDist(vecino, endNode);
                        vecino.Parent = CurrentNode;    
                        if(!tree.Contains(vecino))
                        {
                            tree.AddNode(vecino);
                        }
                    }
                }
                
            }
            if (WaypointSucess)
            {
                Waypoints = TrazarPath(startNode,endNode);
            }
            callback(new PathResult(Waypoints , WaypointSucess , pathRequest.callback ));
        }
    }
    void Update(){

    }
    
    int NodeDist(Node a, Node b)
    {
        float XDist = Mathf.Abs(a.GridPos.x - b.GridPos.x);
        float YDist = Mathf.Abs(a.GridPos.y - b.GridPos.y);
        // en las direcciones no diagonales la distancia es 1 y en diagonales es sqrt(2) 
        if (XDist > YDist)
            return (int)( 14 * YDist + 10 * (XDist - YDist) );
        
        else return (int)( 14 * XDist + 10 * (YDist - XDist) );

    }
    
    //Trazar el camino que tiene q recorrer y de paso devolverlo en unidades de unity
    Vector3[] TrazarPath(Node StartNode, Node EndNode)
    {
        Node Current = EndNode;
        List<Node> Path = new List<Node>();

        while (Current != StartNode)
        {
            Path.Add(Current);
            Current = Current.Parent;
        }
        
        Vector3[] Waypoints = SimplifyPath(Path);
        Array.Reverse(Waypoints);
        return Waypoints;
    }
    //Si algun par de waypoints comparten direccion entonces dejarlo en uno solo
    Vector3[] SimplifyPath(List<Node> Path)
    {
       List<Vector3> NewPath = new List<Vector3>();
       Vector2 OldDirection = Vector2.zero;
        for (int i = 1 ; i < Path.Count; i++)
        {
            Vector2 directionNew = new Vector2(Path[i-1].GridPos.x - Path[i].GridPos.x ,Path[i-1].GridPos.y - Path[i].GridPos.y);
            if (directionNew != OldDirection) {
                NewPath.Add(Path[i].WorldPos);
            }
            OldDirection = directionNew;
        }

        return NewPath.ToArray();
    }
    
}
