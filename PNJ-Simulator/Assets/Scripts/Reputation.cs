using UnityEngine;
using System.Collections;

public class Reputation : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        EventManager.addActionToEvent<int>(EventType.REPUTATION_CHANGE, setReputationValue);
    }

    void setReputationValue(int newValue)
    {
        this.gameObject.GetComponentInChildren<TextMesh>().text = newValue.ToString();
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<int>(EventType.REPUTATION_CHANGE, setReputationValue);
    }
}
