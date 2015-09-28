using UnityEngine;
using System.Collections;

/// <summary>
/// This is base script of Player's State script.
/// Hold PlayerFSMManager script reference because this is used very often.
/// </summary>
public class PlayerStateBase : MonoBehaviour
{
    protected PlayerFSMManager _manager;

    void Awake()
    {
        _manager = GetComponent<PlayerFSMManager>();
    }
}