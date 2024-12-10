using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Hostile : MonoBehaviour
{
    public Transform spawnpoint;
    //private Transform player;
    Rigidbody2D rb;

    public float speed = 3f;
    public Transform position1;
    public Transform position2;

    public float distance1;
    public float distance2;

    bool goforward;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = position1.position;
        goforward = true;
        //player = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        distance1 = Vector3.Distance(transform.position, position1.position);
        distance2 = Vector3.Distance(transform.position, position2.position);

        //Debug.Log("Distance1" + distance1);
        //Debug.Log("Distance2" + distance2);
        if (distance1 <= 2 )
        {
            goforward = true;
        }
        else if (distance2 <= 1)
        {
            goforward = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (goforward)
        {
            Debug.Log("moving forward");
            rb.velocity = new Vector2(speed,0);
            //transform.position += new Vector3(speed*Time.deltaTime, 0, 0);
        }
        else
        {

            rb.velocity = new Vector2(-(speed), 0);
            //.position += new Vector3(-(speed), 0, 0);
        }
            /*
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            transform.position = Vector3.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            */
        }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Ground")
        {

            collision.transform.position = spawnpoint.position;
            //Destroy(collision.gameObject);
        }
    }

}
