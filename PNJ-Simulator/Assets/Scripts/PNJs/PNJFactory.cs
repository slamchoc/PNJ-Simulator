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

    void createPNJ(Vector3 position, Delegate[] menu)
    {
        GameObject pnj =  Instantiate(prefabPNJ);
        pnj.GetComponent<PNJ>().setMenu(menu);
        pnj.transform.position = position;
    }

    void createMonster(Vector3 position, Delegate[] menu, int pv,  Vector3 patternA, Vector3 patternB)
    {
        GameObject monster = Instantiate(prefabMonster);
        monster.GetComponent<Monster>().Monster(pv, patternA, patternB);
        monster.GetComponent<Monster>().setMenu(menu);
        monster.transform.position = position;
    }
}
