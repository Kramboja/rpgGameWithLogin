using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy's Idle State script.
/// 1. Wait until waitTime
/// 2. if the _elapsedTime passed waitTime then, change to Patrol State.
/// 3. if player is detected at anytime then, chase player to attack.
/// </summary>
public class EnemyIdleState : EnemyStateBase
{
    // time variable to check elapsed time.
    private float _elapsedTime = 0f;

	void OnEnable()
    {
        // when this script is on, set _elapsedTime value is 0 to initialize.
        _elapsedTime = 0f;
    }

    void Update()
    {
        // Timer.
        _elapsedTime += Time.deltaTime;

        // if _elapsedTime is greater than waitTime, change state to Patrol State.
        if (_elapsedTime > _manager.enemyData.disableTime)
        {
            _manager.SetState(EnemyFSMManager.EnemyState.EnemyPatrolState);
            return;
        }

        // if player is detected, change state to AttackRun to chase player.
        if (_manager.IsDetectPlayer)
        {
            _manager.SetState(EnemyFSMManager.EnemyState.EnemyAttackRunState);
            return;
        }
    }

    void OnDisable()
    {

    }
}