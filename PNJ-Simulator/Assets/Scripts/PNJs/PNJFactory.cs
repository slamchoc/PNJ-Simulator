using UnityEngine;
using System.Collections.Generic;
using System;

public class PNJFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPNJ;

    [SerializeField]
    private GameObject prefabMonster;


    List<GameObject> listGO = new List<GameObject>();

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

            createPNJs();
            EventManager.addActionToEvent<GameObject>(EventType.KILL_MONSTER, monsterHasbeenKilled);
            EventManager.addActionToEvent<ScenesType>(EventType.NEW_SCENE, newSceneLoaded);
        }
    }

    void newSceneLoaded(ScenesType newScene)
    {
        Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

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
            listMonsters[i].instantiateMonster(prefabMonster, i);
        listGO.Clear();

        foreach (PNJToCreate m in listPNJs)
        {
            listGO.Add(m.createPNJ(prefabPNJ));
        }
    }

    private void createPNJs()
    {
        Debug.Log("create pnj");
        listGO.Clear();


        /******* Creation des PNJs *********/
        List<Pair<Callback, String>> listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() => {
            Debug.Log(listGO[0]);
            EventManager.raise(EventType.MENU_EXIT);
            listGO[0].GetComponent<Collider>().enabled = false;
            listGO[0].GetComponent<Rigidbody>().velocity = new Vector3(1, 0,0);
        }, "OK."));
        string textPnj = "Bla bla";
        listPNJs.Add( new PNJToCreate(new Vector3(-6, -6, -1), listCallbacks, textPnj));

        listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() => {
            Debug.Log(listGO[1]);
            EventManager.raise(EventType.MENU_EXIT);
            listGO[1].GetComponent<Collider>().enabled = false;
            listGO[1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            listGO[1].GetComponent<Rigidbody>().velocity = new Vector3(5, 0, 0);
            Destroy(listGO[1], 5f);
        }, "Donner une quete au Garde"));
        textPnj = "Vous ne passerz pas.";
        listPNJs.Add(new PNJToCreate(new Vector3(0.63f, 0.5f, -1), listCallbacks, textPnj));

        listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() =>
        {
            Debug.Log(listGO[2]);
            EventManager.raise(EventType.MENU_EXIT);
            listGO[2].GetComponent<Collider>().enabled = false;
            listGO[2].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            listGO[2].GetComponent<Rigidbody>().velocity = new Vector3(5, 0, 0);
            Destroy(listGO[2], 5f);

        }, "Donner une quete au Garde"));
        textPnj = "Vous ne passerz pas.";
        listPNJs.Add(new PNJToCreate(new Vector3(0.63f, 1.7f, -1), listCallbacks, textPnj));

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

    public GameObject createPNJ(GameObject prefabPNJ)
    {
        if (!isDead)
        {
            GameObject pnj = UnityEngine.Object.Instantiate(prefabPNJ);
            pnj.GetComponent<PNJ>().setMenu(_menu, _text);
            pnj.transform.position = _position;
            return pnj;
        }
        return null;
    }
}