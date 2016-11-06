using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {


    [SerializeField]
    private Sprite spriteDestructed;

    public GameObject spriteTitre;

    // Use this for initialization
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<PNJ>() != null)
            other.gameObject.transform.position = new Vector2(10, 10);
        else if (other.gameObject.GetComponent<Hero>() != null)
            other.gameObject.transform.position = new Vector2(10,10);
        else if (other.gameObject.GetComponent<Player>() != null)
        {
            EventManager.raise(EventType.TITRE_APPARAIT);
            StartCoroutine(showMapDuring(3));
        }
        else
            Destroy(other.gameObject);

    }

    IEnumerator showMapDuring(float sec)
    {
        spriteTitre.GetComponent<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(sec);
        EventManager.raise<ScenesType>(EventType.CHANGE_SCENE, ScenesType.MAP);
    }


    public void changeToDestructedSprite()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteDestructed;
    }
}
