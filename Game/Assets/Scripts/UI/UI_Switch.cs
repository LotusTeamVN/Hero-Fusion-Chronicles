using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Switch : MonoBehaviour
{
    [Header("Object Reference")]
    public Image background = null;
    public Image handle = null;
    public TMP_Text switchTxt = null;

    [Header("Configuration")]
    public Ease moveType = Ease.OutBack;
    public float timeHandle = 0.2f;
    public Sprite[] backgroundSprites = null;
    public Sprite[] handleSprites = null;
    public string[] switchContent = null;
    public Color[] switchTextColor = null;
    public RectTransform[] handlePos = null;
    public RectTransform[] switchTxtPos = null;

    private Toggle toggle = null;

    public Action<bool> OnValueChanged = null;
    public bool IsOn { get; private set; }

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnChanged);
    }

    private void OnChanged(bool value)
    {
        IsOn = value;

        RectTransform targetPos = value ? handlePos[1] : handlePos[0];
        handle.rectTransform.DOAnchorPos(targetPos.anchoredPosition, timeHandle).SetEase(moveType);
        handle.sprite = value ? handleSprites[1] : handleSprites[0];
        background.sprite = value ? backgroundSprites[1] : backgroundSprites[0];
        switchTxt.rectTransform.anchoredPosition = value ? switchTxtPos[1].anchoredPosition : switchTxtPos[0].anchoredPosition;
        switchTxt.text = value ? switchContent[1] : switchContent[0];
        switchTxt.color = value ? switchTextColor[1] : switchTextColor[0];

        OnValueChanged?.Invoke(value);
    }
}
