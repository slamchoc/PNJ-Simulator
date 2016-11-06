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
        EventManager.addActionToEvent(EventType.EVENTBEFOREWIN, cinematiqueEnd);
    }

    void cinematiqueEnd()
    {
        pnj.GetComponent<SpriteRenderer>().enabled = true;
        Menu bravo = new Menu(
                     new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=>
                                                                {
                                                                    EventManager.raise(EventType.MENU_EXIT);
                                                                    EventManager.raise(EventType.WIN);
                                                                }, "Finir")
                                                      },
                     "Mon dieu ! Mais tu as tué celui qui a écrasé le héros !\nMais tu n'es pourtant qu'un forgeron sans avenir...\nC'est..."
                 );
        EventManager.raise<Menu>(EventType.MENU_ENTERED, bravo);

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

        yield return new WaitForSeconds(5);
      
        EventManager.raise(EventType.CINEMATIC_ENDED);


        GameObject monster = UnityEngine.Object.Instantiate(prefabMonster);

        monster.GetComponent<Monster>().createMonster(25, toHide.transform.position, toHide.transform.position);
        monster.GetComponent<Monster>().bossFinal = true;
        monster.GetComponent<Monster>().launchBattle();

        toHide.GetComponent<SpriteRenderer>().enabled = false;
        toHide.transform.position = new Vector3(0, 0, 0);
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
