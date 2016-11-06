using UnityEngine;
using System.Collections;

public class Gold : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EventManager.addActionToEvent<int>(EventType.GOLD_CHANGE, setGoldValue);
	}

    void setGoldValue(int newValue)
    {
        this.gameObject.GetComponentInChildren<TextMesh>().text = newValue.ToString();
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<int>(EventType.GOLD_CHANGE, setGoldValue);
    }
}
