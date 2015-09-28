using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// UGUIHUDText made with Unity's new GUI system.
/// Plays alpha fade out and scale in-out animation.
/// </summary>
public class UGUIHUDText : MonoBehaviour
{
    // Manages text information to show.
    class TextEntry
    {
        public float time;
        public float stayTime;
        public string text;

        public TextEntry(float time, float stayTime, string text)
        {
            this.time = time;
            this.stayTime = stayTime;
            this.text = text;
        }
    }

    // Default Text Color.
    public Color defaultTextColor = Color.red;
    // Default alpha fade-out animation duration time.
    public float defaultAnimTime = 3f;
    // Default font.
    public Font defaultFont;
    // Default text font size.
    public int defaultFontSize = 30;
    // Text scale in-out animation's offset value.
    public float scaleOffset = 1.2f;
    // Text scale in-out animation's anim duration.
    public float scaleAnimTime = 1f;

    // used when reduce fontsize.
    private float _scaleReduceInterval = 0f;
    // used to check elapsedtime.
    private float _scaleElapsedTime = 0f;

    // Object pool's active transform.
    public Transform activeTransform;
    // Object pool's inactive transform.
    public Transform inactiveTransform;

    // TextEntry list.
    private List<TextEntry> _entryList = new List<TextEntry>();
    // Object pool's active list.
    private List<Text> _activeList = new List<Text>();
    // Object pool's inactive list.
    private List<Text> _inactiveList = new List<Text>();

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying) return;
#endif
        // If HUDText is enabled, play hudtext animation.
        if (_entryList.Count > 0)
        {
            for (int ix = 0; ix < _entryList.Count; ++ix)
            {
                HUDTextAnimation(ix);
            }
        }
    }

    // Text list Initialization function.
    void Initialize()
    {
        inactiveTransform.gameObject.SetActive(true);

        Text[] texts = GetComponentsInChildren<Text>();
        for (int ix = 0; ix < texts.Length; ++ix)
        {
            _inactiveList.Add(texts[ix]);
            SetTextInfo(texts[ix]);
        }

        inactiveTransform.gameObject.SetActive(false);

        float diff = (defaultFontSize + defaultFontSize * scaleOffset) - defaultFontSize;
        _scaleReduceInterval = scaleAnimTime / diff;
    }

    // Text Component initialization.
    void SetTextInfo(Text text)
    {
        text.font = defaultFont;
        text.fontStyle = FontStyle.Bold;
        text.fontSize = defaultFontSize;
        text.color = defaultTextColor;
        text.alignment = TextAnchor.LowerCenter;
        text.supportRichText = true;
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.verticalOverflow = VerticalWrapMode.Overflow;

        text.rectTransform.sizeDelta = new Vector2(100f, defaultFontSize + 5f);
    }

    // HUDText Alpha animation and turn off the text when the condition is matched.
    void HUDTextAnimation(int entryIndex)
    {
        // Text Scale Animation.
        // Play Text Scale in-out animation.
        if (_activeList[entryIndex].fontSize != defaultFontSize)
        {
            // check elapsed time.
            _scaleElapsedTime += Time.deltaTime;

            // if elapsed time is past reduce interval, minus 1 font size.
            if (_scaleElapsedTime > _scaleReduceInterval)
            {
                --_activeList[entryIndex].fontSize;
                _scaleElapsedTime = 0f;
            }
        }

        // Text Alpha Animation.
        // Add Time -> Timer.
        _entryList[entryIndex].time += Time.deltaTime;

        // Play Alpha animation if elapsed time is past half of stay duration time.
        if (_entryList[entryIndex].time > _entryList[entryIndex].stayTime * 0.5f)
        {
            Color color = _activeList[entryIndex].color;
            color.a -= (2f / _entryList[entryIndex].stayTime) * Time.deltaTime;
            _activeList[entryIndex].color = color;
        }

        // Turn off HUDText from the screen if the time is past stay duration time.
        if (_entryList[entryIndex].time > _entryList[entryIndex].stayTime)
        {
            ReturnTextToList(_activeList[entryIndex]);
            _entryList.RemoveAt(entryIndex);
        }
    }

    // Get Text object from object pool.
    Text GetTextFromList()
    {
        // If text object can be return from the list, just return text object.
        if (_inactiveList.Count > 0)
        {
            _activeList.Add(_inactiveList[0]);
            _inactiveList.RemoveAt(0);
            _activeList[_activeList.Count - 1].transform.SetParent(activeTransform);
            _activeList[_activeList.Count - 1].rectTransform.localPosition = Vector3.zero;

            return _activeList[_activeList.Count - 1];
        }

        // If there's no enouth text object in the object pool, create new text object and return created one.
        else
        {
            GameObject newObj = new GameObject("Text");

            Text newText = newObj.AddComponent<Text>();
            SetTextInfo(newText);

            _activeList.Add(newText);
            _activeList[_activeList.Count - 1].transform.SetParent(activeTransform);
            _activeList[_activeList.Count - 1].rectTransform.localPosition = Vector3.zero;
            _activeList[_activeList.Count - 1].transform.localRotation = Quaternion.identity;
            _activeList[_activeList.Count - 1].transform.localScale = Vector3.one;

            return _activeList[_activeList.Count - 1];
        }
    }

    // Return Text object to object pool.
    void ReturnTextToList(Text text)
    {
        _inactiveList.Add(text);
        _activeList.Remove(text);
        _inactiveList[_inactiveList.Count - 1].transform.SetParent(inactiveTransform);
    }

    // Set Text information to show and Add this info to _entryList.
    void AddEntry(string text, Color color, float stayDuration)
    {
        if (_entryList.Count > 0)
        {
            for (int ix = 0; ix < _entryList.Count; ++ix)
            {
                float yPos = defaultFontSize + 5f;
                _activeList[ix].rectTransform.localPosition += new Vector3(0f, yPos, 0f);
            }
        }

        _entryList.Add(new TextEntry(0f, stayDuration, text));
    }

    // HUDText Add Function.
    public void Add(string text)
    {
        Text hudText = GetTextFromList();
        hudText.color = defaultTextColor;
        hudText.fontSize = (int)((float)defaultFontSize * scaleOffset);
        hudText.text = text;

        AddEntry(text, defaultTextColor, defaultAnimTime);
    }

    public void Add(string text, Color color, float stayDuration)
    {
        Text hudText = GetTextFromList();
        hudText.color = color;
        hudText.fontSize = (int)((float)defaultFontSize * scaleOffset);
        hudText.text = text;

        stayDuration = stayDuration == 0f ? defaultAnimTime : stayDuration;
        AddEntry(text, color, stayDuration);
    }
}