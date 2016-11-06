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
        EventManager.addActionToEvent(EventType.EVENTBEFOREWIN, cinematiqueEnd);
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

    void cinematiqueEnd()
    {
        pnj.GetComponent<SpriteRenderer>().enabled = true;
        Menu bravo = new Menu(
                     new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=>
                                                                {
                                                                    EventManager.raise(EventType.MENU_EXIT);
                                                                    print();
                                                                }, "Finir")
                                                      },
                     "Mon dieu ! Mais tu as tué celui qui a écrasé le héros !\nMais tu n'es pourtant qu'un forgeron sans avenir...\nC'est..."
                 );
        EventManager.raise<Menu>(EventType.MENU_ENTERED, bravo);
    }
}



