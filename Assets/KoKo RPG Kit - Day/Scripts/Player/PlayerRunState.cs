using UnityEngine;
using System.Collections;

/// <summary>
/// Player's Run State Script. 
/// Process player's movement when the user click/touch the "Move" layer object (terrain etc).
/// </summary>
public class PlayerRunState : PlayerStateBase
{
    // time = distance to move / move speed;
    private float _timeToReach = 0f;
    // Elapsed time to move.
    private float _elapsedTime = 0f;

	void OnEnable()
    {
        // when this script is on, turn off attack marker and turn on move marker.
        _manager.playerData.attackMarker.gameObject.SetActive(false);
        _manager.playerData.moveMarker.gameObject.SetActive(true);

        // time = distance to move / move speed + offset;
        _timeToReach = Vector3.Distance(transform.position, _manager.playerData.moveMarker.position) / _manager.playerData.levelData.moveSpeed + 0.5f;
        _elapsedTime = 0f;
    }

    void Update()
    {
        // timer.
        _elapsedTime += Time.deltaTime;

        // exception handling.
        // when move time takes longer than expected time, change state to idle state.
        if (_elapsedTime > _timeToReach)
        {
            _manager.SetState(PlayerFSMManager.PlayerState.PlayerIdleState);
            return;
        }

        // Move to click/touch position.
        if (MoveUtility.MoveFrame(
            _manager.cc, 
            _manager.playerData.moveMarker,
            _manager.playerData.levelData.moveSpeed,
            _manager.playerData.levelData.turnSpeed) == 0f)
        {
            // if reached to target position, change state to Idle State.
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
        // when this script is off, turn off move marker.
        _manager.playerData.moveMarker.gameObject.SetActive(false);
    }
}