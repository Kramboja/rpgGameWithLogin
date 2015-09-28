using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// UGUIExpBar script.
/// Player's EXP Bar UI is placed in lower-center side of screen.
/// </summary>
public class UGUIExpBar : MonoBehaviour
{
    public Image expBar;
    public float animTime = 1f;

    private PlayerLevelData.Attribute[] _levelData;
    private PlayerData _playerData;
    private PlayerLevelData.Attribute _maxlevelData;

    private bool _isAnimPlaying = false;
    private int _currentLevel;

    // Initialization.
    void Start()
    {
        _levelData = DataManager.Instance.playerLevelData;
        _playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
        _maxlevelData = _levelData[_levelData.Length - 1];
        _currentLevel = _playerData.level;
    }

    // UI Update.
    void Update()
    {
        // if player's current level is max level, fill the exp bar full and does nothing.
        if (_playerData.level == _maxlevelData.level)
        {
            expBar.fillAmount = 1f;
            return;
        }

        // calculate the value to be set to hpbar.fillamount.
        float numerator = _playerData.exp - _levelData[_playerData.level - 1].reqEXP;
        float denominator = _levelData[_playerData.level].reqEXP - _levelData[_playerData.level - 1].reqEXP;
        float fillAmount = numerator / denominator;

        // when player levelup, play gauge tween animation.
        if (IsLevelUp && !_isAnimPlaying)
        {
            _currentLevel = _playerData.level;
            StartCoroutine("LevelupFill", fillAmount);
        }

        // when player gets exp, play guage tween animation.
        else if (expBar.fillAmount != fillAmount && !_isAnimPlaying)
        {
            StartCoroutine("GaugeFillAnim", fillAmount);
        }
    }

    // EXPbar guage tween animation function.
    IEnumerator GaugeFillAnim(float targetAmount)
    {
        _isAnimPlaying = true;
        float diff = targetAmount - expBar.fillAmount;

        while (expBar.fillAmount < targetAmount)
        {
            expBar.fillAmount += (1f / animTime) * diff * Time.deltaTime;
            yield return null;
        }

        expBar.fillAmount = targetAmount;
        _isAnimPlaying = false;
    }

    // When player level up, fill hpbar.fillamount to 1f (full) and make fillamount to zero then fill again.
    IEnumerator LevelupFill(float targetAmount)
    {
        _isAnimPlaying = true;

        // fill hpbar.fillamount to full.
        yield return StartCoroutine("GaugeFillAnim", 1f);

        // then make fill hpbar.fillamount to zero.
        expBar.fillAmount = 0f;

        // and then fill hpbar.fillamount to targetAmount.
        yield return StartCoroutine("GaugeFillAnim", targetAmount);

        _isAnimPlaying = false;
    }

    // check if player level up.
    bool IsLevelUp
    {
        get { return _currentLevel != _playerData.level; }
    }
}