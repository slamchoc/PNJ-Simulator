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
	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
        EventManager.addActionToEvent<ScenesType>(EventType.END_SCENE, changeScene);
    }

    /// <summary>
    /// Switch between the scenes
    /// </summary>
    /// <param name="newScene"></param>
    void changeScene(ScenesType newScene)
    {
        switch(newScene)
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

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<ScenesType>(EventType.END_SCENE, changeScene);
    }
}
