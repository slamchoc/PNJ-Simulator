using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ShopScript : MonoBehaviour {

    public GameObject cinematique;
    [SerializeField]
    private GameObject stair;

    [SerializeField]
    private Hero hero;

    /// <summary>
    /// the current visitor of the shop
    /// </summary>
    private GameObject visitor;

    private int currentDay = 0;

    private bool needCinematique = true;

    // Use this for initialization
    void Start ()
    {
        EventManager.raise<SoundsType>(EventType.PLAY_SOUND_ONCE, SoundsType.MUSIQUE_CINEMATIQUE2);
        cinematique.SetActive(false);
        cinematique.transform.position = new Vector3(
            -cinematique.GetComponent<SpriteRenderer>().bounds.size.x / 2 - Camera.main.aspect * Camera.main.orthographicSize,
            0,
            0
            );

        Menu stairMenu = new Menu(
                                        new List<Pair<Callback, String>> { new Pair<Callback, String>(nextDay,"dormir") }, "(* Enfin, la journee se termine *)"
                                    );
        stair.GetComponent<PNJ>().setMenu(stairMenu);
        EventManager.addActionToEvent(EventType.END_DAY, nextDay);
        //TODO c'est degueux, trouver autre chose !
        hero = FindObjectOfType<Hero>();
        hero.player.transform.localScale = new Vector2(2, 2);
        hero.player.transform.position = new Vector3(0, -1, -2);
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent(EventType.END_DAY, nextDay);
    }

    // Update is called once per frame
    void Update ()
    {
        if(needCinematique)
        {
            cinematique.SetActive(true);

            // cinematique
            cinematique.transform.position += new Vector3(
                       Time.deltaTime * 2.0f,
                       0,
                       0
                       );
            if (cinematique.transform.position.x > cinematique.GetComponent<SpriteRenderer>().bounds.size.x / 2 + Camera.main.aspect * Camera.main.orthographicSize)
            {
                needCinematique = false;
                cinematique.SetActive(false);
                EventManager.raise(EventType.STOP_SOUND);
                nextDay();
            }

        }

    }

    private GameObject generatePNJ()
    {
        return null;
    }


    void nextDay()
    {
        currentDay++;
        if(hero.load(currentDay))
        {
            visitor = hero.gameObject;
        }
        else
        {
            visitor = generatePNJ();
            if (visitor == null)
            {
                return;
            }

        }
        visitor.SetActive(true);
        visitor.transform.position = new Vector3(0, 5, -2);
        visitor.transform.localScale = new Vector2(2, 2);
        visitor.GetComponent<Rigidbody>().velocity = new Vector3(0,-0.5f,0);
    }

    void exitVisitor()
    {
        visitor.GetComponent<Rigidbody>().velocity = new Vector3(0, 0.5f, 0);
    }
}
