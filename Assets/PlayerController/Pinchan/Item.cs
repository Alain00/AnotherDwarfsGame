﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public bool Comprada;
    public float Ammo;
    public Sprite sprite;
    public abstract void ItemAction( Quaternion Dir );

}
