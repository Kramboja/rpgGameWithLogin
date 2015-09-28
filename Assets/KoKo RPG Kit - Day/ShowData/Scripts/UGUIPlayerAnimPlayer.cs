using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UGUIPlayerAnimPlayer : MonoBehaviour
{
    public enum State
    {
        Idle, Run, AttackRun, Attack, AttackStandby, Combo, DrawBlade, PutBlade, Damage, Skill, Dead, Length
    }

    public Button idleButton;
    public Button runButton;
    public Button attackrunButton;
    public Button attackButton;
    public Button attackStandbyButton;
    public Button comboButton;
    public Button drawBladeButton;
    public Button putBladeButton;
    public Button damageButton;
    public Button skillButton;
    public Button deadButton;

    public Animator playerAnimator;

    void Awake()
    {
        idleButton.onClick.AddListener(OnClickedPlayerIdle);
        runButton.onClick.AddListener(OnClickedPlayerWalk);
        attackrunButton.onClick.AddListener(OnClickedPlayerAttackRun);
        attackButton.onClick.AddListener(OnClickedPlayerAttack);
        attackStandbyButton.onClick.AddListener(OnClickedPlayerAttackStandby);
        comboButton.onClick.AddListener(OnClickedPlayerCombo);
        drawBladeButton.onClick.AddListener(OnClickedPlayerDrawBlade);
        putBladeButton.onClick.AddListener(OnClickedPlayerPutBlade);
        damageButton.onClick.AddListener(OnClickedPlayerDamage);
        skillButton.onClick.AddListener(OnClickedPlayerSkill);
        deadButton.onClick.AddListener(OnClickedPlayerDead);
    }

    public void OnClickedPlayerIdle()
    {
        playerAnimator.SetInteger("state", (int)State.Idle);
    }

    public void OnClickedPlayerWalk()
    {
        playerAnimator.SetInteger("state", (int)State.Run);
    }

    public void OnClickedPlayerAttackRun()
    {
        playerAnimator.SetInteger("state", (int)State.AttackRun);
    }

    public void OnClickedPlayerAttack()
    {
        playerAnimator.SetInteger("state", (int)State.Attack);
    }

    public void OnClickedPlayerAttackStandby()
    {
        playerAnimator.SetInteger("state", (int)State.AttackStandby);
    }

    public void OnClickedPlayerCombo()
    {
        playerAnimator.SetInteger("state", (int)State.Combo);
    }

    public void OnClickedPlayerDrawBlade()
    {
        playerAnimator.SetInteger("state", (int)State.DrawBlade);
    }

    public void OnClickedPlayerPutBlade()
    {
        playerAnimator.SetInteger("state", (int)State.PutBlade);
    }

    public void OnClickedPlayerDamage()
    {
        playerAnimator.SetInteger("state", (int)State.Damage);
    }

    public void OnClickedPlayerSkill()
    {
        playerAnimator.SetInteger("state", (int)State.Skill);
    }

    public void OnClickedPlayerDead()
    {
        playerAnimator.SetInteger("state", (int)State.Dead);
    }
}