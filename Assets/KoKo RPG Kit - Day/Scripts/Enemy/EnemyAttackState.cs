using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy's Attack State script.
/// 1. Rotate to face player.
/// 2. check if the distance between player and enemy is futher than attack range distance.
/// 3. check if player is dead then, change state to Idle State.
/// </summary>
public class EnemyAttackState : EnemyStateBase
{
	void OnEnable()
    {

    }

    void Update()
    {
        // Rotate to face player's forward direction.
        MoveUtility.RotateToDirBurst(transform, _manager.enemyData.playerFSM.transform);

        // check if the distance between enemy and player is futher than attackrange.
        // then change state to AttackRun State to chase again.
        if (Vector3.Distance(transform.position, _manager.enemyData.playerFSM.transform.position)
            > _manager.enemyData.levelData.attackRange)
        {
            _manager.SetState(EnemyFSMManager.EnemyState.EnemyAttackRunState);
            return;
        }

        // if player is dead, change state to Idle State.
        if (_manager.enemyData.playerFSM.currentState == PlayerFSMManager.PlayerState.PlayerDeadState)
        {
            _manager.SetState(EnemyFSMManager.EnemyState.EnemyIdleState);
            return;
        }
    }

    void OnDisable()
    {

    }
}