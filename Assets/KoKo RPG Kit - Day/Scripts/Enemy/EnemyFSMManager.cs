using UnityEngine;
using System.Collections;

public class EnemyFSMManager : MonoBehaviour
{
    // EnemyState enum. Enum variable is equal to Enemy State Component's name.
    // variable name = component(script) name / enum number = animator state number.
    public enum EnemyState
    {
        EnemyIdleState = 0,
        EnemyPatrolState = 1,
        EnemyAttackRunState = 2,
        EnemyAttackState = 3,
        EnemyDeadState = 4,

        Length
    }

    public EnemyState currentState = EnemyState.EnemyIdleState;
    public CharacterController cc;
    public Animator animator;
    public EnemyData enemyData;
    public UGUIEnemyHUD enemyHUD;
    public MonoBehaviour[] fsmComponents;

    public Camera sight;

    void Awake()
    {
        // Get Components' References.
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        enemyData = GetComponent<EnemyData>();
        sight = GetComponentInChildren<Camera>();
        enemyHUD = GetComponentInChildren<UGUIEnemyHUD>();

        // Initialize FSM Component.
        InitComponents();
    }

    // Initialize FSM Component.
    void InitComponents()
    {
        // set the array size.
        fsmComponents = new MonoBehaviour[(int)EnemyState.Length];

        // Add State Script on this GameObject and fsmComponents array.
        for (int ix = 0; ix < (int)EnemyState.Length; ++ix)
        {

#if UNITY_5
            // UNITY 5 doesn't support add component by component's name but by it's type.
            string stateName = ((EnemyState)ix).ToString();
            System.Type stateType = System.Type.GetType(stateName);
            fsmComponents[ix] = gameObject.AddComponent(stateType) as MonoBehaviour;
#else
            // AddComponent with Component's name.
            fsmComponents[ix] = gameObject.AddComponent(((EnemyState)ix).ToString()) as MonoBehaviour;
#endif
            // Set all component disabled.
            fsmComponents[ix].enabled = false;
        }

        // Set Idle state compoentn enalbed.
        fsmComponents[(int)currentState].enabled = true;
    }

    // Change FSM State.
    public void SetState(EnemyState newState)
    {
        // Disable privous state component.
        fsmComponents[(int)currentState].enabled = false;
        // set new state.
        currentState = newState;
        // enable new state component.
        fsmComponents[(int)currentState].enabled = true;
        // change animation to new state animation.
        animator.SetInteger("state", (int)currentState);
    }

    // Check if the player is detected using camera component's frustum planes.
    public bool IsDetectPlayer
    {
        get
        {
            // Get Frustum info from camera attached.
            Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);

            // check if the player's bound is collided with frustum planes.
            return GeometryUtility.TestPlanesAABB(ps, enemyData.playerFSM.cc.bounds)
                && enemyData.playerFSM.currentState != PlayerFSMManager.PlayerState.PlayerDeadState;
        }
    }

    // Called from animation event set on EnemyAttack Animation.
    public void DamageTo()
    {
        // attack player.
        enemyData.playerFSM.Damage(enemyData.levelData.attack);
    }

    public void Damage(float damage)
    {
        // Apply Damage.

        float finalDamage = damage - enemyData.levelData.defence;
        finalDamage = finalDamage <= 0 ? 0 : finalDamage;

        enemyData.currentHP -= (int)finalDamage;
        enemyData.currentHP = enemyData.currentHP <= 0 ? 0 : enemyData.currentHP;

        // HUD UI Update.
        string damageString = finalDamage <= 0 ? "0" : "-" + ((int)finalDamage).ToString();
        enemyHUD.PrintDamage(damageString);

        float hpAmount = (float)enemyData.currentHP / (float)enemyData.levelData.maxHP;
        enemyHUD.SetHPBar(hpAmount);
        //enemyHUD.hpBar.fillAmount = (float)enemyData.currentHP / (float)enemyData.levelData.maxHP;

        // Dead state.
        if (enemyData.currentHP == 0)
        {
            // Player gets exp and gold when killed enemy.
            enemyData.playerFSM.GainEXP(enemyData.levelData.gainEXP);
            enemyData.playerFSM.GainGold(enemyData.levelData.gainGold);

            // change state to Dead State.
            SetState(EnemyState.EnemyDeadState);
            return;
        }

        // If player attacked this enemy and the enemy didn't detect player yet 
        // then rotate to player's direction and change state to AttackRun
        if (currentState != EnemyState.EnemyAttackState && currentState != EnemyState.EnemyAttackRunState)
        {
            MoveUtility.RotateToDirBurst(transform, enemyData.playerFSM.transform);
            SetState(EnemyState.EnemyAttackRunState);
            return;
        }
    }
}