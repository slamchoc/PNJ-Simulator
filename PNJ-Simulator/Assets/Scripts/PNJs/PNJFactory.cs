using UnityEngine;
using System.Collections.Generic;
using System;

public class PNJFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPNJ;

    [SerializeField]
    private GameObject prefabMonster;


    private List<MonsterToCreate> listMonsters = new List<MonsterToCreate>();
    private List<PNJToCreate> listPNJs = new List<PNJToCreate>();

    // Use this for initialization
    void Start()
    {
        if (FindObjectsOfType<PNJFactory>().Length > 1)
            Destroy(this.gameObject);
        else
        {
            DontDestroyOnLoad(this.gameObject);
            Debug.Log(listMonsters.Count);

            createPNJs();
            instantiatePNJs();
            EventManager.addActionToEvent<GameObject>(EventType.KILL_MONSTER, monsterHasbeenKilled);
            EventManager.addActionToEvent<ScenesType>(EventType.NEW_SCENE, newSceneLoaded);
        }
    }

    void newSceneLoaded(ScenesType newScene)
    {
        if(newScene == ScenesType.MAP)
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
        for (int i = 0; i < listMonsters.Count; i++)
            listMonsters[i].instantiateMonster(prefabMonster, i);

        foreach (PNJToCreate m in listPNJs)
            m.createPNJ(prefabPNJ);
    }

    private void createPNJs()
    {

        /******* Creation des PNJs *********/
        List<Pair<Callback, String>> listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() => {
            Debug.Log("?");
            EventManager.raise(EventType.MENU_EXIT);
        }, "Loultest"));
        string textPnj = "Je suis le texte du PNJ";
        listPNJs.Add( new PNJToCreate(new Vector3(-6, -6, -1), listCallbacks, textPnj));

        listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() => {
            Debug.Log("?");
            EventManager.raise(EventType.MENU_EXIT);
        }, "Loultest"));
        textPnj = "Je suis le texte du garde !";
        listPNJs.Add(new PNJToCreate(new Vector3(1.25f, 1.17f, -1), listCallbacks, textPnj));

        listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() =>
        {
            Debug.Log("?"); EventManager.raise(EventType.MENU_EXIT);
        }, "Loultest"));
        textPnj = "Je suis le texte du garde numero 2!";
        listPNJs.Add(new PNJToCreate(new Vector3(1.25f, 0.17f, -1), listCallbacks, textPnj));

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
        listMonsters.Add(new MonsterToCreate(listCallbacks, textMonster, 3, new Vector3(6,3,-2), new Vector3(9,3,-2)));

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
        listMonsters.Add(new MonsterToCreate(listCallbacks, textMonster, 3, new Vector3(10, 15, -2), new Vector3(9, 13, -2)));

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

    public void instantiateMonster(GameObject prefabMonster, int id)
    {
        if(!isDead)
        {
            GameObject monster = UnityEngine.Object.Instantiate(prefabMonster);
            monster.GetComponent<Monster>().createMonster(_pv, _patternA, _patternB);
            monster.GetComponent<Monster>().setMenu(_menu, _text);
            monster.GetComponent<Monster>().id = id;

            monster.transform.position = _patternA;
        }
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

    public void createPNJ(GameObject prefabPNJ)
    {
        if (!isDead)
        {
            GameObject pnj = UnityEngine.Object.Instantiate(prefabPNJ);
            pnj.GetComponent<PNJ>().setMenu(_menu, _text);
            pnj.transform.position = _position;
        }
    }
}