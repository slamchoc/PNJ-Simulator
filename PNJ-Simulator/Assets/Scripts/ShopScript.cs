using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ShopScript : MonoBehaviour {

    public GameObject cinematique;
    [SerializeField]
    private GameObject stair;
    [SerializeField]
    private GameObject myPNJ;
    [SerializeField]
    private Door door;

    private Vector3 initPlayerPosition = new Vector3(0, -1, -2);

    String PNJName; 
    String textRandom;
    String textOption1Random;
    Menu option1Retour;
    String textOption2Random;
    Menu option2Retour;
    String textOption3Random;
    Menu option3Retour;

    List<String> list1 = new List<String>
    {
        "épée",
        "bouclier",
        "bombe"
    };
    List<String> list2 = new List<String>
    {
        "une loutre",
        "un dragon",
        "un tabouret malin",
        "une tortue",
        "un pinguoin",
        "un géant",
        "une grand-mère",
        "un feunouil",
    };
    List<String> list3 = new List<String>
    {
        "de givre",
        "de dragon",
        "divine",
        "étoilée",
        "pinguesque",
        "pour les poulettes",
        "de flammes",
        "pour myopes",
        "en braille",
        "magnanime"
    };
    List<String> listNames = new List<String>
    {
        "Totoro"
    };
    List<String> listQuetes = new List<String>
    {
        "J'ai perdu mon marteau.\nPeux-tu aller le récuperer dans le marécage maudit ?",
        "Peux-tu me donner 800 ailes de papillon albinos.",
        "Peux-tu aller tuer l'\"Emma la cruelle\" ?",
        "Peux-tu aider les développeurs de ce jeu ?",
        "Peux-tu aller accompagner Joe le rigolo au fleuve de Coco l'asticot ?",
        "Jean des égouts voudrait un goûteur qui ne soit pas allergique au cyanure.",
        "Va tuer la grande méchante belle-mère juste derrière ma forge."
    };

    Menu goodRetour;
    Menu badRetour;


    [SerializeField]
    private Hero hero;

    /// <summary>
    /// the current visitor of the shop
    /// </summary>
    private GameObject visitor;

    private int currentDay = 0;

    private bool needCinematique = true;

    public GameObject titre;

    // Use this for initialization
    void Start ()
    {

        /*menu du PNJ*/
        goodRetour = new Menu(
                                new List<Pair<Callback, String>> { new Pair<Callback, String>(() => { exitVisitor(); hero.player.addGold(); hero.player.addReputation(); EventManager.raise(EventType.MENU_EXIT); }, "Finir journée") }
                                , PNJName + " :\nAh, parfait !\nMerci"
                            );
        badRetour = new Menu(
                                        new List<Pair<Callback, String>> { new Pair<Callback, String>(() => { exitVisitor(); hero.player.addGold(); hero.player.addReputation(); EventManager.raise(EventType.MENU_EXIT); }, "Finir journée") }
                                        , PNJName + " :\nAh, parfait !\nMerci"
                                    );



        EventManager.raise(EventType.STOP_SOUND);
        EventManager.raise<SoundsType>(EventType.PLAY_SOUND_ONCE, SoundsType.MUSIQUE_CINEMATIQUE2);
        cinematique.SetActive(true);

        Menu stairMenu = new Menu(
                                        new List<Pair<Callback, String>> { new Pair<Callback, String>(nextDay,"Dormir") }, "(* Enfin, la journée se termine *)"
                                    );
        stair.GetComponent<PNJ>().setMenu(stairMenu);
        EventManager.addActionToEvent(EventType.END_DAY, nextDay);
        EventManager.addActionToEvent(EventType.SLAM_DOOR,slamDoor);
        //TODO c'est degueux, trouver autre chose !
        hero = FindObjectOfType<Hero>();
        hero.gameObject.SetActive(true);
        hero.player.gameObject.SetActive(true);
        hero.player.transform.localScale = new Vector2(2, 2);
        hero.transform.localScale = new Vector2(2, 2);
        hero.player.transform.position = initPlayerPosition;
        hero.GetComponent<SpriteRenderer>().enabled = true;
        hero.transform.position = new Vector2(10,10);

        visitor = hero.gameObject;
    }

    void OnDestroy()
    {
        hero.transform.localScale = new Vector2(1, 1);
        hero.player.transform.localScale = new Vector2(1, 1);
        EventManager.removeActionFromEvent(EventType.END_DAY, nextDay);
        EventManager.removeActionFromEvent(EventType.SLAM_DOOR, slamDoor);
    }

    void slamDoor()
    {
        door.changeToDestructedSprite();
        hero.GetComponent<Rigidbody>().velocity = new Vector2(0, -5);
        hero.GetComponent<Animator>().Play("Grrrr");
        EventManager.raise<SoundsType>(EventType.PLAY_SOUND_ONCE, SoundsType.PORTE_CASSEE);

    }

    // Update is called once per frame
    void Update ()
    {
        if(needCinematique)
        {
            cinematique.SetActive(true);

            // cinematique
            cinematique.transform.position -= new Vector3(
                       Time.deltaTime * 2.0f,
                       0,
                       0
                       );
            if (cinematique.transform.position.x < -  cinematique.GetComponent<SpriteRenderer>().bounds.size.x / 2 - Camera.main.aspect * Camera.main.orthographicSize)
            {
                needCinematique = false;
                cinematique.SetActive(false);
                EventManager.raise(EventType.STOP_SOUND);
                EventManager.raise<SoundsType>(EventType.PLAY_SOUND_LOOP, SoundsType.AMBIANCE_FORGE);
                EventManager.raise<SoundsType>(EventType.PLAY_SOUND_LOOP, SoundsType.MUSIQUE_HEROS);

                nextDay();
            }

        }

    }

    private void generatePNJ()
    {
        String rand1 = list1[UnityEngine.Random.Range(0,list1.Count)];
        String rand2 = list2[UnityEngine.Random.Range(0, list2.Count)];
        String rand3 = list3[UnityEngine.Random.Range(0, list3.Count)];
        String rand4 = list3[UnityEngine.Random.Range(0, list3.Count)];
        String texteQuete = listQuetes[UnityEngine.Random.Range(0, listQuetes.Count)];
        String PNJName = listNames[UnityEngine.Random.Range(0, listNames.Count)];
        bool isSecondChoiceGood = (UnityEngine.Random.Range(0,2) == 0);

        textRandom = PNJName+" :\nBonjour forgeron, peux-tu me conseiller en " + rand1 + " ?\nJe dois me battre contre "+rand2;  
        textOption1Random = "Donner une quète";
        option1Retour = new Menu(new List<Pair<Callback, String>> {new Pair<Callback,String>(queteRetour,"Continuer")},texteQuete);
        textOption2Random = rand1+" "+rand3;
        textOption3Random = rand1+" "+rand4;

        if (isSecondChoiceGood)
        {
            option2Retour = goodRetour;
            option3Retour = badRetour;
        }
        else
        {
            option2Retour = badRetour;
            option3Retour = goodRetour;
        }
    }

    private void option1()
    {
        EventManager.raise(EventType.MENU_EXIT);
        EventManager.raise<Menu>(EventType.MENU_ENTERED, option1Retour);
    }
    private void queteRetour()
    {
        EventManager.raise(EventType.MENU_EXIT);
        EventManager.raise<Menu>(EventType.MENU_ENTERED, new Menu(new List<Pair<Callback, String>> { new Pair<Callback,String>(()=> { exitVisitor(); EventManager.raise(EventType.MENU_EXIT); hero.player.addReputation(); hero.player.looseGold(); },"Finir journée")},PNJName+" :\nVous avez raison, je vais prendre votre quète.\nMerci !"));
    }
    private void option2()
    {
        EventManager.raise(EventType.MENU_EXIT);
        EventManager.raise<Menu>(EventType.MENU_ENTERED, option2Retour);
    }
    private void option3()
    {
        EventManager.raise(EventType.MENU_EXIT);
        EventManager.raise<Menu>(EventType.MENU_ENTERED, option3Retour);
    }
   


    void nextDay()
    {
        EventManager.raise(EventType.MENU_EXIT);
        hero.player.transform.position = new Vector3(0, -1, -2);
        currentDay++;
        //on annule le visiteur precedent
        visitor.transform.position = new Vector2(10, 10);
        visitor.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        //on regarde le visiteur acctuel
        if(hero.load(currentDay))
        {
           visitor = hero.gameObject;
           visitor.GetComponent<Animator>().Play("Down");
        }
        else
        {
            generatePNJ();
            myPNJ.GetComponent<PNJ>().setMenu(new List<Pair<Callback, String>> { new Pair<Callback, String>(option1, textOption1Random), new Pair<Callback, String>(option2, textOption2Random), new Pair<Callback, String>(option3, textOption3Random) }, textRandom);
            visitor = myPNJ;
        }
        //on set ce visiteur
        visitor.SetActive(true);
        visitor.transform.position = new Vector3(0,4,-2);
        visitor.transform.localScale = new Vector2(2, 2);
        visitor.GetComponent<Rigidbody>().velocity = new Vector3(0,-0.5f,0);
        EventManager.raise<SoundsType>(EventType.PLAY_SOUND_ONCE, SoundsType.PORTE_OUVERTE);
    }

    void exitVisitor()
    {
        visitor.GetComponent<Rigidbody>().velocity = new Vector3(0, 0.5f, 0);
    }
}
