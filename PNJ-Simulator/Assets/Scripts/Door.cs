using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {


    [SerializeField]
    private Sprite spriteDestructed;

    // Use this for initialization
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<PNJ>() != null)
            other.gameObject.transform.position = new Vector2(10, 10);
        else if (other.gameObject.GetComponent<Hero>() != null)
            other.gameObject.transform.position = new Vector2(10,10);
        else if (other.gameObject.GetComponent<Player>() != null)
        {
            EventManager.raise<ScenesType>(EventType.CHANGE_SCENE, ScenesType.MAP);
        }
        else
            Destroy(other.gameObject);

    }

    public void changeToDestructedSprite()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteDestructed;
    }
}
