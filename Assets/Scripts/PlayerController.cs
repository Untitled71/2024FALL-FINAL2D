using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class PlayerController : MonoBehaviour
{
    // fuck with people or stop murders before your own time runs out.


    private Vector2 mouse;
    public GameObject cursor;
    private Rigidbody2D rb;
    private float mx;
    private float my;

    // PLAYER STATS
    public float PlayerLife = 3f;
    public float Haccel = 10f;

    // States
    public enum Playerstates
    {
        LIVING,
        LIMINAL,
        GHOST,
        DEAD
    }
    public Playerstates Pstate;
    //private Playerstates prevstate;







    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Pstate = Playerstates.LIVING;
    }

    // Update is called once per frame
    void Update()
    {
        // place different control settings in each case!!!
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");


        // MOUSE /////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mouse);
        cursor.transform.position = mouse;

        if (Input.GetMouseButtonDown(0))
        {
            ActiveHeldObj();
        }

        // SWITCH ////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////
        switch (Pstate)
        {
            case Playerstates.LIVING:
                // see default, move normal 

                break;

            case Playerstates.LIMINAL: 
                // see more, move normal ~slower/wobbly

                break;

            case Playerstates.GHOST:
                // see everything, move GHOST, Closer to DEAD

                break;

            case Playerstates.DEAD:
                // Zombie?

                break;

        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(mx, my).normalized * Haccel;
        //rb.AddForce(transform)
    }

    private void ActiveHeldObj()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player has collided with: " + collision.gameObject.name);
        if (Pstate != Playerstates.GHOST)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(collision.gameObject);
                PlayerLife--;

            }
        }


    }
}
