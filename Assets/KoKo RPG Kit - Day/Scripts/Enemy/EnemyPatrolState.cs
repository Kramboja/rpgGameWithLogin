using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy's Patrol State script.
/// 1. Move to target waypoint.
/// 2. if reached target waypoint, change state to Idle State.
/// 3. if player is detected at anytime, change state to AttackRun to chase player.
/// </summary>
public class EnemyPatrolState : EnemyStateBase
{
    // transform variable to get target waypoint reference.
    private Transform _target;
    // time = distance to move / move speed;
    private float _timeToReach = 0f;
    // Elapsed time to move.
    private float _elapsedTime = 0f;

    void OnEnable()
    {   
        if (_manager.enemyData.waypoints.Length > 0)
        {
            // get the target waypoint to patrol using Unity's Random.Range function.
            _target = _manager.enemyData.waypoints[Random.Range(0, _manager.enemyData.waypoints.Length)];
         
            // time = distance to move / move speed;
            _timeToReach = Vector3.Distance(transform.position, _target.position) / _manager.enemyData.levelData.walkSpeed;
            _elapsedTime = 0f;
        }
    }

    void Update()
    {
        // exception handling.
        // when move time takes longer than expected time, change state to idle state.
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _timeToReach)
        {
            _manager.SetState(EnemyFSMManager.EnemyState.EnemyIdleState);
            return;
        }

        // Move(Patrol) to target waypoint. If reached target waypoint, change state to Idle State.
        if (MoveUtility.MoveFrame(
            _manager.cc, 
            _target,
            _manager.enemyData.levelData.walkSpeed,
            _manager.enemyData.levelData.turnSpeed) == 0f)
        {
            _manager.SetState(EnemyFSMManager.EnemyState.EnemyIdleState);
            return;
        }

        // if player is detected, change state to AttackRun State to chase player.
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