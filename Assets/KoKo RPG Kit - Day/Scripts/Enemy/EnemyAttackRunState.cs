using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy's AttackRun State script.
/// 1. chase player until the distance between player and enemy is near to attack range distance.
/// 2. if the distance is less than attack range, change state to Attack State to attack.
/// 3. if player run away so the enemy can't detect player, change state to Idle State.
/// </summary>
public class EnemyAttackRunState : EnemyStateBase
{
	void OnEnable()
    {

    }

    void Update()
    {
        // Chase Player until the distance is near to attack range distance.
        if (MoveUtility.MoveFrame(
            _manager.cc, 
            _manager.enemyData.playerFSM.transform, 
            _manager.enemyData.levelData.runSpeed,
            _manager.enemyData.levelData.turnSpeed) <= _manager.enemyData.levelData.attackRange)
        {
            _manager.SetState(EnemyFSMManager.EnemyState.EnemyAttackState);
            return;
        }

        // if the enemy can't detect player, change to Idle State.
        if (!_manager.IsDetectPlayer)
        {
            _manager.SetState(EnemyFSMManager.EnemyState.EnemyIdleState);
            return;
        }
    }

    void OnDisable()
    {

    }
}