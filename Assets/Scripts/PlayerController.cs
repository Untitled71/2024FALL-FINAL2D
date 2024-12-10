using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
//using static UnityEditor.VersionControl.Asset;

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
    private GameObject self;
        private Rigidbody2D rb;
        private CapsuleCollider2D clidr;
        private SpriteRenderer srpt;

        // EXTERNAL REFERENCES
        public BoxCollider2D Groundcheck;
        public LayerMask groundMask;
        public GameObject renderPLAYER;
    public GameObject deadbody;
        public Sprite sALIVE;
        public Sprite sLIMINAL;
        public Sprite sGHOST;
        public GameObject cursor;
        private Vector2 mouse;
        private Vector3 innitialpos;
    public TextMeshProUGUI Ghosttext;

    public Transform Rotatorattackthing;
    public Transform AttackPoint;

    public float ghosttimeallowed;

        // MOVEMENT LOGIC
        private float mx;
        private float my;

        // JUMPING LOGIC
        private bool grounded;
        [Range(0f, 1f)]
        public float grounddecay;

    // ATTACKING LOGIC
    // private float damage = 1.5f;
    // LEFT
    private float atkticker = 0f;
    private float atktime = .7f;
    // RIGHT
    private float atkticker2 = 0f;
    private float atktime2 = .3f;

    public GameObject RenderATK;
    public GameObject RenderATK2;
    //public Transform attackPos;
    //public LayerMask whatisEnemies;
    //public float attackRange = 10f;
     
        // PLAYER STATS
        public float PlayerLife = 3f;
        public float PlayerSpeed = 10f;
        public float JumpSpeed = 5f;
        public bool seeghosts = false;

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
            self = GetComponent<GameObject>();
            rb = GetComponent<Rigidbody2D>();
            clidr = GetComponent<CapsuleCollider2D>();
            srpt = renderPLAYER.GetComponent<SpriteRenderer>();

            clidr.enabled = true;
            srpt.sprite = sALIVE;
            Pstate = Playerstates.LIVING;

        ghosttimeallowed = 10f;
            grounded = false;
            //groundMask = "Ground";


            UnityEngine.Cursor.visible = false;

    }



    // MOST INPUTS:
        // BY FRAMERATE TICK REFRESH
        void Update()
        {
        Ghosttext.text = "Ghost Timer: " + ghosttimeallowed;

            mx = Input.GetAxisRaw("Horizontal");
            my = Input.GetAxisRaw("Vertical");
        
             float angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Rotatorattackthing.localRotation = Quaternion.Euler(0, 0, angle + 90);
        AttackPoint.rotation = Rotatorattackthing.rotation;
        // MOUSE /////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursor.transform.position = mouse;
            if ( atkticker <= 0f) {
                if ((Input.GetMouseButtonDown(1) && Pstate == Playerstates.LIVING))
                {
                    //Debug.Log("spawmed");
                    Instantiate(RenderATK, mouse, Quaternion.identity);
                    //ActiveHeldObj();
                    /*Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatisEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage; 
                    }*/
                    atkticker = atktime;
            }
            } else { atkticker -= Time.deltaTime; }
            
        if (atkticker2 <= 0f)
        {
            if ((Input.GetMouseButtonDown(0) && Pstate == Playerstates.LIMINAL))
            {
                Debug.Log("L clicked");
                Instantiate(RenderATK2, AttackPoint.position, AttackPoint.rotation);
                //ActiveHeldObj();
                /*Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatisEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage; 
                }*/
                atkticker2 = atktime2;
            }
        }
        else { atkticker2 -= Time.deltaTime; }

        // INPUTS ////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////
        if (Input.GetKeyDown("q"))
            {   // TRANSFORM!!
                if (Pstate == Playerstates.LIVING || Pstate == Playerstates.LIMINAL)
                {
                     innitialpos = transform.position;//
                    Instantiate(deadbody,transform.position, Quaternion.identity);//
                    prevstate = Pstate;
                    Pstate = Playerstates.GHOST;
                } else if (Pstate == Playerstates.GHOST)
            {
                transform.position = innitialpos;//
                Pstate = prevstate;
                rb.velocity += new Vector2(0, -3);
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
                    if (Pstate == Playerstates.GHOST) { 
                    transform.position = innitialpos; //
                }
                    Pstate = Playerstates.LIVING;
                rb.velocity += new Vector2(0, -3);
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


                // see default, move normal 
                // MOVEMENT
                friction();
                    rb.gravityScale = 7f; 
                    if (Mathf.Abs(mx) > 0)
                    {
                        rb.velocity = new Vector2(mx * PlayerSpeed, rb.velocity.y);
                    }
                    if (Mathf.Abs(my) > 0 && grounded)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, my * JumpSpeed);
                    }

                seeghosts = false;
                if (ghosttimeallowed <= 10)
                {
                    ghosttimeallowed += Time.deltaTime *2;
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

                seeghosts = false;
                ghosttimeallowed -= Time.deltaTime * 2;
                break;

                case Playerstates.GHOST:
                    srpt.sprite = sGHOST;
                    clidr.enabled = false;

                    rb.gravityScale = 0;
                    // see everything, move GHOST, Closer to DEAD, Touch Ghost
                    // not seen by anyone alive

                        rb.velocity = new Vector2(mx, my).normalized * PlayerSpeed;


                // 
                seeghosts = true;
                ghosttimeallowed -= Time.deltaTime;

                break;

                case Playerstates.DEAD:
                    // Zombie?

                    break;

            }

            if(ghosttimeallowed <= 0)
            {
                Pstate = Playerstates.LIVING;
            transform.position = innitialpos;
        }
        }





    // FUNCTIONS
    // ACTIVES
    private void ActiveHeldObj()
    {



    }

    // PASSIVES
        // walking
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
        
        // attack


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
        else if(collision.gameObject.tag == "Ghosts"){

        }

    }
}



