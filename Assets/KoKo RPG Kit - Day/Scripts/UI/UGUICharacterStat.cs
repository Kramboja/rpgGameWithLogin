using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// UGUICharacterStat script.
/// Character Stat UI is shown in the sceen's upperleft side.
/// </summary>
public class UGUICharacterStat : MonoBehaviour
{
    public Image hpBar;
    public Text hpText;
    public Text levelText;
    public Text goldText;

    public UGUIOptionPopup optionPopup;

    private PlayerData _playerData;

    void Start()
    {
        // Initialization.
        _playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
        hpText.text = ((int)_playerData.currentHP).ToString() + " / " + ((int)_playerData.levelData.maxHP).ToString();
    }

    void Update()
    {
        // Update UI with it's data.
        hpBar.fillAmount = (float)_playerData.currentHP / (float)_playerData.levelData.maxHP;
        hpText.text = ((int)_playerData.currentHP).ToString() + " / " + ((int)_playerData.levelData.maxHP).ToString();
        levelText.text = _playerData.level.ToString();
        goldText.text = _playerData.gold.ToString();

        // android back button event handler.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // show pause popup.
            optionPopup.OnOptionButtonClicked();
        }
    }
}