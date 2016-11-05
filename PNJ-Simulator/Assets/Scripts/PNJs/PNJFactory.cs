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
        ;
    }

	// Update is called once per frame
	void Update ()
    {
	
	}

    void createPNJ(Vector3 position, Pair<Callback, String>[] menu, string text)
    {
        GameObject pnj =  Instantiate(prefabPNJ);
        pnj.GetComponent<PNJ>().setMenu(menu, text);
        pnj.transform.position = position;
    }

    void createMonster(Vector3 position, Pair<Callback, String>[] menu, string text, int pv,  Vector3 patternA, Vector3 patternB)
    {
        GameObject monster = Instantiate(prefabMonster);
        monster.GetComponent<Monster>().createMonster(pv, patternA, patternB);
        monster.GetComponent<Monster>().setMenu(menu, text);
        monster.transform.position = position;
    }
}
