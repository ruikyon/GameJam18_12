using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private float speed = 10;
    private float border = 400;
    public bool move = true, alive = true;
    private Vector3 prePos;
    //private int counter = 0;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        prePos = transform.position;
        Scheduler.instance.AddEvent(5, CheckPos);
    }

    // Update is called once per frame
    void Update() {

        if (move)
        {
            var distance = (Player.instance.transform.position - transform.position).sqrMagnitude;
            if (distance < border)
            {
                speed = 4.5f;
                //Debug.Log("in");
            }
            else
            {
                speed = 2.5f;
                //Debug.Log("out");
            }

            var dir = Player.instance.transform.position;
            dir.y = transform.position.y;
            transform.LookAt(dir);
            var vel = transform.forward * speed;
            vel.y = rb.velocity.y;
            rb.velocity = vel;
            //Debug.Log("velocity: " + rb.velocity);
        }
    }

    public void Death()
    {
        if (!alive) return;
        alive = false;
        GetComponent<Collider>().isTrigger = false;
        animator.SetBool("Death", true);
        move = false;
        GameManager.instance.DecEnemy();
        Destroy(gameObject, 2);
    }

    private void CheckPos()
    {
        if((transform.position -prePos).sqrMagnitude < 0.01)
        {            
            var x = Random.Range(-50, 50);
            var y = Random.Range(-50, 50);
            transform.position = new Vector3(x, 0, y);
        }
    }
}
