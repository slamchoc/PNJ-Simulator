using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    private ScenesType actualScene;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
        EventManager.addActionToEvent<ScenesType>(EventType.NEW_SCENE, sceneChanged);
        EventManager.addActionToEvent<Menu>(EventType.MENU_ENTERED, menuToPrint);
    }

    // Update is called once per frame
    void Update ()
    {
	    
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

    }

    void printDialogue(Menu _menu)
    {

    }

    void printBattle(Menu _menu)
    {

    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<ScenesType>(EventType.NEW_SCENE, sceneChanged);
        EventManager.removeActionFromEvent<Menu>(EventType.MENU_ENTERED, menuToPrint);
    }
}
