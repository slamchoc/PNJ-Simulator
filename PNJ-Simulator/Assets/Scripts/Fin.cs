using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Fin : MonoBehaviour {

    bool defiler = false;
    float speed = 0.5f;
    public GameObject pnj;

	// Use this for initialization
	void Start () {
        EventManager.addActionToEvent(EventType.WIN,print);
    }

    void print()
    {
        EventManager.raise(EventType.CINEMATIC_BEGIN);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        defiler = true;
    }

	// Update is called once per frame
	void Update () {
        if (defiler)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y + (speed * Time.deltaTime), transform.localPosition.z);
            if(transform.localPosition.y > 5)               
            {
                Debug.Log("FIN !");
                defiler = false;
            }
        }
	}

    void OnDestroy()
    {
        EventManager.removeActionFromEvent(EventType.WIN, print);
    }
}



