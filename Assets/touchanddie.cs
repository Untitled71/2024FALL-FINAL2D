using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchanddie : MonoBehaviour
{

    public Transform spawnpoint;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = spawnpoint.position;
        }
    }
}
