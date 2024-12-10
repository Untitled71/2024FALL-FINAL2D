using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerController;

public class attackeffect : MonoBehaviour
{
    // public float damage = 1.5f;
    private float lifetime = 0.3f;
    public void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        //Destroy(gameObject);

    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }

    }
}
