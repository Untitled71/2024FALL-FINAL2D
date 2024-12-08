using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class puzzlesystem1 : MonoBehaviour
{

    public GameObject Player;
    public bool display;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;

    public GameObject objecttomove;
    public Transform gohere;
    private void Awake()
    {
        display = Player.GetComponent<PlayerController>().seeghosts;
    }

    // Update is called once per frame
    void Update()
    {
        display = Player.GetComponent<PlayerController>().seeghosts;
        if (display)
        {
            button1.GetComponent<MeshRenderer>().enabled = true;
            button2.GetComponent<MeshRenderer>().enabled = true;
            button3.GetComponent<MeshRenderer>().enabled = true;
            button4.GetComponent<MeshRenderer>().enabled = true;
            button5.GetComponent<MeshRenderer>().enabled = true;
        } else
        {
            button1.GetComponent<MeshRenderer>().enabled = false;
            button2.GetComponent<MeshRenderer>().enabled = false;
            button3.GetComponent<MeshRenderer>().enabled = false;
            button4.GetComponent<MeshRenderer>().enabled = false;
            button5.GetComponent<MeshRenderer>().enabled = false;
        }
       

        if (button1.GetComponent<button>().pulse == true)
        {
            button1.GetComponent<MeshRenderer>().enabled = true;
            if (button5.GetComponent<button>().pulse == true)
            {
                button5.GetComponent<MeshRenderer>().enabled = true;
                if (button2.GetComponent<button>().pulse == true)
                {
                    button2.GetComponent<MeshRenderer>().enabled = true;
                    if (button4.GetComponent<button>().pulse == true)
                    {
                        button4.GetComponent<MeshRenderer>().enabled = true;
                        if (button3.GetComponent<button>().pulse == true)
                        {
                            button3.GetComponent<MeshRenderer>().enabled = true;
                            objecttomove.transform.position = gohere.position;
                        }
                    }
                }
            }
        }


    }
}
