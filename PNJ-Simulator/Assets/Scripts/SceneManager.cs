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
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

        DontDestroyOnLoad(this.gameObject);
        EventManager.addActionToEvent<ScenesType>(EventType.CHANGE_SCENE, changeScene);
        EventManager.addActionToEvent(EventType.PLAYER_DEAD, playerIsDead);

    }

    void Update()
    {
        if(sceneToLoad == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name && !eventRaised)
        {
            EventManager.raise<ScenesType>(EventType.NEW_SCENE, actualScene);

            switch (actualScene)
            {
                case ScenesType.MAIN_MENU:
                    EventManager.raise<SoundsType>(EventType.PLAY_SOUND_LOOP, SoundsType.MUSIQUE_HEROS);
                    break;
                case ScenesType.BATTLE:
                    EventManager.raise<SoundsType>(EventType.PLAY_SOUND_LOOP, SoundsType.MUSIQUE_PNJ);
                    break;
                case ScenesType.MAP:
                    EventManager.raise<SoundsType>(EventType.PLAY_SOUND_LOOP, SoundsType.AMBIANCE_VILLAGE);
                    EventManager.raise<SoundsType>(EventType.PLAY_SOUND_LOOP, SoundsType.MUSIQUE_VILLAGE);
                    EventManager.raise<SoundsType>(EventType.PLAY_SOUND_LOOP, SoundsType.SON_FOULE);
                    break;
                case ScenesType.SHOP:
                    EventManager.raise<SoundsType>(EventType.PLAY_SOUND_LOOP, SoundsType.AMBIANCE_FORGE);
                    break;
                default:
                    Debug.Log("Probleme dans changeScene");
                    break;
            }

            eventRaised = true;
        }
    }
    public void playerIsDead()
    {
        Debug.Log("Player DED");
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        Camera.main.transform.position = this.gameObject.transform.position;
        Camera.main.GetComponent<CameraFollow>().stopCamera = true;
        EventManager.raise(EventType.MENU_EXIT);
    }

    /// <summary>
    /// Switch between the scenes
    /// </summary>
    /// <param name="newScene"></param>
    public void changeScene(ScenesType newScene)
    {
        Debug.Log("Change to " + newScene);
        EventManager.raise<ScenesType>(EventType.END_SCENE, actualScene);
        EventManager.raise(EventType.STOP_SOUND);

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
