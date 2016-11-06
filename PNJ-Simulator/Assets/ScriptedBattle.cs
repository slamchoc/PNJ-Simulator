using UnityEngine;
using System.Collections;


public enum ScriptedBattleType
{
    TPT,
    HERODIE
}

public class ScriptedBattle : MonoBehaviour
{
    public Vector3 positionplayer;

    public GameObject hero;

    public ScriptedBattleType typeBattle;

    void Start()
    {
        hero = FindObjectOfType<Hero>().gameObject;
    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            EventManager.raise(EventType.CINEMATIC_BEGIN);
            collision.gameObject.GetComponent<Player>().goToGoal(positionplayer);

            if(typeBattle == ScriptedBattleType.HERODIE)
            {
                hero.transform.position = new Vector3(10, 0, 0);
                hero.GetComponent<Animator>().Play("SecondBattle");
            }
            else
            {
                hero.transform.position = new Vector3(5, 0, 0);
                hero.GetComponent<Animator>().Play("FirstBattlePart1");
            }
        }
    }


}
