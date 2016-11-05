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

    public GameObject cinematique;

    private bool cinematiqueBeforeShop = false;

	// Use this for initialization
	void Start ()
    {
        cinematique.SetActive(false);
        cinematique.transform.position = new Vector3(
            - cinematique.GetComponent<SpriteRenderer>().bounds.size.x / 2 - Camera.main.aspect * Camera.main.orthographicSize,
            0, 
            0
            );
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if(cinematiqueBeforeShop)
        {
            // cinematique
            cinematique.transform.position += new Vector3(
                       Time.deltaTime * 2.0f,
                       0,
                       0
                       );
            if (cinematique.transform.position.x > cinematique.GetComponent<SpriteRenderer>().bounds.size.x / 2 + Camera.main.aspect * Camera.main.orthographicSize)
            {
                cinematiqueBeforeShop = false;
                cinematique.SetActive(false);
                EventManager.raise(EventType.STOP_SOUND);
                goToShop();
            }
        }
    }

    /// <summary>
    /// Switch between the scenes
    /// </summary>
    /// <param name="newScene"></param>
    public void changeScene(ScenesType newScene)
    {
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
                cinematiqueBeforeShop = true;
                cinematique.SetActive(true);
                EventManager.raise<SoundsType>(EventType.PLAY_SOUND_ONCE, SoundsType.MUSIQUE_CINEMATIQUE2);
                break;
            default:
                Debug.Log("Probleme dans changeScene");
                break;
        }

        EventManager.raise<ScenesType>(EventType.NEW_SCENE, newScene);
    }

    void goToMainMenu()
    {
        EventManager.raise<ScenesType>(EventType.END_SCENE, actualScene);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }

    void goToBattle()
    {
        EventManager.raise<ScenesType>(EventType.END_SCENE, actualScene);
        UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScene");
    }

    void goToMap()
    {
        EventManager.raise<ScenesType>(EventType.END_SCENE, actualScene);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mapscene");
    }

    void goToShop()
    {
        EventManager.raise<ScenesType>(EventType.END_SCENE, actualScene);
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
