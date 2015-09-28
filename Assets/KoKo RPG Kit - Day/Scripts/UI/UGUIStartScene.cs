using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// UGUIStartScene script.
/// Start Scene's UI Button Lisntener script.
/// </summary>
public class UGUIStartScene : MonoBehaviour
{
    public Button goToAnimCheckButton;
    public Button goToGamestartButton;
    public Button quitButton;

    // Initialization.
    void Awake()
    {
        // Set button's click listener.
        goToGamestartButton.onClick.AddListener(OnGameStartButtonClicked);
        goToAnimCheckButton.onClick.AddListener(OnAnimCheckButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    // When user clicked Game Start Button, Load "2.Village" Scene.
    public void OnGameStartButtonClicked()
    {
        DataManager.Instance.SceneLoad("2.Village");
    }

    // When user clicked Animation Check Button, Load "Character Animation" Scene.
    public void OnAnimCheckButtonClicked()
    {
        DataManager.Instance.SceneLoad("Character Animation");
    }

    // When user clicked Game Quit Button, then quit game.
    public void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://www.google.com");
#else
        Application.Quit();
#endif
    }
}