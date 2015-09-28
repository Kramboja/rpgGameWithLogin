using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UGUICharacterStatPopup script.
/// Character Stat Popup will be shown when user click Character stat button placed in screen's upper right side.
/// </summary>
public class UGUICharacterStatPopup : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Text hpText;
    public Text attackText;
    public Text skillAttackText;
    public Text expText;
    public Text goldText;

    private PlayerData _playerData;

    void Awake()
    {
        // Get PlayerData component's reference to show player data.
        _playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
        
        // there's no player name data so, set "KoKo" as player name.
        nameText.text = "KoKo";
    }

	void Update()
	{
        // Update UI with player data.
        levelText.text = _playerData.level.ToString();
        hpText.text = _playerData.currentHP.ToString() + "/" + _playerData.levelData.maxHP.ToString();
        attackText.text = _playerData.levelData.baseAttack.ToString();
        skillAttackText.text = _playerData.levelData.skillAttack.ToString();
        expText.text = _playerData.exp.ToString();
        goldText.text = _playerData.gold.ToString();
	}

    // Called when user clicked character stat button placed in upper right side of screen.
    public void OnStatButtonClicked()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}