using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum ScriptedBattleType
{
    TPT,
    HERODIE
}

public class ScriptedBattle : MonoBehaviour
{
    public Vector3 positionplayer;

    public GameObject hero;

    public GameObject pnj;

    public GameObject prefabMonster;

    public ScriptedBattleType typeBattle;

    string nameAnimator = "";

    float timeBeforeNext = 0;
    float timeLaunchAnim = 0;

    bool firstTime = true;

    void Start()
    {
        hero = FindObjectOfType<Hero>().gameObject;
        DontDestroyOnLoad(pnj);
    }


    // Update is called once per frame
    void Update ()
    {
        if (nameAnimator == "FirstBattlePart2" && Time.time > timeBeforeNext + timeLaunchAnim && firstTime)
        {
            nameAnimator = "";
            hero.GetComponent<Animator>().Play("FirstBattlePart1");
            firstTime = false;
            Menu think = new Menu(
                      new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=>
                                                                {
                                                                    EventManager.raise(EventType.MENU_EXIT);
                                                                    timeLaunchAnim = Time.time;
                                                                    nameAnimator = "FirstBattlePart2";
                                                                    hero.GetComponent<Animator>().Play(nameAnimator);
                                                                }, "Continuer")
                                                       },
                      "(* C'est donc ça un combat... *)"
                  );

            Menu battle = new Menu(
                        new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> 
                                                                {
                                                                    EventManager.raise(EventType.MENU_EXIT);
                                                                    EventManager.raise<Menu>(EventType.MENU_ENTERED, think);
                                                                }, "Continuer")
                                                         },
                        "Héros :\nAh, vil démon ! C'est à ton tour de m'attaquer,\nje suis prêt à encaisser ton coup !"
                    );

          

            EventManager.raise<Menu>(EventType.MENU_ENTERED, battle);
        }
        else if (nameAnimator == "FirstBattlePart2" && Time.time > timeBeforeNext + timeLaunchAnim && !firstTime)
        {
            nameAnimator = "FirstBattlePart3";
            hero.GetComponent<Animator>().Play(nameAnimator);
        }
        else if (Time.time< timeBeforeNext + timeLaunchAnim)
        {
            EventManager.raise(EventType.STOP_SOUND);

            EventManager.raise<SoundsType>(EventType.PLAY_SOUND_ONCE, SoundsType.COUP_EPEE);
            EventManager.raise<SoundsType>(EventType.PLAY_SOUND_ONCE, SoundsType.COUP_EPEE_ECLAIR);
        }

        if (nameAnimator == "FirstBattlePart3" && hero.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FirstBattlePart3"))
        {
            nameAnimator = "FirstBattlePart4";
            hero.GetComponent<Animator>().Play(nameAnimator);

            Menu think = new Menu(
                 new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=>
                                                                {
                                                                    EventManager.raise(EventType.MENU_EXIT);
                                                                    EventManager.raise(EventType.CINEMATIC_ENDED);
                                                                }, "Continuer")
                                                  },
                 "(* Il se bat quand même bizarrement... *)"
             );

            Menu battle = new Menu(
                        new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=>
                                                                {
                                                                    EventManager.raise(EventType.MENU_EXIT);
                                                                    EventManager.raise<Menu>(EventType.MENU_ENTERED, think);
                                                                    hero.GetComponent<Animator>().Play("Up");
                                                                    this.GetComponent<Collider>().enabled = false;
                                                                    hero.GetComponent<Rigidbody>().velocity = new Vector3(0,2,0);
                                                                    StartCoroutine(waithThenHide(hero));
                                                                }, "Continuer")
                                                         },
                        "Héros :\nEt voilà pour toi, je t'ai pourfendu ! Allez, au suivant."
                    );

            EventManager.raise<Menu>(EventType.MENU_ENTERED, battle);

            EventManager.raise(EventType.CINEMATIC_ENDED);
        }
    }


    private IEnumerator waithThenHide(GameObject toHide)
    {
        yield return new WaitForSeconds(5);
        toHide.GetComponent<SpriteRenderer>().enabled = false;

        toHide.transform.position = new Vector3(0, 0, 0);
        Destroy(this.gameObject);
    }

    private IEnumerator laucnhbattle(GameObject toHide)
    {
        this.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(5);
      
        EventManager.raise(EventType.CINEMATIC_ENDED);


        GameObject monster = UnityEngine.Object.Instantiate(prefabMonster);

        monster.GetComponent<Monster>().createMonster(25, this.gameObject.transform.position, this.gameObject.transform.position);
        monster.GetComponent<Monster>().bossFinal = true;
        monster.GetComponent<Monster>().neverCollided = false;
        monster.GetComponent<Monster>().player = toHide.GetComponent<Hero>().player;

        monster.GetComponent<Monster>().launchBattle();

        toHide.GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            EventManager.raise(EventType.CINEMATIC_BEGIN);
            collision.gameObject.GetComponent<Player>().goToGoal(positionplayer);

            if(typeBattle == ScriptedBattleType.HERODIE)
            {
                hero.transform.position = this.transform.position;
                hero.GetComponent<SpriteRenderer>().enabled = true;
                hero.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

                nameAnimator = "SecondBattle";
                hero.GetComponent<Animator>().Play(nameAnimator);
                StartCoroutine(laucnhbattle(hero));
            }
            else
            {
                hero.transform.position = this.transform.position;
                hero.GetComponent<SpriteRenderer>().enabled = true;
                hero.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                nameAnimator = "FirstBattlePart2";
                timeBeforeNext = 3.0f;
                timeLaunchAnim = Time.time;

                hero.GetComponent<Animator>().Play(nameAnimator);
            }


            this.GetComponent<Collider>().enabled = false;
        }
    }


}
