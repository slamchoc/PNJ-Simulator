using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class House : MonoBehaviour {

    // Use this for initialization
    void Start() {
        Menu houseMenu = new Menu(
                                        new List<Pair<Callback, String>> { new Pair<Callback, String>( () => { EventManager.raise(EventType.MENU_EXIT); }, "Continue...") }, "(* Mais on ne rentre pas chez les gens... *)"
                                    );
        this.gameObject.GetComponent<PNJ>().setMenu(houseMenu);
    }
}
