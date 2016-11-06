using UnityEngine;
using System.Collections.Generic;
using System;

public class Hero : MonoBehaviour {

    [SerializeField]
    public Player player;

    public Menu nextMenu { get; private set; }

	// Use this for initialization
	void Start ()
    {
        Debug.Assert(player != null);
        DontDestroyOnLoad(this.gameObject);

        EventManager.addActionToEvent<ScenesType>(EventType.CHANGE_SCENE, hideHero);

	}

    // Update is called once per frame
    void Update()
    {

    }

    void hideHero(ScenesType newScene)
    {
        if (newScene == ScenesType.MAP)
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public bool load(int day)
    {
        if (day == 1)
        {
            initJour1();
            nextMenu = jour1;
        }
        else if (day == 2)
        {
            initJour2();
            nextMenu = jour2;
        }
        else if (day == 5)
        {
            initJour5();
            nextMenu = jour5;
        }
        else if (day == 7)
        {
            initJour7();
            nextMenu = jour7;
            EventManager.raise(EventType.SLAM_DOOR);
        }
        else
            return false;
        return true;
    }

    public void printnextMenu()
    {
        if(nextMenu != null)
            EventManager.raise<Menu>(EventType.MENU_ENTERED, nextMenu);
    }

    /* Shop Dialogues */
    Menu jour1;
    Menu jour1VenteEpee;
    Menu jour1RecevoirEpee;
    Menu jour1VenteBouclier;
    Menu jour1RecevoirBouclier;
    Menu jourFin;

    Menu jour2;
    Menu jour2Suite;
    Menu jour2VenteEpee;
    Menu jour2RecevoirEpee;
    Menu jour2betterSword;
    Menu jour2recevoirBetterSword;
    Menu jour2Internal;
    Menu jour2Fin;
    Menu jour2Internal2;

    Menu jour5;
    Menu jour5Suite;
    Menu jour5Coupe;
    Menu jour5VenteEpee;
    Menu jour5Encore;
    Menu jour5Better;
    Menu jour5Inside;
    Menu jour5Buy;

    Menu jour7;
    Menu jour7_1;
    Menu jour7_2;
    Menu jour7_3;
    Menu jour7_4;
    Menu jour7_5;
    Menu jour7_6;
    Menu jour7_7;
    Menu jour7_8;
    Menu jour7_9;

    String venteEpee = "Tiens voila cette epee, elle est de tres bonne facture.\nElle a ete forgee a base d'eponge de Valsemin";
    String betterSword = "Ouais mais cette epee elle est mieux !";


    void initJour1()
    {
        jour1 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_EXIT);  EventManager.raise<Menu>(EventType.MENU_ENTERED, jour1VenteEpee); }, "vendre epee"),
                                                                new Pair<Callback, String>(() => { EventManager.raise(EventType.MENU_EXIT);EventManager.raise<Menu>(EventType.MENU_ENTERED, jour1VenteBouclier); }, "vendre bouclier")
                                                                 },
                                "Heros :\nBonjour, forgeron.\nC'est la premiere fois que j'entre dans ton echoppe.\nAurais-tu un armement de qualite a me proposer ?"
                            );

        jour1VenteBouclier = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(() => {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour1RecevoirBouclier); }, "continuer")
                                                                 },
                                "Tiens voila ce bouclier, il est de tres bonne facture.\nIl a ete forgee a base d'ecailles de langoustes du nord"
                            );

        jour1RecevoirBouclier = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(() => {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour1VenteEpee); }, "vendre epee")
                                                                 },
                                "Heros :\nNon, je vous ai demande un armement !"
                            );

        jour1VenteEpee = new Menu(
                            new List<Pair<Callback, String>> {
                                                            new Pair<Callback, String>(() => {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour1RecevoirEpee); }, "Continuer")
                                                             },
                            venteEpee
                        );

        jour1RecevoirEpee = new Menu(
                            new List<Pair<Callback, String>> {
                                                            new Pair<Callback, String>(() => {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jourFin); }, "Continuer")
                                                             },
                            "Heros :\nHa ... Heu ... Tres bien, je la prend.\nJe vous laisse, j'ai un royaume a sauver, moi !"
                        );

        jourFin = new Menu(
                            new List<Pair<Callback, String>> {
                                                            new Pair<Callback, String>(() => {GetComponent<Animator>().Play("Up"); GetComponent<Rigidbody>().velocity = new Vector3(0,0.5f,0); nextMenu = null; EventManager.raise(EventType.MENU_EXIT); EventManager.raise(EventType.MENU_EXIT); EventManager.raise(EventType.END_DAY); }, "terminer le jour")
                                                             },
                            "Merci, et bonne journee a vous"
                        );
    }

    void initJour2()
    {
        jour2 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2Suite); }, "continuer")
                                                                 },
                                "Bonjour aventurier."
                            );
        jour2Suite = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2VenteEpee); }, "vendre epee")
                                                                 },
                                "Heros :\nOuais bonjour, ton epee s'est cassee tres rapidement.\nC'est ca ta bonne qualite ???\nTu n'as pas une autre epee ?"
                            );
        jour2VenteEpee = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2RecevoirEpee); }, "continuer")
                                                                 },
                                venteEpee
                            );
        jour2RecevoirEpee = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2betterSword); }, "continuer")
                                                                 },
                                "Heros :\nNan mais c'est la meme ..."
                            );
        jour2betterSword = new Menu(
                        new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2recevoirBetterSword); }, "continuer")
                                                         },
                        betterSword
                    );
        jour2recevoirBetterSword = new Menu(
                        new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2Internal); }, "continuer")
                                                         },
                        "Heros :\nBon d'accord, je vais essayer ..."
                    );
        jour2Internal = new Menu(
                       new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { nextMenu = jour2RecevoirEpee; EventManager.raise(EventType.MENU_EXIT); }, "continuer")
                                                        },
                       "(* mais je vais la sortir d'où cette epee ? Je dois aller la chercher *)"
                   );
        jour2RecevoirEpee = new Menu(
                       new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2Fin); }, "continuer")
                                                        },
                       "Heros :\nMerci .. j'espere pour toi que ce sera mieux"
                   );
        jour2Fin = new Menu(
                       new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2Internal2); }, "continuer")
                                                        },
                       "Merci et bonne journee a vous"
                   );
        jour2Internal2 = new Menu(
                       new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {GetComponent<Animator>().Play("Up"); GetComponent<Rigidbody>().velocity = new Vector3(0,0.5f,0); nextMenu = null; player.addGold();player.looseReputation(); EventManager.raise(EventType.MENU_EXIT); }, "terminer le jour")
                                                        },
                       "(* Le Heros commence a m'enerver !\nIl n'y aura plus personne pour aujourd'hui.\n Je vais vers l'escalier, il serai temps d'aller me coucher *)"
                   );
    }

    void initJour5()
    {
        jour5 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour5Suite); }, "continuer")
                                                                 },
                                "HEROS :\nMAIS C'EST DE LA MERDE !!!"
                            );
        jour5Suite = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour5Coupe); }, "continuer")
                                                                 },
                                "Bonjour avent..."
                            );
        jour5Coupe = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour5VenteEpee); }, "continuer")
                                                                 },
                                "Heros :\nMais ferme la !\nL'epee c'est la meme, et c'est de la merde !\nDonne moi une bonne epee sale paysan !"
                            );
        jour5VenteEpee = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour5Encore); }, "continuer")
                                                                 },
                                venteEpee
                            );
        jour5Encore = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour5Better); }, "continuer")
                                                                 },
                                "HEROS :\nMAIS T'EST CON, C'EST LA MEME !"
                            );
        jour5Better = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour5Inside); }, "continuer")
                                                                 },
                                betterSword
                            );
        jour5Inside = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour5Buy); }, "continuer")
                                                                 },
                                "(* Pourquoi je raconte toujours la meme chose ? *)"
                            );
        jour5Buy = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {GetComponent<Animator>().Play("Up"); GetComponent<Rigidbody>().velocity = new Vector3(0,0.5f,0); nextMenu = null;player.addGold();player.looseReputation(); EventManager.raise(EventType.MENU_EXIT); }, "continuer")
                                                                 },
                                "Heros :\nBon OK, mais c'est la derniere fois sinon ...\nEssaie de faire ce que je fait avec ta camelote"
                            );    }

    void initJour7()
    {
        jour7 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_1); }, "continuer")
                                                                 },
                                "HEROS :\nTU TE FOUT DE MA GUEULE !!!"
                            );
        jour7_1 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_2); }, "continuer")
                                                                 },
                                "Heu ... on peut discuter ?"
                            );
        jour7_2 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_3); }, "continuer")
                                                                 },
                                "HEROS :\nNAN, TOUT CE QUE TU VENDS CA SERT A RIEN !!!"
                            );
        jour7_3 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_4); }, "continuer")
                                                                 },
                                "(* Il va se calmer l'autre la ? *)"
                            );
        jour7_4 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_5); }, "continuer")
                                                                 },
                                "HEROS :\nMAIS TU SERS A QUOI DANS LA VIE ?"
                            );
        jour7_5 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_6); }, "continuer")
                                                                 },
                                "(* Bah ... *)"
                            );
        jour7_6 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_7); }, "continuer")
                                                                 },
                                "Heros :\nHeuresement j'ai trouve une superbe epee dans un donjon.\nJe n'aurai plus besoin de revoir ta sale tronche !"
                            );
        jour7_7 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {  EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_8); }, "continuer")
                                                                 },
                                "Heros :\nBonne fin de vie"
                            );

        jour7_8 = new Menu(
                        new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_9); }, "continuer")
                                                         },
                        "(* Il m'insulte mais qu'est-ce qui nous differencie ?\nSon armement, son experience, son charisme ... *)"
                    );
        jour7_9 = new Menu(
                        new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {GetComponent<Animator>().Play("Up"); GetComponent<Rigidbody>().velocity = new Vector3(0,0.5f,0); nextMenu = null; EventManager.raise(EventType.MENU_EXIT); }, "continuer")
                                                         },
                        "(* A part ca moi aussi je peux le faire ! *)"
                    );

    }
}
