using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
        EventManager.addActionToEvent<Menu>(EventType.MENU_ENTERED, menuToPrint);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void menuToPrint(Menu _menu)
    {

    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<Menu>(EventType.MENU_ENTERED, menuToPrint);
    }
}
