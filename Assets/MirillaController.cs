using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirillaController : MonoBehaviour
{
    public Texture2D Mirilla;
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.SetCursor(Mirilla , Vector2.zero , CursorMode.ForceSoftware);
    }
}
