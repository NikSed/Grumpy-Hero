using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FastLocalizationUI : MonoBehaviour
{
    [SerializeField] private string _key;

    private TextMeshProUGUI _textMeshProUGUI;

    private void Awake()
    {
        LocalizationManager.OnLanguageChange.AddListener(OnLanguageChangeHandler);
    }

    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _textMeshProUGUI.text = LocalizationManager.Instance.GetText(_key);
    }

    private void OnLanguageChangeHandler()
    {
        _textMeshProUGUI.text = LocalizationManager.Instance.GetText(_key);
    }
}