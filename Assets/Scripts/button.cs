using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public GameObject Player;
    public bool display;
    public bool pulse;
    public float timelast;
    public Collider2D cd2d;


    // Start is called before the first frame update
    void Start()
    {
        pulse = false;
        timelast = 5.0f;
        display = Player.GetComponent<PlayerController>().seeghosts;
        cd2d = GetComponent<Collider2D>();
        cd2d.isTrigger = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (pulse == true) { timelast -= Time.deltaTime; }
        if (timelast <= 0 ) { pulse = false;
            timelast = 5.0f;
        }


        display = Player.GetComponent<PlayerController>().seeghosts;
        if (display)
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
        else if (pulse == true)
        {

            GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collidered button");
        if ( collision.gameObject.tag == "actor")
        {
            pulse = true;
            //Debug.Log("pulsing");
        }
    }



  
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collidered button");
        if (other.gameObject.tag == "actor")
        {
            pulse = true;
            //.Log("pulsing");
        }
    }
}
