using UnityEngine;
using System.Collections.Generic;
using System;

public class PNJFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPNJ;

    [SerializeField]
    private GameObject prefabMonster;

    // Use this for initialization
    void Start ()
    {
        createPNJs();
    }

    private void createPNJs()
    {
        /******* Creation des PNJs *********/
        List<Pair<Callback, String>> listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() => { Debug.Log("?"); }, "Loultest"));
        string textPnj = "Je suis le texte du PNJ";

        createPNJ(new Vector3(0, 0, -1), listCallbacks, textPnj);

        /************ Creation des monstres **********************/
        listCallbacks = new List<Pair<Callback, String>>();
        listCallbacks.Add(new Pair<Callback, String>(() => 
        {
            Debug.Log("HIT");
            EventManager.raise<AttackType>(EventType.ATTACK_ENNEMY, AttackType.STRONG);
        }, "Attaque forte"));
        listCallbacks.Add(new Pair<Callback, String>(() =>
        {
            Debug.Log("HIT");
            EventManager.raise<AttackType>(EventType.ATTACK_ENNEMY, AttackType.WEAK);
        }, "Attaque faible"));

        string textMonster = "??";

        createMonster(new Vector3(0, 0, -1), listCallbacks, textMonster, 3, new Vector3(0,0,0), new Vector3(1,1,0));
    }

    void createPNJ(Vector3 position, List<Pair<Callback, String>> menu, string text)
    {
        GameObject pnj = Instantiate(prefabPNJ);
        pnj.GetComponent<PNJ>().setMenu(menu, text);
        pnj.transform.position = position;
    }

    void createMonster(Vector3 position, List<Pair<Callback, String>> menu, string text, int pv,  Vector3 patternA, Vector3 patternB)
    {
        GameObject monster = Instantiate(prefabMonster);
        monster.GetComponent<Monster>().createMonster(pv, patternA, patternB);
        monster.GetComponent<Monster>().setMenu(menu, text);
        monster.transform.position = position;
    }
}