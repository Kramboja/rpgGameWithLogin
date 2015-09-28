using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy's Dead State script.
/// 1. Disable enemy gameobject after deadtime passed.
/// 2. Re-spawn enemy gameobject after respawn time passed.
/// </summary>
public class EnemyDeadState : EnemyStateBase
{
    private bool isInitialized = false;

    void OnEnable()
    {
        // avoid when this script is attached for the first time.
        if (!isInitialized)
        {
            isInitialized = true;
            return;
        }

        // Invoke SetDisable function after deadTime.
        Invoke("SetDisable", _manager.enemyData.deadTime);
    }

    void Update()
    {
        
    }

    void OnDisable()
    {

    }

    // Disable this gameobject and initialized values.
    void SetDisable()
    {
        _manager.SetState(EnemyFSMManager.EnemyState.EnemyIdleState);
        
        gameObject.SetActive(false);
        transform.position = _manager.enemyData.waypoints[0].position;
        transform.localRotation = Quaternion.identity;
        Invoke("Respawn", _manager.enemyData.respawnTime);
    }

    // turn on gameobject to respawn.
    void Respawn()
    {   
        gameObject.SetActive(true);
    }
}