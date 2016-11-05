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
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
    
	
	}

    public void setMenu(List<Pair<Callback, String>> arrayOfFunctions, String text)
    {
        menu = new Menu(arrayOfFunctions, text);
    }
    public void setMenu(Menu _menu)
    {
        menu = _menu;
    }

    virtual public void printMenu()
    {
        EventManager.raise<Menu>(EventType.MENU_ENTERED, menu);
    }
}
