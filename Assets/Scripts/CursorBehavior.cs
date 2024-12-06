using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehavior : MonoBehaviour
{
    private Vector2 mouse;
    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mouse);

        transform.position = mouse;
    }
}
