using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.VersionControl.Asset;

public class PlayerController : MonoBehaviour//, IPlayerController
{
    // fuck with people or stop murders before your own time runs out.
    /// TO DO LIST
    /// 1. FIX JUMP
    /// 2. 
    /// 
    /// 
    /// 
    /// 
    /// TO DO LIST



    // VARS   
        // OBJECT COMPONENTS
        private Rigidbody2D rb;
        private CapsuleCollider2D clidr;
        private SpriteRenderer srpt;

        //movement and control Logic
        public GameObject renderPLAYER;
        private Vector2 mouse;
        public GameObject cursor;
        public Sprite sALIVE;
        public Sprite sLIMINAL;
        public Sprite sGHOST;
        private float mx;
        private float my;
        private bool postjumpress;

     
        // PLAYER STATS
        public float PlayerLife = 3f;
        public float PlayerSpeed = 10f;
        private float JumpHeight = 100f;

        // States
        public enum Playerstates
        {
            LIVING,
            LIMINAL,
            GHOST,
            DEAD
        }
        public Playerstates Pstate;
        public Playerstates prevstate;
        //private Playerstates prevstate;

    // DECLARES
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            clidr = GetComponent<CapsuleCollider2D>();
            srpt = renderPLAYER.GetComponent<SpriteRenderer>();

            clidr.enabled = true;
            srpt.sprite = sALIVE;
            Pstate = Playerstates.LIVING;

            postjumpress = true;


            UnityEngine.Cursor.visible = false;

    }

  


    // MOST INPUTS:
    // BY FRAMERATE TICK REFRESH
    void Update()
    {
        //Debug.Log(postjumpress);
        //Debug.Log(Pstate.ToString());

        // place different control settings in each case!!!


        // MOUSE /////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = mouse;
        //Debug.Log(mouse);

        if (Input.GetMouseButtonDown(0))
        {
            ActiveHeldObj();
        }

        // INPUTS ////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////
        if (Input.GetKeyDown("q"))
        {   // TRANSFORM!!
            if (Pstate == Playerstates.LIVING || Pstate == Playerstates.LIMINAL)
            {
                prevstate = Pstate;
                Pstate = Playerstates.GHOST;
            } else if (Pstate == Playerstates.GHOST)
            {
                Pstate = prevstate;
            }
            Debug.Log(Pstate.ToString());
        }




    }

    // BY PHYSICS TICK REFRESH
    private void FixedUpdate()
    {
        mx = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(mx, my).normalized * PlayerSpeed;


        // SWITCH ////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////
        switch (Pstate)
        {
            case Playerstates.LIVING:
                srpt.sprite = sALIVE;
                clidr.enabled = true;


                // see default, move normal 
                rb.gravityScale = 10f;
                //when the player hits space, JUMP
                if ((Input.GetAxis("Jump") > .05f) && postjumpress == false)
                { 
                        Debug.Log("trying");
                        postjumpress = true;
                        jump();
                        //rb.AddForce(transform.up * JumpHeight, ForceMode2D.Impulse);
                } 
                if (Input.GetKeyDown("left shift"))
                {
                    rb.AddForce(transform.up * -4f, ForceMode2D.Impulse);
                }
                break;

            case Playerstates.LIMINAL:
                srpt.sprite = sLIMINAL;
                clidr.enabled = true;


                // see more, move normal ~slower/wobbly

                break;

            case Playerstates.GHOST:
                srpt.sprite = sGHOST;
                clidr.enabled = false;


                rb.gravityScale = 0;
                my = Input.GetAxisRaw("Vertical");
                // see everything, move GHOST, Closer to DEAD, Touch Ghost

                break;

            case Playerstates.DEAD:
                // Zombie?

                break;

        }
    }












    // FUNCTIONS
    private void GatherInput()
    {

    }









    private void ActiveHeldObj()
    {

    }





    // COLLISION HANDLING
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
