using UnityEngine;
using System.Collections;

/// <summary>
/// Different types of scenes
/// </summary>
public enum ScenesType
{
    SHOP,
    MAP,
    MAIN_MENU,
    BATTLE
}

/// <summary>
/// Manager of the scenes
/// </summary>
public class SceneManager : MonoBehaviour
{
    ScenesType actualScene = ScenesType.MAIN_MENU;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

    }

    /// <summary>
    /// Switch between the scenes
    /// </summary>
    /// <param name="newScene"></param>
    public void changeScene(ScenesType newScene)
    {
        EventManager.raise<ScenesType>(EventType.END_SCENE, actualScene);

        actualScene = newScene;

        switch (newScene)
        {
            case ScenesType.MAIN_MENU:
                goToMainMenu();
                break;
            case ScenesType.BATTLE:
                goToBattle();
                break;
            case ScenesType.MAP:
                goToMap();
                break;
            case ScenesType.SHOP:
                Debug.Log("DONT FORGET TO CHANGE DAT");
                goToMap();
                break;
            default:
                Debug.Log("Probleme dans changeScene");
                break;
        }

        EventManager.raise<ScenesType>(EventType.NEW_SCENE, newScene);
    }

    void goToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }

    void goToBattle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScene");
    }

    void goToMap()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mapscene");
    }

    void goToShop()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ShopScene");
    }

    public void quitGame()
    {
        EventManager.raise(EventType.QUIT_GAME);
        Application.Quit();
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<ScenesType>(EventType.END_SCENE, changeScene);
    }
}
