using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DifficultyButton : Selectable, IPointerClickHandler
{
    public DifficultySO difficultySettings;

    public TextMeshProUGUI buttonText;

    public UnityEvent OnClick;

    protected override void Start()
    {
        base.Start();

        ConfigureButton();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (!IsActive() || !IsInteractable() || difficultySettings == null)
        {
            return;
        }
        DifficultyMemory.Instance.SetDifficultySettings(difficultySettings);
        OnClick?.Invoke();
    }

    [ContextMenu("Configure Button")]
    void ConfigureButton()
    {
        if (difficultySettings == null) return;

        buttonText.text = difficultySettings.difficultyName;

        ColorBlock colorsBlock = colors;

        colorsBlock.normalColor = difficultySettings.buttonColor;

        colors = colorsBlock;
    }
}
