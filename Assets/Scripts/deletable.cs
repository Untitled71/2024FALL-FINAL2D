using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerController;

public class deletable : MonoBehaviour
{
   // public float damage = 1.5f;
    public float lifetime = 2.5f; 
    public void OnCollisionEnter2D(Collision2D collision) { 
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
