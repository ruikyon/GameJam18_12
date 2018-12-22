using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCamera : MonoBehaviour
{
    private Vector3 height = new Vector3(0, 1, 0);
    private float ud = 0, lr = 0;
    private float rotSpeed = 2;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.instance.transform.position + height;

        var x = Input.GetAxisRaw("CameraLR");
        var y = Input.GetAxisRaw("CameraUD");
        //Debug.Log("camera: "+x+", "+y);

        if (-5 < ud + y && ud + y < 45)
        {
            transform.eulerAngles += new Vector3(y, x, 0) * rotSpeed;
            ud += y;
            //Debug.Log("ud: "+ud);
        }
    }
}
