using UnityEngine;
using System.Collections;

public enum ScenesType
{
    SHOP,
    MAP,
    MAIN_MENU,
    BATTLE
}

public class SceneManager : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
        subscribeForSceneChanging();
	}
	
    /// <summary>
    /// Toutes les inscriptions aux changements de scene
    /// </summary>
	private void subscribeForSceneChanging()
    {
        EventManager.addActionToEvent<ScenesType>(EventType.END_SCENE, changeScene);
    }

    void changeScene(ScenesType newScene)
    {

    }


    void goToMainMenu()
    {

    }

    void goToBattle()
    {

    }

    void goToMap()
    {

    }

    void goToShop()
    {

    }


}
