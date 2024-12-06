using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.VersionControl.Asset;

public class PlayerController : MonoBehaviour
{
    // fuck with people or stop murders before your own time runs out.


    //movement and control Logic
    private Vector2 mouse;
    public GameObject cursor;
    private Rigidbody2D rb;
    private float mx;
    private float my;
    private bool postjumpress;


    // PLAYER STATS
    public float PlayerLife = 3f;
    public float PlayerSpeed = 10f;
    public float JumpHeight = 10f;

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
        UnityEngine.Cursor.visible = false;
        postjumpress = false;
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


        // MOUSE /////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mouse);
        cursor.transform.position = mouse;

        if (Input.GetMouseButtonDown(0))
        {
            ActiveHeldObj();
        }

        // INPUTS ////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////
        if (Input.GetKeyDown("q"))
        {   // TRANSFORM!!
            Playerstates prevstate = Pstate;
            if (Pstate == Playerstates.LIVING || Pstate == Playerstates.LIMINAL)
            {
                Pstate = Playerstates.GHOST;
            }
            else if (Pstate == Playerstates.GHOST)
            {
                Playerstates Pstate = prevstate;
            }

            Debug.Log(Pstate.ToString());
        }



        // SWITCH ////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////
        switch (Pstate)
        {
            case Playerstates.LIVING:
                // see default, move normal 
                if (postjumpress != true)
                {
                    if (Input.GetKeyDown("space"))
                    {
                        postjumpress = true;
                        rb.AddForce(transform.up * JumpHeight, ForceMode2D.Impulse);
                    }
                }
                if (Input.GetKeyDown("left shift"))
                {
                    rb.AddForce(transform.up * -4f, ForceMode2D.Impulse);
                }
                break;

            case Playerstates.LIMINAL:
                // see more, move normal ~slower/wobbly

                break;

            case Playerstates.GHOST:
                my = Input.GetAxisRaw("Vertical");
                // see everything, move GHOST, Closer to DEAD, Touch Ghost

                break;

            case Playerstates.DEAD:
                // Zombie?

                break;

        }
    }



















    private void FixedUpdate()
    {

        rb.velocity = new Vector2(mx, my).normalized * PlayerSpeed;

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
            if (collision.gameObject.tag == "Ground")
            {
                postjumpress = false;

            }
        }


    }
}
