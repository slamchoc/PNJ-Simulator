using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (target != null)
        {
            Vector3 velocity = target.GetComponentInParent<Rigidbody>().velocity;
            Vector3 future =  Vector3.SmoothDamp(this.transform.position, target.transform.position, ref velocity, 0.2f);
            this.gameObject.transform.position = new Vector3(future.x, future.y, -10);
        }
        else
            target = GameObject.FindObjectOfType<Player>().gameObject;
	}
}
