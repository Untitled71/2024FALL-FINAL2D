using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Hostile : MonoBehaviour
{
    private Transform player;
    public float speed = 3f;

    float distance;
    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        /*
        distance = Vector3.Distance(transform.position, player.position);
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        transform.position = Vector3.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        */
    }


}
