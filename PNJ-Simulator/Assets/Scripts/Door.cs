using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    // Use this for initialization
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<PNJ>() != null)
            Destroy(other.gameObject);
        else if (other.gameObject.GetComponent<Hero>() != null)
            other.gameObject.SetActive(false);
        else if (other.gameObject.GetComponent<Player>() != null)
            EventManager.raise<ScenesType>(EventType.CHANGE_SCENE, ScenesType.MAP);
        else
            Destroy(other.gameObject);

    }
}
