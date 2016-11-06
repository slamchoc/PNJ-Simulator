using UnityEngine;
using System.Collections;

public class PlayerPV : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        EventManager.addActionToEvent<int>(EventType.LOOSE_LIFE_PLAYER, setPValue);
    }

    void setPValue(int newValue)
    {
        this.gameObject.GetComponentInChildren<TextMesh>().text = newValue.ToString();
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<int>(EventType.LOOSE_LIFE_PLAYER, setPValue);
    }
}
