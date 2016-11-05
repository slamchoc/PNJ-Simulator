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
            Debug.Log("add !");
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
       // listMonsters.Remove(monster);
        EventManager.raise(EventType.STOP_SOUND);
        EventManager.raise<ScenesType>(EventType.CHANGE_SCENE, ScenesType.MAP);
    }


    private void instantiatePNJs()
    {
        foreach (MonsterToCreate m in listMonsters)
            m.instantiateMonster(prefabMonster);

        foreach (PNJToCreate m in listPNJs)
            m.createPNJ(prefabPNJ);
    }

    private void createPNJs()
    {
        Debug.Log("createPNJ");

        /******* Creation des PNJs *********/
        List<Pair<Callback, String>> listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() => { Debug.Log("?"); }, "Loultest"));
        string textPnj = "Je suis le texte du PNJ";

        listPNJs.Add( new PNJToCreate(new Vector3(-3, -3, -1), listCallbacks, textPnj));

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

        listMonsters.Add(new MonsterToCreate(listCallbacks, textMonster, 3, new Vector3(-3,3,0), new Vector3(3,3,0)));
    }

  
}

class MonsterToCreate
{
    List<Pair<Callback, String>> _menu;
    public string _text;
    int _pv;
    Vector3 _patternA;
    Vector3 _patternB;

    public MonsterToCreate(List<Pair<Callback, String>> menu, string text, int pv, Vector3 patternA, Vector3 patternB)
    {
        _menu = menu;
        _text = text;
        _pv = pv;
        _patternA = patternA;
        _patternB = patternB;
    }

    public GameObject instantiateMonster(GameObject prefabMonster)
    {
        GameObject monster = UnityEngine.Object.Instantiate(prefabMonster);
        monster.GetComponent<Monster>().createMonster(_pv, _patternA, _patternB);
        monster.GetComponent<Monster>().setMenu(_menu, _text);
        monster.transform.position = _patternA;

        return monster;
    }
}

class PNJToCreate
{
    Vector3 _position;
    List<Pair<Callback, String>> _menu;
    public string _text;

    public PNJToCreate(Vector3 position, List<Pair<Callback, String>> menu, string text)
    {
        _position = position;
        _menu = menu;
        _text = text;
    }

    public GameObject createPNJ(GameObject prefabPNJ)
    {
        GameObject pnj = UnityEngine.Object.Instantiate(prefabPNJ);
        pnj.GetComponent<PNJ>().setMenu(_menu, _text);
        pnj.transform.position = _position;
        return pnj;
    }
}