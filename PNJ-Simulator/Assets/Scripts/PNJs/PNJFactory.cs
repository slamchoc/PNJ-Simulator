﻿using UnityEngine;
using System.Collections.Generic;
using System;

public class PNJFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPNJ;

    [SerializeField]
    private GameObject prefabMonster;
    [SerializeField]
    private GameObject prefabMonsterEpic;

    public List<Sprite> PNJS;
    public List<Sprite> monstres;

    List<GameObject> listGO = new List<GameObject>();

    private List<MonsterToCreate> listMonsters = new List<MonsterToCreate>();
    private List<MonsterToCreate> listMonstersEpic = new List<MonsterToCreate>();

    private List<PNJToCreate> listPNJs = new List<PNJToCreate>();

    // Use this for initialization
    void Start()
    {
        if (FindObjectsOfType<PNJFactory>().Length > 1)
            Destroy(this.gameObject);
        else
        {
            DontDestroyOnLoad(this.gameObject);

            createPNJs();
            EventManager.addActionToEvent<GameObject>(EventType.KILL_MONSTER, monsterHasbeenKilled);
            EventManager.addActionToEvent<ScenesType>(EventType.NEW_SCENE, newSceneLoaded);
        }
    }

    void newSceneLoaded(ScenesType newScene)
    {
        if (newScene == ScenesType.MAP)
            instantiatePNJs();
    }

    void monsterHasbeenKilled(GameObject monster)
    {
        listMonsters[monster.GetComponent<Monster>().id].isDead = true;
        EventManager.raise(EventType.STOP_SOUND);
        EventManager.raise<ScenesType>(EventType.CHANGE_SCENE, ScenesType.MAP);
    }


    private void instantiatePNJs()
    {
        Debug.Log("Instantiate");

        for (int i = 0; i < listMonsters.Count; i++)
            listMonsters[i].instantiateMonster(prefabMonster ,i);
        for (int i = 0; i < listMonstersEpic.Count; i++)
            listMonstersEpic[i].instantiateMonster(prefabMonsterEpic, i);
        listGO.Clear();

        for (int i = 0; i < listPNJs.Count; i++)
        {
            listGO.Add(listPNJs[i].createPNJ(prefabPNJ, PNJS[i]));
        }
    }

    private void createPNJs()
    {
        Debug.Log("create pnj");
        listGO.Clear();

        /******* Creation des PNJs *********/
        List<Pair<Callback, String>> listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() => 
        {
            EventManager.raise(EventType.MENU_EXIT);
        }, "Fuir."));
        string textPnj = "Geneviève :\nMais que fais tu avec un balai dehors ?\nRentre chez toi, ce n'est pas ta place.";
        listPNJs.Add(new PNJToCreate(new Vector3(-6, -6, -1), listCallbacks, textPnj));


        listCallbacks = new List<Pair<Callback, String>>();
        Menu answerGarde = new Menu(
                               new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> 
                                                                {
                                                                    EventManager.raise(EventType.MENU_EXIT);
                                                                    listGO[1].GetComponent<Collider>().enabled = false;
                                                                    listGO[1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                                                                    listGO[1].GetComponent<Rigidbody>().velocity = new Vector3(5, 0, 0);
                                                                    Destroy(listGO[1], 5f);
                                                                }, "..."),
                                                                },
                               "Garde :\nC'est vrai ?! Bon, bah j'y vais, merci !"
                           );
        Menu gardequete = new Menu(
                               new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> 
                                                                {
                                                                    EventManager.raise(EventType.MENU_EXIT);
                                                                     EventManager.raise<Menu>(EventType.MENU_ENTERED, answerGarde);
                                                                }, "Continuer"),
                                                                },
                               "Je viens ici pour vous donner une quète !\nVous devez récuperer 20 plumes de Dorade des plaines,\nvous deviendrez riches !"
                           );

        listCallbacks.Add(new Pair<Callback, String>(() =>
        {
            EventManager.raise(EventType.MENU_EXIT);
        }, "D'accord j'ai compris..."));
        listCallbacks.Add(new Pair<Callback, String>(() =>
        {
            EventManager.raise(EventType.MENU_EXIT);
            EventManager.raise<Menu>(EventType.MENU_ENTERED, gardequete);
        }, "Donner une quète épique au garde."));
        textPnj = "Garde :\nTu ne peux pas sortir, désolé.\nTu n'es qu'un forgeron, c'est dangereux d'y aller tout seul.";
        listPNJs.Add(new PNJToCreate(new Vector3(3.65f, -3.81f, -1), listCallbacks, textPnj));


        listCallbacks = new List<Pair<Callback, String>>();
        Menu answerGarde2 = new Menu(
                               new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=>
                                                                {
                                                                    EventManager.raise(EventType.MENU_EXIT);
                                                                    listGO[2].GetComponent<Collider>().enabled = false;
                                                                    listGO[2].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                                                                    listGO[2].GetComponent<Rigidbody>().velocity = new Vector3(5, 0, 0);
                                                                    Destroy(listGO[2], 5f);
                                                                }, "..."),
                                                                },
                               "Garde :\nC'est vrai ?! Bon, bah j'y vais, merci !"
                           );
        Menu gardequete2 = new Menu(
                               new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=>
                                                                {
                                                                    EventManager.raise(EventType.MENU_EXIT);
                                                                     EventManager.raise<Menu>(EventType.MENU_ENTERED, answerGarde2);
                                                                }, "Continuer"),
                                                                },
                               "Je viens ici pour vous donner une quète !\nVous devez récuperer 50 pendules maléfiques\ndans la rivière aux noyés !"
                           );

        listCallbacks.Add(new Pair<Callback, String>(() =>
        {
            EventManager.raise(EventType.MENU_EXIT);
        }, "D'accord j'ai compris..."));
        listCallbacks.Add(new Pair<Callback, String>(() =>
        {
            EventManager.raise(EventType.MENU_EXIT);
            EventManager.raise<Menu>(EventType.MENU_ENTERED, gardequete2);
        }, "Donner une quète épique au garde."));
        textPnj = "Garde :\nTu ne peux pas sortir, désolé.\nTu n'es qu'un forgeron, c'est dangereux d'y aller tout seul.";
        listPNJs.Add(new PNJToCreate(new Vector3(3.65f, -2.39f, -1), listCallbacks, textPnj));


        listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() =>
        {
            EventManager.raise(EventType.MENU_EXIT);
        }, "Baisser les yeux"));
        textPnj = "Gerard :\nTu veux faire comme le heros,\nmais te battre avec un balai...\nT'es mignon mais...";
        listPNJs.Add(new PNJToCreate(new Vector3(-6, 1, -1), listCallbacks, textPnj));

        listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() =>
        {
            EventManager.raise(EventType.MENU_EXIT);
        }, "Ah ok."));
        textPnj = "Jean-Eudes :\nLe héros est vraiment impressionnant !\nIl va tous nous sauver !";
        listPNJs.Add(new PNJToCreate(new Vector3(-4, 12, -1), listCallbacks, textPnj));



        /************ Creation des monstres **********************/

        listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() => 
        {
            EventManager.raise<AttackType>(EventType.ATTACK_ENNEMY, AttackType.STRONG);
        }, "Attaque forte"));
        listCallbacks.Add(new Pair<Callback, String>(() =>
        {
            EventManager.raise<AttackType>(EventType.ATTACK_ENNEMY, AttackType.WEAK);
        }, "Attaque faible"));
        string textMonster = "??";
        listMonsters.Add(new MonsterToCreate(listCallbacks, textMonster, 3, new Vector3(50, 3,-2), new Vector3(55, 3,-2)));

        listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() =>
        {
            EventManager.raise<AttackType>(EventType.ATTACK_ENNEMY, AttackType.STRONG);
        }, "Attaque forte"));
        listCallbacks.Add(new Pair<Callback, String>(() =>
        {
            EventManager.raise<AttackType>(EventType.ATTACK_ENNEMY, AttackType.WEAK);
        }, "Attaque faible"));
        textMonster = "??";

        listMonsters.Add(new MonsterToCreate(listCallbacks, textMonster, 3, new Vector3(40, 15, -2), new Vector3(40, 0, -2)));

    }
}

class MonsterToCreate
{
    List<Pair<Callback, String>> _menu;
    string _text;
    int _pv;
    Vector3 _patternA;
    Vector3 _patternB;
    public bool isDead;

    public MonsterToCreate(List<Pair<Callback, String>> menu, string text, int pv, Vector3 patternA, Vector3 patternB)
    {
        isDead = false;
        _menu = menu;
        _text = text;
        _pv = pv;
        _patternA = patternA;
        _patternB = patternB;
    }

    public GameObject instantiateMonster(GameObject prefabMonster, int id)
    {
        if(!isDead)
        {
            GameObject monster = UnityEngine.Object.Instantiate(prefabMonster);

            monster.GetComponent<Monster>().createMonster(_pv, _patternA, _patternB);
            monster.GetComponent<Monster>().setMenu(_menu, _text);
            monster.GetComponent<Monster>().id = id;

            monster.transform.position = _patternA;
            return monster;
        }

        return null;
    }
}

class PNJToCreate
{
    Vector3 _position;
    List<Pair<Callback, String>> _menu;
    string _text;
    public bool isDead;

    public PNJToCreate(Vector3 position, List<Pair<Callback, String>> menu, string text)
    {
        isDead = false;
        _position = position;
        _menu = menu;
        _text = text;
    }

    public GameObject createPNJ(GameObject prefabPNJ, Sprite sprite)
    {
        if (!isDead)
        {
            GameObject pnj = UnityEngine.Object.Instantiate(prefabPNJ);
            pnj.GetComponent<SpriteRenderer>().sprite = sprite;
            pnj.GetComponent<PNJ>().setMenu(_menu, _text);
            pnj.transform.position = _position;
            return pnj;
        }
        return null;
    }
}