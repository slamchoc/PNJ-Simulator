using UnityEngine;
using System.Collections;

public class Monster : PNJ
{
    /// <summary>
    /// Nombre de PVs du monstre
    /// </summary>
    private int nbPvs = 1;

    public int id = 0;
    /// <summary>
    /// Points that the monster will cross
    /// </summary>
    [SerializeField]
    private Vector3 pointPatterA = new Vector3(0, 0, 0);
    [SerializeField]
    private Vector3 pointPatterB = new Vector3(0, 0, 0);

    [SerializeField]
    private float speed = 1.5f;

    [SerializeField]
    private float attackDelay = 2;

    private float timePassed;
    [SerializeField]
    private float timeAttack = 1.5f;

    private Player player;

    private Coroutine currentCoroutine = null;

    private Animator animator = null;

    private int damages = 3;

    private bool neverCollided = true;

    private bool attacking = false;

    private bool inBattle = false;

    public void createMonster(int _pvs, Vector3 _patternA, Vector3 _patternB)
    {
        neverCollided = true;
        nbPvs = _pvs;
        pointPatterA = _patternA;
        pointPatterB = _patternB;

        this.transform.position = pointPatterA;
    }

    void Start()
    {
        neverCollided = true;

        this.transform.position = pointPatterA;
        this.transform.GetComponent<Rigidbody>().velocity = pointPatterB - pointPatterA;
        animator = this.transform.GetComponent<Animator>();
    }

    void Update()
    {
        // move through the pattern 
        if (neverCollided)
        {
            if (Vector3.Distance(this.transform.position, pointPatterB) >= Vector3.Distance(pointPatterA, pointPatterB))
            {
                this.transform.GetComponent<Rigidbody>().velocity = (pointPatterB - pointPatterA) * speed;
                if (animator != null)
                {
                    if (this.transform.GetComponent<Rigidbody>().velocity.x < 0)
                        animator.Play("LeftMove");
                    else
                        animator.Play("Move");
                }

            }
            else if (Vector3.Distance(this.transform.position, pointPatterA) >= Vector3.Distance(pointPatterA, pointPatterB))

            {
                this.transform.GetComponent<Rigidbody>().velocity = (pointPatterA - pointPatterB) * speed;
                if (animator != null)
                {
                    if (this.transform.GetComponent<Rigidbody>().velocity.x < 0)
                        animator.Play("LeftMove");
                    else
                        animator.Play("Move");
                }
            }
        }
        else if(!attacking && inBattle)
        {
            pointPatterA = this.transform.position;
            pointPatterB = player.transform.position;
            if(Vector3.Distance(pointPatterA,pointPatterB)<= 2)
            {
                this.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                currentCoroutine = StartCoroutine(battleCoroutine());
            }
            else if (Vector3.Distance(this.transform.position, pointPatterB) >= Vector3.Distance(pointPatterA, pointPatterB))
            {
                this.transform.GetComponent<Rigidbody>().velocity = Vector3.Normalize(pointPatterB - pointPatterA) * speed;
                if (animator != null)
                {
                    if (this.transform.GetComponent<Rigidbody>().velocity.x < 0)
                        animator.Play("LeftMove");
                    else
                        animator.Play("Move");
                }

            }
            else if (Vector3.Distance(this.transform.position, pointPatterA) >= Vector3.Distance(pointPatterA, pointPatterB))
            {
                this.transform.GetComponent<Rigidbody>().velocity = Vector3.Normalize(pointPatterA - pointPatterB) * speed;
                if (animator != null)
                {
                    if (this.transform.GetComponent<Rigidbody>().velocity.x < 0)
                        animator.Play("LeftMove");
                    else
                        animator.Play("Move");
                }
            }
        }

    }

    /// <summary>
    /// Hit the monster by an amount of PVs
    /// </summary>
    /// <param name="pvRemoved"></param>
    public void hitMonster(int pvRemoved)
    {
        nbPvs =  nbPvs - pvRemoved;
        EventManager.raise<int>(EventType.LOOSE_LIFE_ENNEMY, nbPvs);
        if (nbPvs <= 0)
        {
            EventManager.raise(EventType.KILL_MONSTER, this.gameObject);
            EventManager.raise<SoundsType>(EventType.PLAY_SOUND_ONCE, SoundsType.MONSTRE_MORT);

            EventManager.raise(EventType.MENU_EXIT);
            Destroy(this.gameObject);
        }
    }

    void attack()
    {

        if (animator != null)
        {
            if (player.transform.position.x > this.transform.position.x)
                animator.Play("Attack");
            else
                animator.Play("LeftAttack");
        }
        EventManager.raise<int>(EventType.DAMAGE_PLAYER, damages);
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            player = collision.gameObject.GetComponent<Player>();
            if (neverCollided)
            {
                neverCollided = false;
                //We save the monster
                DontDestroyOnLoad(this.gameObject);

                EventManager.raise<ScenesType>(EventType.CHANGE_SCENE, ScenesType.BATTLE);
                EventManager.addActionToEvent<ScenesType>(EventType.NEW_SCENE, sceneLoaded);
                this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
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
            EventManager.raise<int>(EventType.LOOSE_LIFE_ENNEMY, nbPvs);
            inBattle = true;
        }
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<ScenesType>(EventType.NEW_SCENE, sceneLoaded);
        EventManager.removeActionFromEvent<int>(EventType.DAMAGE_ENNEMY, hitMonster);

    }

    IEnumerator battleCoroutine()
    {
        timePassed = 0;
        attack();
        attacking = true;
        while (timePassed < timeAttack)
        {
            this.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            timePassed += Time.deltaTime;
            yield return null;
        }
        timePassed = 0;
        while (timePassed < attackDelay)
        {
            this.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            timePassed += Time.deltaTime;
            yield return null;
        }
        currentCoroutine = null;
        attacking = false;
    }

}
