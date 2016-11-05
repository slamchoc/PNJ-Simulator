using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    [SerializeField]
    SceneManager sceneManager;

    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PNJ>() != null)
            Destroy(other.gameObject);
        else if (other.gameObject.GetComponent<Hero>() != null)
            other.gameObject.SetActive(false);
        else if (other.gameObject.GetComponent<Player>() != null)
            sceneManager.changeScene(ScenesType.MAP);
        else
            Destroy(other.gameObject);

    }
}
