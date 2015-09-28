using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// UGUIEnemyHUD script.
/// This manages UGUIHUDText and slider bar to show enemy's health.
/// </summary>
public class UGUIEnemyHUD : MonoBehaviour
{
    public Transform hudRoot;
    public Transform enemyHud;
    public Image hpBar;

    // for hp bar tween animation.
    bool _isAnimPlaying = false;
    float _animTime = 0.5f;

    private UGUIHUDText _hudText;
    private UGUIFollowTarget _followTarget;

    // Initialization.
    void Awake()
    {
        hudRoot = GameObject.FindGameObjectWithTag("HUD Root").transform;

        Transform hud = (Transform)Instantiate(enemyHud);
        hud.SetParent(hudRoot);
        hud.localScale = Vector3.one;
        hud.localPosition = Vector3.zero;

        _hudText = hud.GetComponentInChildren<UGUIHUDText>();

        _followTarget = hud.GetComponent<UGUIFollowTarget>();
        _followTarget.target = transform;

        hpBar = _followTarget.hpBar;
    }

    void OnEnable()
    {
        hpBar.fillAmount = 1f;
    }

    // used for UGUIHUDText's value update.
    public void PrintDamage(string damageString)
    {
        _hudText.Add(damageString);
    }

    public void PrintDamage(string damageString, Color textColor, float duration)
    {
        _hudText.Add(damageString, textColor, duration);
    }

    // used for HUD HP Bar Tween animation functions.
    public void SetHPBar(float targetAmount)
    {
        if (!_isAnimPlaying)
            StartCoroutine("HPBarTweenAnim", targetAmount);
    }

    // EXPbar guage tween animation function.
    IEnumerator HPBarTweenAnim(float targetAmount)
    {
        _isAnimPlaying = true;
        float diff = hpBar.fillAmount - targetAmount;

        while (hpBar.fillAmount > targetAmount)
        {
            hpBar.fillAmount -= (1f / _animTime) * diff * Time.deltaTime;
            yield return null;
        }

        hpBar.fillAmount = targetAmount;
        _isAnimPlaying = false;
    }
}