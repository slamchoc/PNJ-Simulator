﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public bool stopCamera = false;
    public GameObject target;

	// Use this for initialization
	void Start ()
    {
        if(target != null && !stopCamera)
            this.gameObject.transform.position = target.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        if(!stopCamera)
        {

            if (target != null)
            {
                Vector3 velocity = target.GetComponentInParent<Rigidbody>().velocity;
                Vector3 future = Vector3.SmoothDamp(this.transform.position, target.transform.position, ref velocity, 0.1f);
                this.gameObject.transform.position = new Vector3(future.x, future.y, -10);
            }
            else
            {
                if (GameObject.FindObjectOfType<Player>() != null)
                    target = GameObject.FindObjectOfType<Player>().gameObject;
            }
        }
    }
}
