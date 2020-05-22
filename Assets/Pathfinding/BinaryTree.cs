using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree 
{
  public Node[] nodos;
  public int currentNodoCount = 0;
  

  //Definir Size que va a tener el arbol
  public void Tree(int MaxLeafsCount)
  {
    nodos = new Node[MaxLeafsCount];
  }

  //Add un node al arbol y ponerlo en la pos que tiene q ir
  public void AddNode(Node ToAdd)
  {
    ToAdd.TreeIndex = currentNodoCount;
    nodos[currentNodoCount] = ToAdd;
    ////////
    currentNodoCount++;
    SortUP(ToAdd);
    
  }

  //el nombre lo dice
  public void UpdateNode(Node ToUpdate)
  {
    SortUP(ToUpdate);
  }

  //Si quiero sacar el nodo con mas prioridad 
  public Node RemoveFirst()
  {
    Node ToRemoveNode = nodos[0];
    currentNodoCount--;
    nodos[0] = nodos[currentNodoCount];
    nodos[0].TreeIndex = 0;
    SortDown(nodos[0]);
    return ToRemoveNode;



  }
  //Ordenar un nodo en sentido hacia arriba del arbol
  public void SortUP(Node Curnodo)
  {
    int ParentIndex = (Curnodo.TreeIndex - 1) / 2;
    
    while (true)
    {
      Node parent = nodos[ParentIndex];
      if (parent.tCost > Curnodo.tCost)
      {
        Swap(parent , Curnodo);
      }
      else break;
      
      ParentIndex = (Curnodo.TreeIndex - 1) / 2;
    }
    
  }
  //En sentido hacia abajo
  public void SortDown( Node CurNodo)
  {
      while (true)
      {
        int NodoLeftChildren = CurNodo.TreeIndex * 2 + 1;
        int NodoRightChildren = CurNodo.TreeIndex * 2 + 2;
        int SwapNodoIndex = 0;

        if (NodoLeftChildren < currentNodoCount)
        {
          SwapNodoIndex = NodoLeftChildren;
          if (NodoRightChildren < currentNodoCount)
          {
            if (nodos[NodoRightChildren].tCost < nodos[NodoLeftChildren].tCost)
            {
              SwapNodoIndex = NodoRightChildren;
            }
          }
        }
        else return;

        if (nodos[SwapNodoIndex].tCost < CurNodo.tCost)
        {
          Swap(nodos[SwapNodoIndex] , CurNodo);
        }
        else return;
      }
  }
  
  //Swap entre 2 nodos
  public void Swap(Node a, Node b)
  {

    nodos[a.TreeIndex] = b;
    nodos[b.TreeIndex] = a;
    
    int TempIndex = a.TreeIndex;
    a.TreeIndex = b.TreeIndex;
    b.TreeIndex = TempIndex;
    
  }
  //Preguntar si existe tal nodo en el arbol
  public bool Contains( Node nodo)
  {
    return Equals(nodo, nodos[nodo.TreeIndex]);
  }
  
 /* public struct TreeNode
  {
    public Node nodo;
    public int TreeIndex;

    public void _TreeNode(Node a, int index)
    {
      nodo = a;
      TreeIndex = index;
    }
  }*/
}
