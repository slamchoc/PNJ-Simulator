using UnityEngine;
using System.Collections.Generic;
using System;

public class PNJFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPNJ;

    [SerializeField]
    private GameObject prefabMonster;


    private List<GameObject> listMonsters = new List<GameObject>();
    private List<GameObject> listPNJs = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        Debug.Log("Start");
        createPNJs();
        EventManager.addActionToEvent<GameObject>(EventType.KILL_MONSTER, monsterHasbeenKilled);
    }

    void monsterHasbeenKilled(GameObject monster)
    {
        listMonsters.Remove(monster);
        EventManager.raise(EventType.STOP_SOUND);
        EventManager.raise<ScenesType>(EventType.CHANGE_SCENE, ScenesType.MAP);
    }

    private void createPNJs()
    {
        /******* Creation des PNJs *********/
        List<Pair<Callback, String>> listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() => { Debug.Log("?"); }, "Loultest"));
        string textPnj = "Je suis le texte du PNJ";

        listPNJs.Add( createPNJ(new Vector3(-3, -3, -1), listCallbacks, textPnj));

        /************ Creation des monstres **********************/

        listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() => 
        {
            EventManager.raise<AttackType>(EventType.ATTACK_ENNEMY, AttackType.STRONG);
        }, "Attaque forte"));
        listCallbacks.Add(new Pair<Callback, String>(() =>
        {
            Debug.Log("HIT");
            EventManager.raise<AttackType>(EventType.ATTACK_ENNEMY, AttackType.WEAK);
        }, "Attaque faible"));

        string textMonster = "??";

        listMonsters.Add(createMonster(listCallbacks, textMonster, 3, new Vector3(-3,3,0), new Vector3(3,3,0)));
    }

    GameObject createPNJ(Vector3 position, List<Pair<Callback, String>> menu, string text)
    {
        GameObject pnj = Instantiate(prefabPNJ);
        pnj.GetComponent<PNJ>().setMenu(menu, text);
        pnj.transform.position = position;

        return pnj;
    }

    GameObject createMonster(List<Pair<Callback, String>> menu, string text, int pv,  Vector3 patternA, Vector3 patternB)
    {
        GameObject monster = Instantiate(prefabMonster);
        monster.GetComponent<Monster>().createMonster(pv, patternA, patternB);
        monster.GetComponent<Monster>().setMenu(menu, text);
        monster.transform.position = patternA;

        return monster;
    }
}