using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DifficultyButton : Selectable, IPointerClickHandler
{
    public DifficultySO difficultySettings;

    public TextMeshProUGUI buttonText;

    protected override void Start()
    {
        base.Start();

        buttonText.text = difficultySettings.difficultyName;

        ColorBlock colorsBlock= colors;

        colorsBlock.normalColor = difficultySettings.buttonColor;

        colors = colorsBlock;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (!IsActive() || !IsInteractable())
        {
            return;
        }

        DifficultyMemory.Instance.SetDifficultySettings(difficultySettings);

        SceneManager.LoadScene("Game");
    }
}
