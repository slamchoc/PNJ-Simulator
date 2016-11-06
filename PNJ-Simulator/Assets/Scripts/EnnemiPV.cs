using UnityEngine;
using System.Collections;

public class EnnemiPV : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        EventManager.addActionToEvent<int>(EventType.LOOSE_LIFE_ENNEMY, setPValue);
    }

    void setPValue(int newValue)
    {
        this.gameObject.GetComponentInChildren<TextMesh>().text = newValue.ToString();
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<int>(EventType.LOOSE_LIFE_ENNEMY, setPValue);
    }
}
