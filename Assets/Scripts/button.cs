using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public bool pulse;
    public float timelast;


    // Start is called before the first frame update
    void Start()
    {
        pulse = false;
        timelast = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (pulse == true) { timelast -= Time.deltaTime; }
        if (timelast <= 0 ) { pulse = false; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collidered button");
        if ( collision.gameObject.tag == "actor")
        {
            pulse = true;
            Debug.Log("pulsing");
        }
    }




}
