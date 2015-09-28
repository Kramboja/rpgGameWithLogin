using UnityEngine;
using System.Collections;

/// <summary>
/// Player's Attack State script.
/// 1. Check if the distance between player and enemy is futher than attack range distance.
/// 2. if target enemy is dead, change state to Idle State.
/// </summary>
public class PlayerAttackState : PlayerStateBase
{
    void OnEnable()
    {
        // when this script is on, turn on attack marker.
        _manager.playerData.attackMarker.gameObject.SetActive(true);
    }

    void Update()
    {
        MoveUtility.RotateToDirBurst(transform, _manager.playerData.enemyFSM.transform);

        // check the distance between player and target enemy, and the distance is futher than attack range distacne
        // then change state to AttackRun State to chase target enemy again.
        if (Vector3.Distance(transform.position, _manager.playerData.enemyFSM.transform.position)
            > _manager.playerData.levelData.attackRange)
        {
            _manager.SetState(PlayerFSMManager.PlayerState.PlayerAttackRunState);
            return;
        }

        // check if target enemy is dead, if so change state to Idle State.
        if (_manager.playerData.enemyFSM.currentState == EnemyFSMManager.EnemyState.EnemyDeadState)
        {
            _manager.playerData.attackMarker.SetParent(null);
            _manager.SetState(PlayerFSMManager.PlayerState.PlayerIdleState);
            return;
        }

        // Exception handling ( Login state and Animation state should be matched )
        // when logic state and animation state are not matched then change state to skill.
        if (_manager.animator.GetCurrentAnimatorStateInfo(0).IsName("KK_Skill")
            && _manager.currentState != PlayerFSMManager.PlayerState.PlayerSkillState)
        {
            _manager.SetState(PlayerFSMManager.PlayerState.PlayerSkillState);
            return;
        }
    }

    void OnDisable()
    {
        // when this script is off, turn off attack marker.
        _manager.playerData.attackMarker.gameObject.SetActive(false);
    }
}