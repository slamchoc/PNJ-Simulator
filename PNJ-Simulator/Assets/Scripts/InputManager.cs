using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    [SerializeField] private Menu menu;
    [SerializeField] private Player player;

    private ScenesType currentType;

    private float horizontal;
    private float vertical;
    private float attack1;
    private float attack2;
    private float splash;
    private float submit;

    private bool SelectedOptionChanged = false;


	// Use this for initialization
	void Start () {
        currentType = ScenesType.MAIN_MENU;
        DontDestroyOnLoad(this.gameObject);
        EventManager.addActionToEvent<ScenesType>(EventType.END_SCENE, OnSceneChanged);
        EventManager.addActionToEvent<Menu>(EventType.MENU_ENTERED, OnMenuEntered);
        EventManager.addActionToEvent(EventType.MENU_EXIT, OnMenuExit);
    }

    // Update is called once per frame
    void Update () {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        attack1 = Input.GetAxis("Attack1");
        attack2 = Input.GetAxis("Attack2");
        splash = Input.GetAxis("Splash");
        submit = Input.GetAxis("Submit");
        switch (currentType)
        {
            case ScenesType.MAP:
                if (menu != null)
                    OnMenu();
                else if (player != null)
                    OnMap();
                break;
            case ScenesType.SHOP:
                if (menu != null)
                    OnMenu();
                else if(player != null)
                    OnMap();
                break;
            case ScenesType.MAIN_MENU:
                if(menu != null)
                    OnMenu();
                break;
            case ScenesType.BATTLE:
                if(player!=null)
                    OnBattle();
                break;
            default:
                break;
        }

	}

    private void OnSceneChanged(ScenesType type)
    {
        currentType = type;
    }

    private void OnMenuEntered(Menu m)
    {
        menu = m;
    }

    private void OnMenuExit()
    {
        menu = null;
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<ScenesType>(EventType.END_SCENE, OnSceneChanged);
    }

    private void OnMenu()
    {
        if (vertical != 0)
        {
            if (!SelectedOptionChanged)
            {
                if (vertical > 0)
                    menu.decrementPosition();
                else
                    menu.incrementPosition();
                SelectedOptionChanged = true;
            }
        }
        else
            SelectedOptionChanged = false;
        if (submit != 0)
            menu.call();
    }

    private void OnMap()
    {
        player.move(horizontal, vertical);
        if (submit != 0)
            player.interact();
    }

    private void OnBattle()
    {
        player.move(horizontal, vertical);
        if (attack1 != 0)
            player.attack1();
        if (attack2 != 0)
            player.attack2();
        if (splash != 0)
            player.splash();
    }
}
