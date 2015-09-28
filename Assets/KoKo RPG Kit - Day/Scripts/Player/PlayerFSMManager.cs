using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

public class PlayerFSMManager : MonoBehaviour
{
    // PlayerState enum. Enum variable is equal to Player State Component's name.
    // variable name = component(script) name / enum number = animator state number.
    public enum PlayerState
    {
        PlayerIdleState = 0,
        PlayerRunState = 1,
        PlayerAttackRunState = 2,
        PlayerAttackState = 3,
        PlayerDeadState = 4,
        PlayerSkillState = 5,

        Length
    }

    public PlayerState currentState = PlayerState.PlayerIdleState;
    public CharacterController cc;
    public Animator animator;
    public PlayerData playerData;
    public GameObject[] effects;
    public MonoBehaviour[] fsmComponents;

    private int _layermask;

    void Awake()
    {
        // Get Components' References.
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerData = GetComponent<PlayerData>();

        // Set layermask for raycast.
        _layermask = LayerMask.GetMask("Block", "Move", "Enemy");

        // Initialize FSM Componenents.
        InitComponents();
    }

    // Initialize FSM Component.
    void InitComponents()
    {
        // set the array size.
        fsmComponents = new MonoBehaviour[(int)PlayerState.Length];

        // Add State Script on this GameObject and fsmComponents array.
        for (int ix = 0; ix < (int)PlayerState.Length; ++ix)
        {

#if UNITY_5
            // UNITY 5 doesn't support AddComponent by component's name but by it's type.
            string stateName = ((PlayerState)ix).ToString();
            System.Type stateType = System.Type.GetType(stateName);
            fsmComponents[ix] = gameObject.AddComponent(stateType) as MonoBehaviour;
#else
            // AddComponent with Component's name.
            fsmComponents[ix] = gameObject.AddComponent(((PlayerState)ix).ToString()) as MonoBehaviour;
#endif
            // Set all component disabled.
            fsmComponents[ix].enabled = false;
        }

        // Set Idle state compoentn enalbed.
        fsmComponents[(int)currentState].enabled = true;
    }

    // The Character moves by user's mouse/touch input.
    // mouse/touch input process.
    void Update()
    {
        // if player is dead, doesn't do input process.
        if (currentState == PlayerState.PlayerDeadState || currentState == PlayerState.PlayerSkillState)
            return;

        // If the mouse/touch 
#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE
        if (EventSystem.current.IsPointerOverGameObject())
#elif UNITY_ANDROID || UNITY_IPHONE
        if (EventSystem.current.IsPointerOverGameObject(0))
#endif
            return;

        // Process mouse/touch process.
        if (Input.GetMouseButtonDown(0))
        {
            // Get raycast infomation from mouse/touch position.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // shoot raycast with the ray info calculated above.
            if (Physics.Raycast(ray, out hit, 100f, _layermask))
            {
                // get layer from hit object.
                int layer = hit.transform.gameObject.layer;

                // if the hit layer is equal to "Move", then process move.
                if (layer == LayerMask.NameToLayer("Move"))
                {
                    // turn off attack marker.
                    playerData.attackMarker.gameObject.SetActive(false);

                    // set move marker's position and turn on move marker.
                    Vector3 markerPos = hit.point;
                    markerPos.y += 0.1f;
                    playerData.moveMarker.position = markerPos;
                    playerData.moveMarker.gameObject.SetActive(true);

                    // change state to RunState.
                    SetState(PlayerState.PlayerRunState);
                }

                // if the hit layer is equal to "Enemy" then process Attack.
                else if (layer == LayerMask.NameToLayer("Enemy"))
                {
                    // get EnemyFSMManager reference from hit object.
                    playerData.enemyFSM = hit.transform.GetComponent<EnemyFSMManager>();

                    // if the enemy is dead, does nothing.
                    if (playerData.enemyFSM.currentState == EnemyFSMManager.EnemyState.EnemyDeadState)
                        return;

                    // set attack marker's position and turn on attack marker.
                    playerData.attackMarker.SetParent(hit.transform);
                    playerData.attackMarker.localPosition = new Vector3(0f, 0.1f, 0f);
                    playerData.attackMarker.localScale = Vector3.one * 0.5f;
                    playerData.attackMarker.gameObject.SetActive(false);
                    
                    // turn off move marker.
                    playerData.moveMarker.gameObject.SetActive(false);

                    // change state to AttackRun State to chase the enemy.
                    SetState(PlayerState.PlayerAttackRunState);
                }
            }
        }
    }

    // Change FSM State.
    public void SetState(PlayerState newState)
    {
        fsmComponents[(int)currentState].enabled = false;
        currentState = newState;
        fsmComponents[(int)currentState].enabled = true;
        animator.SetInteger("state", (int)currentState);
    }

    // Called from animation event set on PlayerAttack animation.
    public void DamageTo()
    {
        // apply damage to enemy only when player is alive ( hp > 0 )
        if (playerData.currentHP <= 0)
            return;

        // attack enemy.
        playerData.enemyFSM.Damage(playerData.levelData.baseAttack);
    }

    public void Damage(float damage)
    {
        // Apply damage.
        playerData.currentHP -= (int)damage;
        playerData.currentHP = playerData.currentHP <= 0 ? 0 : playerData.currentHP;

        // Dead State.
        if (playerData.currentHP == 0)
        {
            SetState(PlayerState.PlayerDeadState);
            return;
        }
    }

    // Called when skill button clicked.
    public void UseSkill()
    {
        if (currentState != PlayerState.PlayerDeadState)
        {
            SetState(PlayerState.PlayerSkillState);
            return;
        }
    }

    // Area attack. Skill attack.
    public void AreaAttack()
    {
        // if player's hp is less than zero, do not apply damage to enemy
        // -> player is in dead state.
        if (playerData.currentHP <= 0)
            return;

        // Find all game objects which has tag name "Enemy".
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int ix = 0; ix < enemies.Length; ++ix)
        {
            // Calculate distance between player and enemy.
            float distance = Vector3.Distance(transform.position, enemies[ix].transform.position);
            // Get the EnemyFSMManager reference.
            EnemyFSMManager enemyFSM = enemies[ix].transform.GetComponent<EnemyFSMManager>();

            // check state if attackable.
            if (distance <= playerData.levelData.skillAttackRange
                && enemyFSM.currentState != EnemyFSMManager.EnemyState.EnemyDeadState)
            {
                // Apply Skill Damage to Enemy.
                enemies[ix].GetComponent<EnemyFSMManager>().Damage(playerData.levelData.skillAttack);
            }
        }
    }

    // Start Effect by enabling effect game object.
    void StartEffect(string effect)
    {
        int index = GetEffect(effect);
        if (index == -1)
            return;

        effects[index].SetActive(false);
        effects[index].SetActive(true);
    }

    // Stop effect by disabling effect game object.
    void StopEffect(string effect)
    {
        int index = GetEffect(effect);
        if (index == -1)
            return;

        effects[index].SetActive(false);
    }

    // Get effect gameobject's index from effects array.
    int GetEffect(string effect)
    {
        for (int ix = 0; ix < effects.Length; ++ix)
        {
            if (effect == effects[ix].name)
            {
                return ix;
            }
        }

        return -1;
    }

    // When player kiiled enemy EnemyFSMManager.Damage() called this function.
    public void GainEXP(int gainEXP)
    {
        playerData.GainEXP(gainEXP);
    }

    // When player kiiled enemy EnemyFSMManager.Damage() called this function.
    public void GainGold(int gainGold)
    {
        playerData.GainGold(gainGold);
    }

    // Show Level up effect.
    public void LevelUp()
    {
        StartEffect("SD_Level_up_G");
    }
}