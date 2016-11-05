using UnityEngine;
using System.Collections;
using System;

public class PNJ : MonoBehaviour
{

    private Menu menu;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void setMenu(Pair<Callback,String>[] arrayOfFunctions, String text)
    {
        menu = new Menu(arrayOfFunctions, text);
    }

    virtual public void printMenu()
    {
        EventManager.raise<Menu>(EventType.MENU_ENTERED, menu);
    }
}
