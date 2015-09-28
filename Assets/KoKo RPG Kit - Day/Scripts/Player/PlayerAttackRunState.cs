using UnityEngine;
using System.Collections;

/// <summary>
/// Player's AttackRun State script.
/// Chase the enemy clicked/touched.
/// </summary>
public class PlayerAttackRunState : PlayerStateBase
{
    void OnEnable()
    {
        // when this script is on, turn off move marker and turn on attack marker.
        _manager.playerData.moveMarker.gameObject.SetActive(false);
        _manager.playerData.attackMarker.gameObject.SetActive(true);
    }

    void Update()
    {
        // Move to target enemy untill the distance between player and enemy is near attack range distance.
        // then change state to Attack State.
        if (MoveUtility.MoveFrame(
            _manager.cc, 
            _manager.playerData.attackMarker,
            _manager.playerData.levelData.moveSpeed, 
            _manager.playerData.levelData.turnSpeed) < _manager.playerData.levelData.attackRange)
        {
            _manager.SetState(PlayerFSMManager.PlayerState.PlayerAttackState);
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
        
    }
}