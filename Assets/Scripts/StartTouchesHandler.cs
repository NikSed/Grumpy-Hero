using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartTouchesHandler : MonoBehaviour
{
    [SerializeField] private MessagesController _messagesController;
    [SerializeField] private int _minTouchesForPlayButtonActivate = 15;
    [SerializeField] private float _loadingWaitingTime = 20f;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _setButton;
    [SerializeField] private Animator _mainMenuAnimator;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private RectTransform _playerTransform;

    private int _onHeroTouchesCount = 0;
    private int _totalTouchesCount = 0;

    private string _currentText = string.Empty;
    private bool _isLoading = false;

    public void OnHeroTouch()
    {
        if (_isLoading)
        {
            _isLoading = false;
            return;
        }

        _onHeroTouchesCount++;

        int randomValue = Random.Range(0, _messagesController.HeroTouchPhrasesCount);
        _currentText = _messagesController.HeroTouchPhrase(randomValue);

        BaseTouch();
    }

    public void OnPlayButtonTouch()
    {
        _messagesController.HideMessageBox();
        _mainMenuAnimator.SetTrigger("ShowSkins");
        _currentText = LocalizationManager.Instance.GetText("i_hope_no_problems_with_the_choice_of_skin");
        Invoke(nameof(BaseTouch), 0.5f);
    }

    public void OnExitButtonTouch()
    {
        int randomValue = Random.Range(0, _messagesController.ExitTouchPhrasesCount);
        _currentText = _messagesController.ExitTouchPhrase(randomValue);

        BaseTouch();
    }

    public void OnSetButtonTouch()
    {
        _messagesController.NotHideMessage = false;
        _messagesController.HideMessageBox();
        _mainMenuAnimator.SetTrigger("ShowLoading");
        _playerAnimator.SetBool("isRuning", true);
        _isLoading = true;
        StartCoroutine(Loading());
    }

    public void OnSkinTouch(bool himSelf = false)
    {
        if (himSelf)
        {
            _currentText = LocalizationManager.Instance.GetText("this_skin_is_beautiful");
            _setButton.interactable = true;
        }
        else
        {
            _setButton.interactable = false;
            int randomValue = Random.Range(0, _messagesController.SkinTouchPhrasesCount);
            _currentText = _messagesController.SkinTouchPhrase(randomValue);
        }

        BaseTouch();
    }

    public void OnGameTitleTouch()
    {
        _currentText = LocalizationManager.Instance.GetText("where_i_touch");

        BaseTouch();
    }

    private void BaseTouch()
    {
        _totalTouchesCount++;

        if (_totalTouchesCount == _minTouchesForPlayButtonActivate - 1)
        {
            _currentText = LocalizationManager.Instance.GetText("ok_here_your_button");
            _messagesController.ShowMessage(_currentText, true, true);
            Invoke(nameof(ShowPlayButton), 2f);
        }

        _messagesController.ShowMessage(_currentText);
    }

    private void ShowPlayButton()
    {
        _playButton.gameObject.SetActive(true);
    }

    private IEnumerator Loading()
    {
        while (_loadingWaitingTime > 0 && _isLoading)
        {
            _loadingWaitingTime -= Time.deltaTime;
            yield return null;
        }

        _playerAnimator.SetBool("isRuning", false);
        _currentText = LocalizationManager.Instance.GetText("actually_there_was_no_download");
        BaseTouch();
        StartCoroutine(MovePlayer());
    }

    private IEnumerator MovePlayer()
    {
        yield return new WaitForSeconds(3f);
        _playerAnimator.SetBool("isRuning", true);
        _currentText = LocalizationManager.Instance.GetText("just_dont_cry");
        _messagesController.ShowMessage(_currentText, true, true);
        _mainMenuAnimator.SetTrigger("MovePlayer");
    }
}
