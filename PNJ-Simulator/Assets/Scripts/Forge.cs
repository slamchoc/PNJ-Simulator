using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Forge : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Menu forgeMenu = new Menu(
                                            new List<Pair<Callback, String>> { new Pair<Callback, String>(() => { EventManager.raise(EventType.MENU_EXIT); }, "Continue...") }, " \" Fermé. \" "
                                        );
        this.gameObject.GetComponent<PNJ>().setMenu(forgeMenu);
    }
	
}
