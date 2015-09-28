using UnityEngine;
using System.Collections;

/// <summary>
/// Player's Skill State script.
/// </summary>
public class PlayerSkillState : PlayerStateBase
{
    void OnEnable()
    {

    }

    void Update()
    {
        // when animation normalized play time is more than 0.93, change state to idle state.
        if (_manager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.93f)
        {
            _manager.SetState(PlayerFSMManager.PlayerState.PlayerIdleState);
            return;
        }

        // Exception handling.
        // when logic state and animation state is not matched, change state to idle state.
        if (!_manager.animator.GetCurrentAnimatorStateInfo(0).IsName("KK_Skill"))
        {
            _manager.SetState(PlayerFSMManager.PlayerState.PlayerIdleState);
            return;
        }
    }

    void OnDisable()
    {

    }
}