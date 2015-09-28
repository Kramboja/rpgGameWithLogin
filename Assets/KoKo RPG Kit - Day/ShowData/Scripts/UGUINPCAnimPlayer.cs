using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UGUINPCAnimPlayer : MonoBehaviour
{
    public enum State
    {
        Idle, Talk, Walk, Length
    }

    public Button idleButton;
    public Button talkButton;

    public Animator[] npcAnimators;

    void Awake()
    {
        idleButton.onClick.AddListener(OnClickedMonsterIdle);
        talkButton.onClick.AddListener(OnClickedMonsterTalk);
    }

    public void OnClickedMonsterIdle()
    {
        SetAnimation(State.Idle);
    }

    public void OnClickedMonsterTalk()
    {
        SetAnimation(State.Talk);
    }

    void SetAnimation(State newState)
    {
        foreach (Animator anim in npcAnimators)
        {
            anim.SetInteger("state", (int)newState);
        }
    }
}