using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UGUICamUIManager : MonoBehaviour
{
    public enum CamType
    {
        Player, Monster, NPC, Length
    }

    public Transform[] camPositions;
    public Canvas[] canvas;

    public Animator playerAnimator;
    public Animator[] monsterAnimators;
    public Animator[] npcAnimators;

    public Toggle[] toggles;
    public Button goToMainMenuButton;

    private Camera _mainCamera;

    void Awake()
    {
        _mainCamera = Camera.main;

        toggles[(int)CamType.Player].onValueChanged.AddListener(OnPlayerClicked);
        toggles[(int)CamType.Monster].onValueChanged.AddListener(OnMonsterClicked);
        toggles[(int)CamType.NPC].onValueChanged.AddListener(OnNPCClicked);

        goToMainMenuButton.onClick.AddListener(OnGoToMainMenuClicked);
    }

    void OnPlayerClicked(bool isSelected)
    {
        if (isSelected)
        {
            EnableCanvas(CamType.Player);

            foreach (Animator anim in monsterAnimators)
            {
                anim.SetInteger("state", 0);
            }

            foreach (Animator anim in npcAnimators)
            {
                anim.SetInteger("state", 0);
            }
        }
    }

    void OnMonsterClicked(bool isSelected)
    {
        if (isSelected)
        {
            EnableCanvas(CamType.Monster);

            playerAnimator.SetInteger("state", 0);
            foreach (Animator anim in npcAnimators)
            {
                anim.SetInteger("state", 0);
            }
        }
    }

    void OnNPCClicked(bool isSelected)
    {
        if (isSelected)
        {
            EnableCanvas(CamType.NPC);

            playerAnimator.SetInteger("state", 0);
            foreach (Animator anim in monsterAnimators)
            {
                anim.SetInteger("state", 0);
            }
        }
    }

    void EnableCanvas(CamType camType)
    {
        DisableAll();

        _mainCamera.transform.position = camPositions[(int)camType].position;
        canvas[(int)camType].gameObject.SetActive(true);
    }

    void DisableAll()
    {
        foreach (Canvas can in canvas)
        {
            can.gameObject.SetActive(false);
        }
    }

    void OnGoToMainMenuClicked()
    {
        DataManager.Instance.SceneLoad("1.Start_Scene");
    }
}