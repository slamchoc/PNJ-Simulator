using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PNJ : MonoBehaviour
{
    protected Menu menu;

	// Use this for initialization
	void Start ()
    {
        EventManager.addActionToEvent(EventType.TITRE_APPARAIT, () => { this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); });
	}

    void OnDestroy()
    {
        EventManager.removeActionFromEvent(EventType.TITRE_APPARAIT, () => { this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); });
    }

    // Update is called once per frame
    void Update ()
    {
    
	
	}

    public void setMenu(List<Pair<Callback, String>> arrayOfFunctions, String text)
    {
        if(arrayOfFunctions.Count != 0)
            menu = new Menu(arrayOfFunctions, text);
    }
    public void setMenu(Menu _menu)
    {
        menu = _menu;
    }

    virtual public void printMenu()
    {
        if(menu != null)
            EventManager.raise<Menu>(EventType.MENU_ENTERED, menu);
    }
}
