using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    SpriteRenderer sr;
    Collider col;
    //private bool active = false;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        col= GetComponent<Collider>();
        sr.enabled = false;
        col.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateTrap()
    {
        sr.enabled = true;
        col.enabled = true;
        Scheduler.instance.AddEvent(2, FinTrap);
        //active = true;
    }

    private void FinTrap()
    {
        sr.enabled = false;
        col.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().Death();
        }
    }
}
