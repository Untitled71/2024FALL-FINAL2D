using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door1 : MonoBehaviour
{
    public GameObject Player;
    public Transform level2;
    public bool display;
    public bool pulse;
    public float timelast;


    // Start is called before the first frame update
    void Start()
    {
        pulse = false;
        timelast = 5.0f;
        display = Player.GetComponent<PlayerController>().seeghosts;
    }

    // Update is called once per frame
    void Update()
    {
        if (pulse == true) { timelast -= Time.deltaTime; }
        if (timelast <= 0)
        {
            pulse = false;
            timelast = 5.0f;
        }


        display = Player.GetComponent<PlayerController>().seeghosts;
        if (display)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (pulse == true)
        {

            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }

        if (pulse == true)
        {
            // endgame
            //SceneManager.LoadScene(sceneName:"Levelcomplete");
            Player.transform.position = level2.position;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collidered button");
        if (collision.gameObject.tag == "actor")
        {
            pulse = true;
            Debug.Log("pulsing");
        }
    }




}
