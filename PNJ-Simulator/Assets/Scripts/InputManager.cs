﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    [SerializeField] private Menu menu;
    [SerializeField] private Player player;

    [SerializeField] private ScenesType currentType;

    private float horizontal;
    private float vertical;
    private float attack1;
    private float attack2;
    private float splash;
    private float submit;

    private bool SelectedOptionChanged = false;
    private bool Validated = false;
    private bool HasPowerAttack = false;
    private bool HasNormalAttack = false;
    private bool HasSplashed = false;

    private bool isInCinematic = false;

    // Use this for initialization
    void Start () {
        currentType = ScenesType.MAIN_MENU;
        DontDestroyOnLoad(this.gameObject);
        EventManager.addActionToEvent<ScenesType>(EventType.NEW_SCENE, OnSceneChanged);
        EventManager.addActionToEvent<Menu>(EventType.MENU_ENTERED, OnMenuEntered);
        EventManager.addActionToEvent(EventType.MENU_EXIT, OnMenuExit);

        EventManager.addActionToEvent(EventType.CINEMATIC_BEGIN, ()=> { isInCinematic = true; });
        EventManager.addActionToEvent(EventType.CINEMATIC_ENDED, () => { isInCinematic = false; });
    }

    // Update is called once per frame
    void Update ()
    {

        if (!isInCinematic)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            attack1 = Input.GetAxis("Attack1");
            attack2 = Input.GetAxis("Attack2");
            splash = Input.GetAxis("Splash");
        }

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
                else if (player != null)
                    OnMap();
                break;
            case ScenesType.MAIN_MENU:
                if (menu != null)
                    OnMenu();
                break;
            case ScenesType.BATTLE:
                if (menu != null)
                    OnMenu();
                else if (player != null)
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
        player.move(0, 0);
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
                    menu.incrementPosition();
                else
                    menu.decrementPosition();
                SelectedOptionChanged = true;
            }
        }
        else
            SelectedOptionChanged = false;
        if (submit != 0 && !Validated)
        {
            menu.call();
            Validated = true;
        }
        else if (submit == 0)
            Validated = false;
    }

    private void OnMap()
    {
        player.move(horizontal, vertical);
        if (submit != 0 && !Validated)
        {
            player.interact();
            Validated = true;
        }
        else if (submit == 0)
            Validated = false;
    }

    private void OnBattle()
    {
        player.move(horizontal, vertical);
        if (attack1 != 0 && !HasNormalAttack)
        {
            player.attack1();
            HasNormalAttack = true;
        }
        else if (attack1 == 0)
            HasNormalAttack = false;
        if (attack2 != 0 && !HasPowerAttack)
        {
            player.attack2();
            HasPowerAttack = true;
        }
        else if (attack2 == 0)
        {
            HasPowerAttack = false;
        }
        if (splash != 0 && !HasSplashed)
        {
            player.splash();
            HasSplashed = true;
        }
        else if (splash == 0)
            HasSplashed = false;
    }
}
