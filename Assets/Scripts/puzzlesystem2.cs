using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class puzzlesystem2 : MonoBehaviour
{

    public GameObject Player;
    public bool display;
    public GameObject button1;
    public GameObject button2;

    public GameObject objecttomove1;
    public Transform gohere1;
    public GameObject objecttomove2;
    public Transform gohere2;
    private void Awake()
    {
        display = Player.GetComponent<PlayerController>().seeghosts;
    }

    // Update is called once per frame
    void Update()
    {/*
        display = Player.GetComponent<PlayerController>().seeghosts;
        if (display)
        {
            button1.GetComponent<MeshRenderer>().enabled = true;
            button2.GetComponent<MeshRenderer>().enabled = true;
            button3.GetComponent<MeshRenderer>().enabled = true;
            button4.GetComponent<MeshRenderer>().enabled = true;
            button5.GetComponent<MeshRenderer>().enabled = true;
        }else
        {
            button1.GetComponent<MeshRenderer>().enabled = false;
            button2.GetComponent<MeshRenderer>().enabled = false;
            button3.GetComponent<MeshRenderer>().enabled = false;
            button4.GetComponent<MeshRenderer>().enabled = false;
            button5.GetComponent<MeshRenderer>().enabled = false;
        }*/



        if (button1.GetComponent<button>().pulse == true)
        {
            objecttomove1.transform.position = gohere1.transform.position;

        }
        if (button2.GetComponent<button>().pulse == true)
        {
            objecttomove2.transform.position = gohere2.transform.position;

        }

    }
}
