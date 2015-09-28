using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UGUIMonsterAnimPlayer : MonoBehaviour
{
    public enum State
    {
        Idle, Walk, AttackRun, Attack, Damage, Skill, Dead, Length
    }

    public Button idleButton;
    public Button walkButton;
    public Button attackrunButton;
    public Button attackButton;
    public Button damageButton;
    public Button skillButton;
    public Button deadButton;

    public Animator[] monsterAnimators;

    void Awake()
    {
        idleButton.onClick.AddListener(OnClickedMonsterIdle);
        walkButton.onClick.AddListener(OnClickedMonsterWalk);
        attackrunButton.onClick.AddListener(OnClickedMonsterAttackRun);
        attackButton.onClick.AddListener(OnClickedMonsterAttack);
        damageButton.onClick.AddListener(OnClickedMonsterDamage);
        skillButton.onClick.AddListener(OnClickedMonsterSkill);
        deadButton.onClick.AddListener(OnClickedMonsterDead);
    }

    public void OnClickedMonsterIdle()
    {
        SetAnimation(State.Idle);
    }

    public void OnClickedMonsterWalk()
    {
        SetAnimation(State.Walk);
    }

    public void OnClickedMonsterAttackRun()
    {
        SetAnimation(State.AttackRun);
    }

    public void OnClickedMonsterAttack()
    {
        SetAnimation(State.Attack);
    }

    public void OnClickedMonsterDamage()
    {
        SetAnimation(State.Damage);
    }

    public void OnClickedMonsterSkill()
    {
        SetAnimation(State.Skill);
    }

    public void OnClickedMonsterDead()
    {
        SetAnimation(State.Dead);
    }

    void SetAnimation(State newState)
    {
        foreach (Animator anim in monsterAnimators)
        {
            anim.SetInteger("state", (int)newState);
        }
    }
}