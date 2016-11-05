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

    private string sceneToLoad = "";

    private bool eventRaised = true;

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
        EventManager.addActionToEvent<ScenesType>(EventType.CHANGE_SCENE, changeScene);
        EventManager.addActionToEvent(EventType.PLAYER_DEAD, playerIsDead);

    }

    void Update()
    {
        if(sceneToLoad == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name && !eventRaised)
        {
            Debug.Log("raise");
            EventManager.raise<ScenesType>(EventType.NEW_SCENE, actualScene);
            eventRaised = true;
        }
    }
    public void playerIsDead()
    {

    }

    /// <summary>
    /// Switch between the scenes
    /// </summary>
    /// <param name="newScene"></param>
    public void changeScene(ScenesType newScene)
    {
        EventManager.raise<ScenesType>(EventType.END_SCENE, actualScene);
        eventRaised = false;
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
                goToShop();
                break;
            default:
                Debug.Log("Probleme dans changeScene");
                break;
        }
    }

    void goToMainMenu()
    {
        sceneToLoad = "MainMenuScene";

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }

    void goToBattle()
    {
        sceneToLoad = "BattleScene";

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }

    void goToMap()
    {
        sceneToLoad = "Mapscene";

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }

    void goToShop()
    {
        sceneToLoad = "ShopScene";
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }

    public void quitGame()
    {
        EventManager.raise(EventType.QUIT_GAME);
        Application.Quit();
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<ScenesType>(EventType.CHANGE_SCENE, changeScene);
        EventManager.removeActionFromEvent(EventType.PLAYER_DEAD, playerIsDead);

    }
}
