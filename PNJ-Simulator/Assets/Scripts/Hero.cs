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

    String venteEpee = "Tiens, voila cette épée, elle est de très bonne facture.\nElle a été forgée à base d'éponge de Valsemin";
    String betterSword = "Ouais, mais cette épée elle est mieux !";


    void initJour1()
    {
        jour1 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_EXIT);  EventManager.raise<Menu>(EventType.MENU_ENTERED, jour1VenteEpee); }, "Vendre épée"),
                                                                new Pair<Callback, String>(() => { EventManager.raise(EventType.MENU_EXIT);EventManager.raise<Menu>(EventType.MENU_ENTERED, jour1VenteBouclier); }, "Vendre bouclier")
                                                                 },
                                "Héros :\nBonjour, forgeron.\nC'est la première fois que j'entre dans ton échoppe.\nAurais-tu un armement de qualité à me proposer ?"
                            );

        jour1VenteBouclier = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(() => {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour1RecevoirBouclier); }, "Continuer")
                                                                 },
                                "Tiens, voila ce bouclier, il est de tres bonne facture.\nIl a été forgée à base d'écailles de langoustes du nord"
                            );

        jour1RecevoirBouclier = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(() => {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour1VenteEpee); }, "Vendre épée")
                                                                 },
                                "Héros :\nNon, je vous ai demandé un armement !"
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
                            "Heros :\nHa ... Heu ... Très bien, je la prend.\nJe vous laisse, j'ai un royaume à sauver, moi !"
                        );

        jourFin = new Menu(
                            new List<Pair<Callback, String>> {
                                                            new Pair<Callback, String>(() => {GetComponent<Animator>().Play("Up"); GetComponent<Rigidbody>().velocity = new Vector3(0,0.5f,0); nextMenu = null; EventManager.raise(EventType.MENU_EXIT); EventManager.raise(EventType.MENU_EXIT); EventManager.raise(EventType.END_DAY); }, "Terminer le jour")
                                                             },
                            "Merci, et bonne journée a vous"
                        );
    }

    void initJour2()
    {
        jour2 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2Suite); }, "Continuer")
                                                                 },
                                "Bonjour aventurier."
                            );
        jour2Suite = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2VenteEpee); }, "Vendre épée")
                                                                 },
                                "Héros :\nOuais bonjour, ton épée s'est cassée très rapidement.\nC'est ça ta bonne qualité ???\nTu n'as pas une autre épée ?"
                            );
        jour2VenteEpee = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2RecevoirEpee); }, "Continuer")
                                                                 },
                                venteEpee
                            );
        jour2RecevoirEpee = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2betterSword); }, "Continuer")
                                                                 },
                                "Héros :\nNan mais c'est la même ..."
                            );
        jour2betterSword = new Menu(
                        new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2recevoirBetterSword); }, "Continuer")
                                                         },
                        betterSword
                    );
        jour2recevoirBetterSword = new Menu(
                        new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2Internal); }, "Continuer")
                                                         },
                        "Héros :\nBon d'accord, je vais essayer ..."
                    );
        jour2Internal = new Menu(
                       new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { nextMenu = jour2RecevoirEpee; EventManager.raise(EventType.MENU_EXIT); }, "Continuer")
                                                        },
                       "(* Mais je vais la sortir d'où cette épée ? Je dois aller la chercher *)"
                   );
        jour2RecevoirEpee = new Menu(
                       new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2Fin); }, "Continuer")
                                                        },
                       "Héros :\nMerci .. j'espère pour toi que ce sera mieux"
                   );
        jour2Fin = new Menu(
                       new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour2Internal2); }, "Continuer")
                                                        },
                       "Merci et bonne journee à vous"
                   );
        jour2Internal2 = new Menu(
                       new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {GetComponent<Animator>().Play("Up"); GetComponent<Rigidbody>().velocity = new Vector3(0,0.5f,0); nextMenu = null; player.addGold();player.looseReputation(); EventManager.raise(EventType.MENU_EXIT); }, "Terminer le jour")
                                                        },
                       "(* Le Héros commence à m'enerver !\nIl n'y aura plus personne pour aujourd'hui.\n Je vais vers l'escalier, il serait temps d'aller me coucher *)"
                   );
    }

    void initJour5()
    {
        jour5 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour5Suite); }, "Continuer")
                                                                 },
                                "Héros :\nMAIS C'EST DE LA MERDE !!!"
                            );
        jour5Suite = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour5Coupe); }, "Continuer")
                                                                 },
                                "Bonjour avent..."
                            );
        jour5Coupe = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour5VenteEpee); }, "Continuer")
                                                                 },
                                "Héros :\nMais ferme la !\nL'épée c'est la même, et c'est de la merde !\nDonne moi une bonne épée sale paysan !"
                            );
        jour5VenteEpee = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour5Encore); }, "Continuer")
                                                                 },
                                venteEpee
                            );
        jour5Encore = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour5Better); }, "Continuer")
                                                                 },
                                "Héros :\nMAIS T'EST CON, C'EST LA MEME !"
                            );
        jour5Better = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour5Inside); }, "Continuer")
                                                                 },
                                betterSword
                            );
        jour5Inside = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour5Buy); }, "Continuer")
                                                                 },
                                "(* Pourquoi je raconte toujours la même chose ? *)"
                            );
        jour5Buy = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {GetComponent<Animator>().Play("Up"); GetComponent<Rigidbody>().velocity = new Vector3(0,0.5f,0); nextMenu = null;player.addGold();player.looseReputation(); EventManager.raise(EventType.MENU_EXIT); }, "Continuer")
                                                                 },
                                "Héros :\nBon OK, mais c'est la dernière fois sinon ...\nEssaie de faire ce que je fait avec ta camelote..."
                            );    }

    void initJour7()
    {
        jour7 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_1); }, "Continuer")
                                                                 },
                                "Héros :\nTU TE FOUS DE MA GUEULE !!!"
                            );
        jour7_1 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_2); }, "Continuer")
                                                                 },
                                "Heu ... on peut discuter ?"
                            );
        jour7_2 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_3); }, "Continuer")
                                                                 },
                                "Héros :\nNAN, TOUT CE QUE TU VENDS CA SERT A RIEN !!!"
                            );
        jour7_3 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_4); }, "Continuer")
                                                                 },
                                "(* Il va se calmer l'autre là ? *)"
                            );
        jour7_4 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_5); }, "Continuer")
                                                                 },
                                "Héros :\nMAIS TU SERS A QUOI DANS LA VIE ?"
                            );
        jour7_5 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_6); }, "Continuer")
                                                                 },
                                "(* Bah ... *)"
                            );
        jour7_6 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_7); }, "Continuer")
                                                                 },
                                "Héros :\nHeureusement, j'ai trouvé une superbe épée dans un donjon.\nJe n'aurai plus besoin de revoir ta sale tronche !"
                            );
        jour7_7 = new Menu(
                                new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {  EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_8); }, "Continuer")
                                                                 },
                                "Héros :\nBonne fin de vie."
                            );

        jour7_8 = new Menu(
                        new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, jour7_9); }, "Continuer")
                                                         },
                        "(* Il m'insulte mais qu'est-ce qui nous différencie ?\nSon armement, son experience, son charisme ... *)"
                    );
        jour7_9 = new Menu(
                        new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> {GetComponent<Animator>().Play("Up"); GetComponent<Rigidbody>().velocity = new Vector3(0,0.5f,0); nextMenu = null; EventManager.raise(EventType.MENU_EXIT); }, "Continuer")
                                                         },
                        "(* A part ça, moi aussi je peux le faire ! *)"
                    );

    }
}
