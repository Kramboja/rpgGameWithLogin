using UnityEngine;
using System.Collections;

/// <summary>
/// Player's Idle State Script. 
/// </summary>
public class PlayerIdleState : PlayerStateBase
{
    void OnEnable()
    {

    }

    void Update()
    {
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