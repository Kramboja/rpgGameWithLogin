using UnityEngine;

/// <summary>
/// UGUIOptionPopup script.
/// This is button clicked listener script for Option Popup button.
/// </summary>
public class UGUIOptionPopup : MonoBehaviour
{
    public GameObject quitPopup;

    // called When user clicked option popup button.
    // show option popup asking whether quit game or not.
	public void OnOptionButtonClicked()
    {
        Time.timeScale = !quitPopup.activeSelf ? 0f : 1f;
        quitPopup.SetActive(!quitPopup.activeSelf);
    }

    // when user clicked yes button, quit game.
    public void OnYesButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://www.google.com");
#else
        Application.Quit();
#endif
    }

    // when user clicked no button, play game again.
    public void OnNoButtonClicked()
    {
        Time.timeScale = 1f;
        quitPopup.SetActive(false);
    }
}