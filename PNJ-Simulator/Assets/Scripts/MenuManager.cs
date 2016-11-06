using UnityEngine;
using System.Collections.Generic;
using System;

public class MenuManager : MonoBehaviour
{
    private ScenesType actualScene;

    [SerializeField]
    private GameObject arrowMainMenu;
    [SerializeField]
    private GameObject arrowFight;
    [SerializeField]
    private GameObject arrowDialogue;
    [SerializeField]
    private GameObject panelMainMenu;
    [SerializeField]
    private GameObject panelFight;
    [SerializeField]
    private GameObject panelDialogue;
    [SerializeField]
    private GameObject textDialogue;

    [SerializeField]
    private SceneManager sceneManager;

    private Vector3 mainMenuStartPos;
    private Vector3 fightStartPos;
    private Vector3 dialogueStartPos;

    private Vector3 mainMenuOffset;
    private Vector3 fightOffset;
    private Vector3 dialogueOffset;

    private Menu mainMenu;

    Pair<Menu, GameObject> currentMenuActive = new Pair<Menu, GameObject>();

    List<GameObject> actualMenuPrinted = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
         mainMenu = new Menu
                                    (
                                        new List<Pair<Callback, String>> {
                                                                            new Pair<Callback,String>(()=> 
                                                                            {
                                                                                sceneManager.quitGame();
                                                                            }, "Exit"),
                                                                            new Pair<Callback,String>(()=> 
                                                                            {
                                                                                EventManager.raise(EventType.MENU_EXIT);
                                                                                sceneManager.changeScene(ScenesType.SHOP);
                                                                            },"Play")
                                                                        },
                                        "mainMenu"
                                    );
        DontDestroyOnLoad(this.gameObject);

        mainMenuStartPos = new Vector3(0, -2.1f, 1);
        mainMenuOffset = new Vector3(0, 2.1f, 0);

        fightStartPos = new Vector3(-6.5f, -2.1f, 1);
        fightOffset = new Vector3(0, 0.75f, 0);

        dialogueStartPos = new Vector3(-6.75f, -1f, 1);
        dialogueOffset = new Vector3(0, 0.75f, 0);

        actualScene = ScenesType.MAIN_MENU;

        EventManager.addActionToEvent<ScenesType>(EventType.NEW_SCENE, sceneChanged);
        EventManager.addActionToEvent<Menu>(EventType.MENU_ENTERED, menuToPrint);
        EventManager.addActionToEvent(EventType.MENU_EXIT,onMenuExit);
        EventManager.raise<Menu>(EventType.MENU_ENTERED, mainMenu);
    }

    // Update is called once per frame
    void Update ()
    {
        if (currentMenuActive != null)
        {
            Menu menu = currentMenuActive.First;
            GameObject arrow = currentMenuActive.Second;
            if (arrow == null)
                Debug.LogError("CRITICAL arrow == null !");

            if (actualScene == ScenesType.MAIN_MENU)
                arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x , mainMenuStartPos.y + menu.position * mainMenuOffset.y , arrow.transform.localPosition.z);
            else if(actualScene == ScenesType.BATTLE)
                arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x , fightStartPos.y + menu.position * fightOffset.y, arrow.transform.localPosition.z);
            else
                arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x , dialogueStartPos.y + menu.position * dialogueOffset.y , arrow.transform.localPosition.z);
            
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
        Vector3 panelPos = mainMenuStartPos;
        foreach (Pair<Callback, string> pair in _menu.options)
        {
            GameObject tmpPanel = Instantiate(panelMainMenu);
            tmpPanel.transform.parent = Camera.main.transform;

            tmpPanel.GetComponentInChildren<TextMesh>().text = pair.Second;
            tmpPanel.transform.localPosition = panelPos;
            panelPos += mainMenuOffset;
            actualMenuPrinted.Add(tmpPanel);
        }
        GameObject arrow = Instantiate(arrowMainMenu);
        arrow.transform.parent = Camera.main.transform;
        arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, arrow.transform.localPosition.y, 1);

        actualMenuPrinted.Add(arrow);
        currentMenuActive = new Pair<Menu, GameObject>(_menu, arrow);
    }

    void printDialogue(Menu _menu)
    {

        Vector3 dialogueScale = new Vector3(0.4f, 0.3f, 1);

        Vector3 panelPos = dialogueStartPos;
        List<GameObject> panels = new List<GameObject>();
        Vector2 size = new Vector2(0, 0);
        foreach (Pair<Callback, string> pair in _menu.options)
        {
            GameObject tmpPanel = Instantiate(panelDialogue);
            panels.Add(tmpPanel);
            tmpPanel.transform.parent = Camera.main.transform;
            tmpPanel.GetComponentInChildren<TextMesh>().text = pair.Second;
            Bounds tmp = tmpPanel.GetComponentInChildren<TextMesh>().GetComponent<Renderer>().bounds;
            if (tmp.size.x > size.x)
                size.x = tmp.size.x;
            if (tmp.size.y > size.y)
                size.y = tmp.size.y;
            tmpPanel.transform.localPosition = panelPos;

            tmpPanel.transform.localScale = new Vector3(tmpPanel.transform.localScale.x* dialogueScale.x, tmpPanel.transform.localScale.y * dialogueScale.y, tmpPanel.transform.localScale.z * dialogueScale.z);
            panelPos += dialogueOffset;
            actualMenuPrinted.Add(tmpPanel);
        }
        foreach (var panel in panels)
        {
            panel.GetComponentInChildren<TextMesh>().anchor = TextAnchor.MiddleLeft;
            panel.GetComponentInChildren<TextMesh>().transform.position += new Vector3(-1, 0, 0);
        }

        GameObject text = Instantiate(textDialogue);
        text.transform.parent = Camera.main.transform;
        text.transform.localPosition = new Vector3(0, -3.25f, 1);

        text.GetComponentInChildren<TextMesh>().text = _menu.text;
        text.GetComponentInChildren<TextMesh>().transform.localScale = new Vector3(0.75f, 1 + dialogueScale.y, 1);
        actualMenuPrinted.Add(text);

        GameObject arrow = Instantiate(arrowDialogue);
        arrow.transform.parent = Camera.main.transform;

        arrow.transform.localPosition = dialogueStartPos - new Vector3(1.75f,0,0);
        arrow.transform.localScale = new Vector3(arrow.transform.localScale.x * dialogueScale.x, arrow.transform.localScale.y * dialogueScale.y, arrow.transform.localScale.z * dialogueScale.z);
        actualMenuPrinted.Add(arrow);

        currentMenuActive = new Pair<Menu, GameObject>(_menu, arrow);
    }

    void printBattle(Menu _menu)
    {
        Vector3 battleScale = new Vector3(0.5f, 0.3f, 1);
        Vector3 panelPos = fightStartPos;
        if(_menu != null)
        {
            foreach (Pair<Callback, string> pair in _menu.options)
            {
                GameObject tmpPanel = Instantiate(panelFight);
                tmpPanel.transform.parent = Camera.main.transform;

                tmpPanel.GetComponentInChildren<TextMesh>().text = pair.Second;
                tmpPanel.transform.localPosition = panelPos;
                tmpPanel.transform.localScale = battleScale;
                panelPos += fightOffset;
                actualMenuPrinted.Add(tmpPanel);
            }
            GameObject arrow = Instantiate(arrowFight);
            arrow.transform.parent = Camera.main.transform;

            arrow.transform.localPosition = dialogueStartPos - new Vector3(1.5f, 0, 0);
            arrow.transform.localScale = battleScale;
            actualMenuPrinted.Add(arrow);
            currentMenuActive = new Pair<Menu, GameObject>(_menu, arrow);
        }
    
    }

    void onMenuExit()
    {
        for(int i = actualMenuPrinted.Count - 1; i >= 0; i-- )
            Destroy(actualMenuPrinted[i]);
        actualMenuPrinted.Clear();
        currentMenuActive = null;
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<ScenesType>(EventType.NEW_SCENE, sceneChanged);
        EventManager.removeActionFromEvent<Menu>(EventType.MENU_ENTERED, menuToPrint);
    }
}
