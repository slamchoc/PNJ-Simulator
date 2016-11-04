using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private ScenesType currentType;
    [SerializeField] private GameObject player;


	// Use this for initialization
	void Start () {
        currentType = ScenesType.MAIN_MENU;
        DontDestroyOnLoad(this.gameObject);
        EventManager.addActionToEvent<ScenesType>(EventType.END_SCENE, OnSceneChanged);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnSceneChanged(ScenesType type)
    {
        currentType = type;
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<ScenesType>(EventType.END_SCENE, OnSceneChanged);
    }

    private void OnMenu()
    {

    }
}
