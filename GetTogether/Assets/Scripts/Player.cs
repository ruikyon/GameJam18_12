using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player instance;
    private Rigidbody rb;
    private Animator animator;
    private float speed = 5;
    private float interval = 8;
    [SerializeField]
    private Transform userCamera;
    [SerializeField]
    private Trap[] traps = new Trap[4];
    public int selectTrap = 0;
    public bool cooling = false;
    private int preChange = 0;
    private int preUse = -1;

    [SerializeField]
    TextMeshProUGUI preUseColor;

	// Use this for initialization
	void Start () {
        instance = this;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        if (x != 0 || y != 0)
        {
            animator.SetBool("Running", true);
            transform.forward = Quaternion.Euler(0, userCamera.eulerAngles.y, 0) * new Vector3(x, 0, -y);
            //var rot = (x!=0)?90-Mathf.Atan(y / x):0;
            //transform.Rotate(new Vector3(0, rot, 0));
            var vel = transform.forward * speed;
            vel.y = rb.velocity.y;
            rb.velocity = vel;
        }
        else
        {
            animator.SetBool("Running", false);
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        var change = (int)Input.GetAxisRaw("Select");
        if (preChange != change && change != 0)
            selectTrap = (selectTrap + change < 0) ? selectTrap + change + 4 : (selectTrap + change)%4;
        preChange = change;

        //if (Input.GetButtonDown("Fire1")) Debug.Log("fire 1");
        if (!cooling && Input.GetButtonDown("Fire1") && preUse != selectTrap)
        {
            cooling = true;
            traps[selectTrap].ActivateTrap();
            Scheduler.instance.AddEvent(interval, FinCool);

            string text;
            switch (selectTrap)
            {
                case 0:
                    text = "red";
                    break;
                case 1:
                    text = "green";
                    break;
                case 2:
                    text = "yellow";
                    break;
                case 3:
                    text = "blue";
                    break;
                default:
                    text = "";
                    break;
            }
            preUseColor.text = text;
            preUse = selectTrap;
            GameManager.trapCount++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy>().alive)
        {
            GameManager.instance.GameOver();
        }
       
    }

    private void FinCool()
    {
        cooling = false;
    }
}
