using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class puzzlesystem3 : MonoBehaviour
{

    public GameObject Player;
    public bool display;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;

    public GameObject button6;



    public GameObject objecttomove1;
    private Vector3 og1;
    public Transform gohere1;
    public GameObject objecttomove2;
    private Vector3 og2;
    public Transform gohere2;
    public GameObject objecttomove3;
    private Vector3 og3;
    public Transform gohere3;
    public GameObject objecttomove4;
    private Vector3 og4;
    public Transform gohere4;
    public GameObject objecttomove5;
    private Vector3 og5;
    public Transform gohere5;

    public GameObject objecttomove6;
    public Transform gohere6;
    private void Awake()
    {
        display = Player.GetComponent<PlayerController>().seeghosts;
    }
    private void Start()
    {

        og1 = objecttomove1.transform.position;
        og2 = objecttomove2.transform.position;
        og3 = objecttomove3.transform.position;
        og4 = objecttomove4.transform.position;
        og5 = objecttomove5.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (button1.GetComponent<button>().pulse != true)
        {
            objecttomove1.transform.position = og1;
        }
        if (button2.GetComponent<button>().pulse != true)
        {
            objecttomove2.transform.position = og2;
        }
        if (button3.GetComponent<button>().pulse != true)
        {
            objecttomove3.transform.position = og3;
        }
        if (button4.GetComponent<button>().pulse != true)
        {
            objecttomove4.transform.position = og4;
        }
        if (button5.GetComponent<button>().pulse != true)
        {
            objecttomove5.transform.position = og5;
        }

        while (button1.GetComponent<button>().pulse == true)
        {
            objecttomove1.transform.position = gohere1.transform.position;
        }
        while (button2.GetComponent<button>().pulse == true)
        {
            objecttomove2.transform.position = gohere2.transform.position;
        }
        while (button3.GetComponent<button>().pulse == true)
        {
            objecttomove3.transform.position = gohere3.transform.position;
        }
        while (button4.GetComponent<button>().pulse == true)
        {
            objecttomove4.transform.position = gohere4.transform.position;
        }
        while (button5.GetComponent<button>().pulse == true)
        {
            objecttomove5.transform.position = gohere5.transform.position;
        }
        while (button6.GetComponent<button>().pulse == true)
        {
            objecttomove6.transform.position = gohere6.transform.position;
        }
    }
}
