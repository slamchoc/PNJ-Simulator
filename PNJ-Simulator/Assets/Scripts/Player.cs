using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum AttackType
{
    STRONG,
    WEAK,
    MIDDLE,
    MAGICAL
}
public enum Orientation { UP,DOWN,LEFT,RIGHT}

public class Player : MonoBehaviour {
    
    [SerializeField]
    private int lifePoint = 10;
    public int gold { get; private set; }
    public int reputation { get; private set; }
    private Orientation currentOrientation = Orientation.DOWN;
    [SerializeField]
    private float speed;
    [SerializeField]
    Animator animator;



    private int damagesAttackMagical = 0;

    private int damagesAttackStrong = 5;
    private int damagesAttackMiddle = 3;
    private int damagesAttackWeak = 1;


    void Start()
    {
        DontDestroyOnLoad(this);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        EventManager.addActionToEvent<ScenesType>(EventType.END_SCENE, sceneEnded);
        EventManager.addActionToEvent<ScenesType>(EventType.NEW_SCENE, sceneBegin);
        EventManager.addActionToEvent<int>(EventType.DAMAGE_PLAYER, damagesTaken);

        EventManager.addActionToEvent<AttackType>(EventType.ATTACK_ENNEMY, attack);
    }


    void OnDestroy()
    {
        EventManager.removeActionFromEvent<ScenesType>(EventType.END_SCENE, sceneEnded);
        EventManager.removeActionFromEvent<ScenesType>(EventType.NEW_SCENE, sceneBegin);
        EventManager.removeActionFromEvent<int>(EventType.DAMAGE_PLAYER, damagesTaken);

        EventManager.removeActionFromEvent<AttackType>(EventType.ATTACK_ENNEMY, attack);
    }

    void thinkToYourself(ScenesType lol)
    {
        Menu broomTaken = new Menu(
                               new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_EXIT); }, "..."),
                                                                },
                               "Balai récupéré !"
                           );
        Menu swordTaken = new Menu(
                               new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, broomTaken);}, "Je vais prendre le balai"),
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, broomTaken);}, "Je vais prendre le balai")
                                                                },
                               "Vous n'avez pas la force necessaire pour manier cette epee."
                           );

        Menu choice = new Menu(
                               new List<Pair<Callback, String>> {
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, broomTaken);}, "Je vais prendre le balai"),
                                                                new Pair<Callback, String>(()=> { EventManager.raise(EventType.MENU_EXIT); EventManager.raise<Menu>(EventType.MENU_ENTERED, swordTaken);}, "Je vais prendre l'epee"),
                                                                },
                               "Prendre le balai ou l'epee ?"
                           );

        EventManager.raise<Menu>(EventType.MENU_ENTERED, choice);
    }

    void attack(AttackType type)
    {
        int damages = 0;
        switch(type)
        {
            case AttackType.WEAK:
                damages = damagesAttackWeak;
                attack1();
                break;
            case AttackType.STRONG:
                damages = damagesAttackStrong;
                attack2();
                break;
            case AttackType.MAGICAL:
                damages = damagesAttackMagical;
                break;
            default:
                damages = 1;
                break;
        }

        EventManager.raise<int>(EventType.DAMAGE_ENNEMY, damages);
    }

    void damagesTaken(int damages)
    {
        lifePoint -= damages;
        if(damages > lifePoint/3)
        {
            EventManager.raise<SoundsType>(EventType.PLAY_SOUND_ONCE, SoundsType.PNJ_TOUCHE_FORT);
        }
        else
        {
            float rand = UnityEngine.Random.Range(0, 3);
            if (rand > 2)
                EventManager.raise<SoundsType>(EventType.PLAY_SOUND_ONCE, SoundsType.PNJ_TOUCHE1);
            else if (rand > 1)
                EventManager.raise<SoundsType>(EventType.PLAY_SOUND_ONCE, SoundsType.PNJ_TOUCHE2);
            else
                EventManager.raise<SoundsType>(EventType.PLAY_SOUND_ONCE, SoundsType.PNJ_TOUCHE3);
        }
      
        if (lifePoint <= 0)
        {
            EventManager.raise<SoundsType>(EventType.PLAY_SOUND_ONCE, SoundsType.MORT_PNJ);
            EventManager.raise(EventType.PLAYER_DEAD);
        }
    }

    void sceneEnded(ScenesType sceneEnded)
    {
        Debug.Log("scene ended " + sceneEnded);
        if (sceneEnded == ScenesType.MAIN_MENU)
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        else if (sceneEnded == ScenesType.SHOP)
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            EventManager.addActionToEvent<ScenesType>(EventType.NEW_SCENE, thinkToYourself);
        }
    }

    void sceneBegin(ScenesType sceneBegin)
    {
        Debug.Log("sceneBegin " + sceneBegin);
        if (sceneBegin == ScenesType.MAIN_MENU)
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        else if(sceneBegin == ScenesType.SHOP)
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void move (float dx, float dy)
    {
        if (dy > 0)
            currentOrientation = Orientation.UP;
        else if (dy < 0)
            currentOrientation = Orientation.DOWN;
        if (dx < 0)
            currentOrientation = Orientation.LEFT;
        else if (dx > 0)
            currentOrientation = Orientation.RIGHT;
        GetComponent<Rigidbody>().velocity = new Vector2(dx * speed, dy * speed);
        if (dx != 0 || dy != 0)
        {
            playAnimation();
        }

    }

    public void interact()
    {
        GameObject interactWith = null;
        Vector2 direction = new Vector2((currentOrientation == Orientation.LEFT) ? -1 : ((currentOrientation == Orientation.RIGHT) ? 1 : 0),
                                        (currentOrientation == Orientation.DOWN) ? -1 : ((currentOrientation == Orientation.UP) ? 1 : 0));

        RaycastHit hit = new RaycastHit();
        //Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(direction.x,direction.y,0) * 42f);
        if (Physics.Raycast(this.transform.position, direction, out hit, 1.5f))
            interactWith = hit.collider.gameObject;
        else
            return;

        PNJ interactScript = interactWith.GetComponent<PNJ>();
        Hero heroScript = interactWith.GetComponent<Hero>();

        if (interactScript != null)
        {
            interactScript.printMenu();
            return;
        }
        else if(heroScript != null)
        {
            heroScript.printnextMenu();
            return;
        }
    }

    public void attack1()
    {
        if (currentOrientation == Orientation.LEFT)
            animator.Play("LeftNormalAttack");
        else
            animator.Play("RightNormalAttack");
    }

    public void attack2()
    {
        if (currentOrientation == Orientation.LEFT)
            animator.Play("LeftPowerAttack");
        else
            animator.Play("RightPowerAttack");
    }

    public void splash()
    {
        if (currentOrientation == Orientation.LEFT)
        {
            animator.Play("LeftSplash");
        }
        else
        {
            animator.Play("RightSplash");
        }
    }

    public void playAnimation()
    {
        if (currentOrientation == Orientation.UP)
            animator.Play("WalkUp");
        else if (currentOrientation == Orientation.DOWN)
            animator.Play("WalkDown");
        else if (currentOrientation == Orientation.RIGHT)
            animator.Play("WalkRight");
        else if (currentOrientation == Orientation.LEFT)
            animator.Play("WalkLeft");
    }

    public void stopAnimation()
    {
        if (currentOrientation == Orientation.UP)
            animator.enabled = false;
        else if (currentOrientation == Orientation.RIGHT)
            animator.Play("RightIdle");
        else if (currentOrientation == Orientation.LEFT)
            animator.Play("LeftIdle");
    }

    public void addGold()
    {
        gold += (int)(100 + (reputation - 100) / 2);
    }

    public void addReputation()
    {
        reputation += (int)(reputation*0.1f);
    }

    public void looseReputation()
    {
        reputation -= (int)(reputation * 0.2f);
    }

    public void looseGold()
    {
        gold -= 100;
    }
}
