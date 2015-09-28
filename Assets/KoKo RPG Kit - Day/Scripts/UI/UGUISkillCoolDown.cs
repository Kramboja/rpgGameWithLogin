using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// UI Skill Cool Down Script.
/// </summary>
public class UGUISkillCoolDown : MonoBehaviour
{
    public float cooldownTime = 5f;                 // Skill Cooldown time.
    public Text cooldownText;                       // UGUI Text Component Reference. used to show cool down time.
    public Image image;                             // UGUI Image Component Reference. used to show cool down sprite animation.
    
    private PlayerFSMManager _playerFSM;            // PlayerFSMManager script Reference.
    private float _elapsedTime = 0;                 // used to calculate rest time for next skill.

    void Awake()
    {
        // Get PlayerFSMManager script reference to change player's fsm state to skill when user click skill button.
        _playerFSM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFSMManager>();
    }

    void Update()
    {
        // show cool down sprite animation and cool down text.
        if (image.fillAmount != 0f)
        {
            // cooldown sprite animation using fillAmount.
            image.fillAmount -= (1f / cooldownTime) * Time.deltaTime;
         
            // update cooldown text.
            _elapsedTime -= Time.deltaTime;
            cooldownText.text = ((int)_elapsedTime).ToString();

            // if next cool down time is less than 0.2 (zero) disable text component.
            if (_elapsedTime <= 1.2f)
            {
                cooldownText.text = "";
                cooldownText.gameObject.SetActive(false);
            }
        }
    }

    // Called when user click skill button.
    public void UseSkill()
    {
        if (CanUseSkill)
        {
            // set Image Component's fillAmount propety to 1, then Cool down animation will be played.
            image.fillAmount = 1f;

            // set _elasedTime var to cooldowntime to calculate rest time for next skill.
            _elapsedTime = cooldownTime + 1f;
            // update cool down text.
            cooldownText.text = _elapsedTime.ToString();
            // enable cooldownText UGUI Text to show remain time.
            cooldownText.gameObject.SetActive(true);

            // change player's fsm state to skill state.
            _playerFSM.UseSkill();
        }
    }

    // return boolean value if user is able to use skill.
    bool CanUseSkill
    {
        get { return image.fillAmount == 0f && _playerFSM.currentState != PlayerFSMManager.PlayerState.PlayerDeadState; }
    }
}