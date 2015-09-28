using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UGUISceneLoader script.
/// This is used to show the progress of scene loading.
/// </summary>
public class UGUISceneLoader : MonoBehaviour
{
    public Image progressBar;
    public Text progressText;

    private AsyncOperation _async;

    void Start()
    {
        // Scene Load asynchronously.
        _async = Application.LoadLevelAsync(DataManager.Instance.sceneNameToLoad);
    }

    void Update()
    {
        // Scene loading process update.
        if (!_async.isDone)
        {
            float pProgress = _async.progress * 100f;
            int pInt = Mathf.RoundToInt(pProgress);
            progressText.text = pInt.ToString() + "%";
            progressBar.fillAmount = _async.progress;
        }
    }
}