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

        // EXTERNAL REFERENCES
        public BoxCollider2D Groundcheck;
        public LayerMask groundMask;
        public GameObject renderPLAYER;
        public Sprite sALIVE;
        public Sprite sLIMINAL;
        public Sprite sGHOST;
        public GameObject cursor;
        private Vector2 mouse;

        // MOVEMENT LOGIC
        private float mx;
        private float my;
        private bool grounded;

        [Range(0f, 1f)]
        public float grounddecay;
     
        // PLAYER STATS
        public float PlayerLife = 3f;
        public float PlayerSpeed = 10f;
        public float JumpSpeed = 5f;

        // STATES (FSM)
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

            grounded = false;
            //groundMask = "Ground";


            UnityEngine.Cursor.visible = false;

    }



    // MOST INPUTS:
        // BY FRAMERATE TICK REFRESH
        void Update()
        {
            mx = Input.GetAxisRaw("Horizontal");
            my = Input.GetAxisRaw("Vertical");

            // MOUSE /////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursor.transform.position = mouse;
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
            }
            if (Input.GetKeyDown("e"))
            {   // TRANSFORM!!
                if (Pstate == Playerstates.LIVING)
                {
                    Pstate = Playerstates.LIMINAL;
                }
                else 
                {
                    Pstate = Playerstates.LIVING;
                }
            }


    }

        // BY PHYSICS TICK REFRESH
        private void FixedUpdate()
        {
            checkground();

            // SWITCH ////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////
            switch (Pstate)
            {
                case Playerstates.LIVING:
                    srpt.sprite = sALIVE;
                    clidr.enabled = true;

                    friction();
                    // see default, move normal 
                    rb.gravityScale = 7f; 
                    if (Mathf.Abs(mx) > 0)
                    {
                        rb.velocity = new Vector2(mx * PlayerSpeed, rb.velocity.y);
                    }
                    if (Mathf.Abs(my) > 0 && grounded)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, my * JumpSpeed);
                    }


                    break;

                case Playerstates.LIMINAL:
                    srpt.sprite = sLIMINAL;
                    clidr.enabled = true;


                    // see more, move normal ~slower/wobbly
                    // seen as crazy
                    rb.gravityScale = 4f; 
                    if (Mathf.Abs(mx) > 0)
                    {
                        rb.velocity = new Vector2(mx * PlayerSpeed, rb.velocity.y);
                    }
                    if (Mathf.Abs(my) > 0 && grounded)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, my * JumpSpeed);
                    }


                    break;

                case Playerstates.GHOST:
                    srpt.sprite = sGHOST;
                    clidr.enabled = false;

                    rb.gravityScale = 0;
                    // see everything, move GHOST, Closer to DEAD, Touch Ghost
                    // not seen by anyone alive

                        rb.velocity = new Vector2(mx, my).normalized * PlayerSpeed;
    

                    break;

                case Playerstates.DEAD:
                    // Zombie?

                    break;

            }
        }





    // FUNCTIONS
    // ACTIVES
    private void ActiveHeldObj()
    {




    }

    // PASSIVES
    private void friction()
    {
        if (grounded && mx == 0 && my == 0)
        {
            rb.velocity *= grounddecay;
        }
    }

    private void checkground()
    {
        grounded = Physics2D.OverlapAreaAll(Groundcheck.bounds.min, Groundcheck.bounds.max, groundMask).Length > 0;
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
                grounded = true;

            }
        }


    }
}



