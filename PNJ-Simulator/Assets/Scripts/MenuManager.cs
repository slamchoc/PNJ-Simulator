﻿using UnityEngine;
using System.Collections.Generic;
using System;

public class MenuManager : MonoBehaviour
{
    private ScenesType actualScene;

    [SerializeField]
    private GameObject arrowFight;
    [SerializeField]
    private GameObject arrowDialogue;
    [SerializeField]
    private GameObject panelFight;
    [SerializeField]
    private GameObject panelDialogue;

    private Menu mainMenu = new Menu
                                    (
                                        new List<Pair<Callback, String>> {
                                                                            new Pair<Callback,String>(()=> { Debug.Log("game"); },"Game"),
                                                                            new Pair<Callback,String>(()=> { Debug.Log("choix alt"); },"Alt"),
                                                                            new Pair<Callback,String>(()=> { Application.Quit(); }, "Exit")
                                                                        },
                                        "mainMenu"
                                    );

    private List<Pair<Menu,GameObject>> currentMenuActive = new List<Pair<Menu,GameObject>>();

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
        EventManager.addActionToEvent<ScenesType>(EventType.NEW_SCENE, sceneChanged);
        EventManager.addActionToEvent<Menu>(EventType.MENU_ENTERED, menuToPrint);
        EventManager.raise<Menu>(EventType.MENU_ENTERED, mainMenu);
        //menuToPrint(mainMenu);
    }

    // Update is called once per frame
    void Update ()
    {
	    foreach(Pair<Menu,GameObject> menu in currentMenuActive)
        {
            // TODO : Gerer les differentes positions de départ des differents menus
            menu.Second.transform.position = new Vector3(menu.Second.transform.position.x, -menu.First.position * 2.1f, menu.Second.transform.position.z);
            //menu.Second.transform.position = new Vector3(menu.Second.transform.position.x , menu.Second.transform.position.y + menu.First.position, menu.Second.transform.position.z);
        }
	}

    void sceneChanged(ScenesType newScene)
    {
        actualScene = newScene;
    }

    void menuToPrint(Menu _menu)
    {
        switch(actualScene)
        {
            case ScenesType.MAIN_MENU:
                printMainMenu(_menu);
                break;
            case ScenesType.BATTLE:
                printBattle(_menu);
                break;
            case ScenesType.MAP:
                printDialogue(_menu);
                break;
            case ScenesType.SHOP:
                printDialogue(_menu);
                break;
            default:
                Debug.Log("Probleme dans menuToPrint");
                break;
        }
    }

    void printMainMenu(Menu _menu)
    {
        Vector3 panelPos = new Vector3(0, 0, 0);
        Vector3 offset = new Vector3(0, -2*panelFight.transform.localScale.y - 0.1f, 0);
        foreach (Pair<Callback, string> pair in _menu.options)
        {
            GameObject tmpPanel = Instantiate(panelFight);
            tmpPanel.GetComponentInChildren<TextMesh>().text = pair.Second;
            tmpPanel.transform.position = panelPos;
            panelPos += offset;
        }
        GameObject arrow = Instantiate(arrowFight);

        currentMenuActive.Add(new Pair<Menu, GameObject>(_menu, arrow));
    }

    void printDialogue(Menu _menu)
    {
        Vector3 panelPos = new Vector3(0, 0, 0);
        Vector3 offset = new Vector3(0, -2*panelFight.transform.localScale.y - 0.1f, 0);
        foreach (Pair<Callback, string> pair in _menu.options)
        {

            GameObject tmpPanel = Instantiate(panelFight);
            tmpPanel.GetComponentInChildren<TextMesh>().text = pair.Second;
            tmpPanel.transform.position = panelPos;
            panelPos += offset;
        }
        GameObject arrow = Instantiate(arrowFight);

        currentMenuActive.Add(new Pair<Menu, GameObject>(_menu, arrow));
    }

    void printBattle(Menu _menu)
    {
        Vector3 panelPos = new Vector3(0, 0, 0);
        Vector3 offset = new Vector3(0, - 2*panelFight.transform.localScale.y -0.1f, 0);
        foreach (Pair<Callback,string> pair in _menu.options)
        {
            GameObject tmpPanel = Instantiate(panelFight);
            tmpPanel.GetComponentInChildren<TextMesh>().text = pair.Second;
            tmpPanel.transform.position = panelPos;
            panelPos += offset;
        }
        GameObject arrow = Instantiate(arrowFight);

        currentMenuActive.Add(new Pair<Menu, GameObject>(_menu, arrow));

    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<ScenesType>(EventType.NEW_SCENE, sceneChanged);
        EventManager.removeActionFromEvent<Menu>(EventType.MENU_ENTERED, menuToPrint);
    }
}
