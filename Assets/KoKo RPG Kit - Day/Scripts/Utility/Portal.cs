using UnityEngine;
using System.Collections;

/// <summary>
/// Portal Script.
/// This is used to change Scene.
/// </summary>
public class Portal : MonoBehaviour
{
    public string sceneToLoad;

    void OnTriggerEnter(Collider other)
    {
        DataManager.Instance.SceneLoad(sceneToLoad);
    }
}