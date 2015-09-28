using UnityEngine;
using System.Collections;

/// <summary>
/// This is base script of Enemy's State script.
/// Hold EnemyFSMManager script reference because this is used very often.
/// </summary>
public class EnemyStateBase : MonoBehaviour
{
    protected EnemyFSMManager _manager;

    void Awake()
    {
        _manager = GetComponent<EnemyFSMManager>();
    }
}