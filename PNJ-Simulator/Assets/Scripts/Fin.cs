using UnityEngine;
using System.Collections;

public class Fin : MonoBehaviour {

    bool defiler = false;
    float speed = 0.5f;

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
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - (speed * Time.deltaTime), transform.localPosition.z);
            if(transform.localPosition.y >= 6)
            {
                Debug.Log("FIN !");
                defiler = false;
                EventManager.raise(EventType.QUIT_GAME);
            }
        }
	}
}
