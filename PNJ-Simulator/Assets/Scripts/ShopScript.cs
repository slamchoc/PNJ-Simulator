using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ShopScript : MonoBehaviour {

    public GameObject cinematique;
    [SerializeField]
    private GameObject table;
    [SerializeField]
    private GameObject stair;

    [SerializeField]
    private Hero hero;

    private Menu nextMenu;
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

        Menu tableMenu = new Menu(
                                        new List<Pair<Callback, String>> { new Pair<Callback, String>() }, ""
                                    );
        table.GetComponent<PNJ>().setMenu(tableMenu);

        nextDay();
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
                nextDay();
                return;
            }

        }
        visitor.transform.position = new Vector3(0, 5, -2);
        visitor.transform.localScale = new Vector2(2, 2);
        visitor.GetComponent<Rigidbody>().velocity = new Vector3(0,0.5f,0);
    }
}
