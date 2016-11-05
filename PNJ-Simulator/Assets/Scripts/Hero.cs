using UnityEngine;
using System.Collections.Generic;
using System;

public class Hero : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void changementJour()
    {

    }

    /* Shop Dialogues */
    Menu jour1VenteEpee;
    Menu jour1RecevoirEpee;
    Menu jour1VenteBouclier;
    Menu jour1RecevoirBouclier;
    Menu jourFin;
    Menu jour1;

    void initJour1()
    {
        jour1 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_ENTERED, jour1VenteEpee); }, "vendre epee"),
                                                                new Pair<Callback, String>(() => { EventManager.raise(EventType.MENU_ENTERED, jour1VenteBouclier); }, "vendre bouclier")
                                                                 },
                                "Hero :\nBonjour, forgeron.\nC'est la premiere fois que j'entre dans ton echoppe.\nAurais-tu un armement de qualite a me proposer ?"
                            );

        jour1VenteBouclier = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(() => { EventManager.raise(EventType.MENU_ENTERED, jour1RecevoirBouclier); }, "continuer")
                                                                 },
                                "Tiens voila ce bouclier, il est de tres bonne facture.\nIl a ete forgee a base d'ecailles de langoustes du nord"
                            );

        jour1RecevoirBouclier = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(() => { EventManager.raise(EventType.MENU_ENTERED, jour1VenteEpee); }, "vendre epee")
                                                                 },
                                "Hero :\nNon, je vous ai demande un armement !"
                            );

        jour1VenteEpee = new Menu(
                            new List<Pair<Callback, String>> {
                                                            new Pair<Callback, String>(() => { EventManager.raise(EventType.MENU_ENTERED, jour1RecevoirEpee); }, "Continuer")
                                                             },
                            "Tiens voila cette epee, elle est de tres bonne facture.\nElle a ete forgee a base d'eponge de Valsemin"
                        );

        jour1RecevoirEpee = new Menu(
                            new List<Pair<Callback, String>> {
                                                            new Pair<Callback, String>(() => { EventManager.raise(EventType.MENU_ENTERED, jourFin); }, "Continuer")
                                                             },
                            "Hero :\nHa ... Heu ... Tres bien, je la prend.\nJe vous laisse, j'ai un royaume a sauver, moi !"
                        );

        jourFin = new Menu(
                            new List<Pair<Callback, String>> {
                                                            new Pair<Callback, String>(() => { changementJour(); }, "termier le jour")
                                                             },
                            "Merci, et bonne journee a vous"
                        );
    }

}
