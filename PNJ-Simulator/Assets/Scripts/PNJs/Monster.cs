using UnityEngine;
using System.Collections;

public class Monster : PNJ
{
    /// <summary>
    /// Nombre de PVs du monstre
    /// </summary>
    private int nbPvs = 1;

    /// <summary>
    /// Points that the monster will cross
    /// </summary>
    [SerializeField]
    private Vector3 pointPatterA = new Vector3(0, 0, 0);
    [SerializeField]
    private Vector3 pointPatterB = new Vector3(0, 0, 0);

    [SerializeField]
    private float speed = 1.5f;

    public void createMonster(int _pvs, Vector3 _patternA, Vector3 _patternB)
    {
        nbPvs = _pvs;
        pointPatterA = _patternA;
        pointPatterB = _patternB;

        this.transform.position = pointPatterA;
    }

    void Start()
    {
        this.transform.position = pointPatterA;
        this.transform.GetComponent<Rigidbody>().velocity = pointPatterB - pointPatterA;
    }

    void Update()
    {
        // move through the pattern 

        if (Vector3.Distance(this.transform.position, pointPatterB) >= Vector3.Distance(pointPatterA, pointPatterB))
        {
            this.transform.GetComponent<Rigidbody>().velocity = (pointPatterB - pointPatterA) * speed;
        }
        else if (Vector3.Distance(this.transform.position, pointPatterA) >= Vector3.Distance(pointPatterA, pointPatterB))

        {
            this.transform.GetComponent<Rigidbody>().velocity = (pointPatterA - pointPatterB) * speed;
        }
    }

    /// <summary>
    /// Hit the monster by an amount of PVs
    /// </summary>
    /// <param name="pvRemoved"></param>
    public void hitMonster(int pvRemoved)
    {
        nbPvs =  nbPvs - pvRemoved;
        if (nbPvs < 0)
        {
            EventManager.raise(EventType.KILL_MONSTER, this.gameObject);
            EventManager.raise(EventType.MENU_EXIT);
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            //We save the monster
            DontDestroyOnLoad(this.gameObject);

            EventManager.raise<ScenesType>(EventType.CHANGE_SCENE, ScenesType.BATTLE);
            EventManager.addActionToEvent<ScenesType>(EventType.NEW_SCENE, sceneLoaded);
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        }
    }

    void sceneLoaded(ScenesType oldScene)
    {
        if(oldScene == ScenesType.BATTLE)
        {
            EventManager.removeActionFromEvent<ScenesType>(EventType.NEW_SCENE, sceneLoaded);

            EventManager.raise<Menu>(EventType.MENU_ENTERED, menu);
            EventManager.raise<SoundsType>(EventType.PLAY_SOUND_LOOP, SoundsType.MUSIQUE_PNJ);
            EventManager.addActionToEvent<int>(EventType.DAMAGE_ENNEMY, hitMonster);

        }
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<ScenesType>(EventType.NEW_SCENE, sceneLoaded);
        EventManager.removeActionFromEvent<int>(EventType.DAMAGE_ENNEMY, hitMonster);

    }

}
