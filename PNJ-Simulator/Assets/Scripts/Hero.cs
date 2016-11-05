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

    Menu jour2;
    Menu jour2Suite;
    Menu jour2VenteEpee;
    Menu jour2RecevoirEpee;
    Menu jour2betterSword;
    Menu jour2recevoirBetterSword;
    Menu jour2Internal;
    Menu jour2Fin;
    Menu jour2Internal2;

    String venteEpee = "Tiens voila cette epee, elle est de tres bonne facture.\nElle a ete forgee a base d'eponge de Valsemin";
    String betterSword = "Ouais mais cette epee elle est mieux !";


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
                            venteEpee
                        );

        jour1RecevoirEpee = new Menu(
                            new List<Pair<Callback, String>> {
                                                            new Pair<Callback, String>(() => { EventManager.raise(EventType.MENU_ENTERED, jourFin); }, "Continuer")
                                                             },
                            "Hero :\nHa ... Heu ... Tres bien, je la prend.\nJe vous laisse, j'ai un royaume a sauver, moi !"
                        );

        jourFin = new Menu(
                            new List<Pair<Callback, String>> {
                                                            new Pair<Callback, String>(() => { changementJour(); }, "terminer le jour")
                                                             },
                            "Merci, et bonne journee a vous"
                        );
    }

    void initJour2()
    {
        jour2 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_ENTERED, jour2Suite); }, "continuer")
                                                                 },
                                "Bonjour aventurier."
                            );
        jour2Suite = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_ENTERED, jour2VenteEpee); }, "vendre epee")
                                                                 },
                                "Hero :\nOuais bonjour, ton epee s'est cassee tres rapidement.\nC'est ca ta bonne qualite ???\nTu n'as pas une autre epee ?"
                            );
        jour2VenteEpee = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_ENTERED, jour2RecevoirEpee); }, "continuer")
                                                                 },
                                venteEpee
                            );
        jour2RecevoirEpee = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_ENTERED, jour2betterSword); }, "continuer")
                                                                 },
                                "Hero :\nNan mais c'est la meme ..."
                            );
        jour2betterSword = new Menu(
                        new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_ENTERED, jour2recevoirBetterSword); }, "continuer")
                                                         },
                        betterSword
                    );
        jour2recevoirBetterSword = new Menu(
                        new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_ENTERED, jour2Internal); }, "continuer")
                                                         },
                        "Hero :\nBon d'accord, je vais essayer ..."
                    );
        jour2Internal = new Menu(
                       new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_ENTERED, ); }, "continuer")
                                                        },
                       "(* mais je vais la sortir d'où cette epee ? Je dois aller la chercher *)"
                   );
        jour2RecevoirEpee = new Menu(
                       new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_ENTERED, jour2Fin); }, "continuer")
                                                        },
                       "Hero :\nMerci .. j'espere pour toi que ce sera mieux"
                   );
        jour2Fin = new Menu(
                       new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_ENTERED, jour2Internal2); }, "continuer")
                                                        },
                       "Merci et bonne journee a vous"
                   );
        jour2Internal2 = new Menu(
                       new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { /*TODO gain gold, loose rep*/ }, "terminer le jour")
                                                        },
                       "(* Le hero commence a m'enerver !\nIl n'y aura plus personne pour aujourd'hui.\n Je vais vers l'escalier, il serai temps d'aller me coucher *)"
                   );
    }
}
