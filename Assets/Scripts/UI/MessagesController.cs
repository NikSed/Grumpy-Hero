using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessagesController : MonoBehaviour
{
    [SerializeField] private Transform _messageBox;
    [SerializeField] private TextMeshProUGUI _messageTextUI;
    [SerializeField] private TextMeshPro _messageText;
    [SerializeField] private float _showingMessageTime = 3f;

    private List<string> _exitTouchPhrases;
    private List<string> _heroTouchPhrases;
    private List<string> _skinTouchPhrases;

    private bool _messageIsShowing = false;

    public string HeroTouchPhrase(int index) => _heroTouchPhrases[index];

    public int HeroTouchPhrasesCount => _heroTouchPhrases.Count;

    public string ExitTouchPhrase(int index) => _exitTouchPhrases[index];

    public int ExitTouchPhrasesCount => _exitTouchPhrases.Count;

    public string SkinTouchPhrase(int index) => _skinTouchPhrases[index];

    public int SkinTouchPhrasesCount => _skinTouchPhrases.Count;


    public bool NotHideMessage { get; set; } = false;

    public void ShowMessage(string text, bool withoutQueue = false, bool notHide = false)
    {
        if (NotHideMessage)
            return;

        if (withoutQueue)
        {
            HideMessageBox();
        }

        NotHideMessage = notHide;

        if (_messageIsShowing)
            return;

        if (_messageText != null)
            _messageText.text = text;
        else
            _messageTextUI.text = text;

        StartCoroutine(ShowMessageBox());
    }

    public void HideMessageBox()
    {
        NotHideMessage = false;
        StopCoroutine(ShowMessageBox());
        _messageIsShowing = false;
        _messageBox.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreatePhrases();
    }

    private void CreatePhrases()
    {
        _heroTouchPhrases = new List<string>()
        {
            LocalizationManager.Instance.GetText("you_stupid_or_something"),
            LocalizationManager.Instance.GetText("poke_yourself_in_one_place"),
            LocalizationManager.Instance.GetText("stop_it"),
            LocalizationManager.Instance.GetText("i_no_have_words"),
            LocalizationManager.Instance.GetText("you_are_pissing_me_off"),
            LocalizationManager.Instance.GetText("you_look_like_smart"),
            LocalizationManager.Instance.GetText("censorship"),
            LocalizationManager.Instance.GetText("ellipsis"),
            LocalizationManager.Instance.GetText("what_a_fool")
        };

        _exitTouchPhrases = new List<string>()
        {
            LocalizationManager.Instance.GetText("this_is_not_the_play_button"),
            LocalizationManager.Instance.GetText("press_the_play_button"),
            LocalizationManager.Instance.GetText("are_you_going_to_get_out"),
            LocalizationManager.Instance.GetText("good_riddance")
        };

        _skinTouchPhrases = new List<string>()
        {
            LocalizationManager.Instance.GetText("did_you_buy_this_skin_to_use"),
            LocalizationManager.Instance.GetText("take_what_they_give"),
            LocalizationManager.Instance.GetText("just_going_to_stare"),
            LocalizationManager.Instance.GetText("think_faster"),
            LocalizationManager.Instance.GetText("i_wll_never_wear_that")
        };
    }

    private IEnumerator ShowMessageBox()
    {
        _messageIsShowing = true;
        _messageBox.gameObject.SetActive(true);

        yield return new WaitForSeconds(_showingMessageTime);

        while (NotHideMessage)
            yield return null;

        _messageIsShowing = false;
        _messageBox.gameObject.SetActive(false);
    }
}
