using UnityEngine;
using UnityEngine.UI;

public class StartTouchesHandler : MonoBehaviour
{

    [SerializeField] private MessagesController _messagesController;
    [SerializeField] private int _minTouchesForPlayButtonActivate = 15;
    [SerializeField] private Button _playButton;

    private int _onHeroTouchesCount = 0;
    private int _totalTouchesCount = 0;

    private string _currentText = string.Empty;

    public void OnHeroTouch()
    {
        int randomValue = Random.Range(0, 3);

        _onHeroTouchesCount++;

        if (randomValue == 1)
        {
            _currentText = LocalizationManager.Instance.GetText("press_the_play_button");
            _messagesController.ShowMessage(_currentText);
        }
        else
        {
            randomValue = Random.Range(0, _messagesController.HeroTouchPhrasesCount);
            _currentText = _messagesController.HeroTouchPhrase(randomValue);
        }

        BaseTouch();
    }

    public void OnPlayButtonTouch()
    {
        _messagesController.NotHideMessage = false;
    }

    public void OnExitButtonTouch()
    {
        int randomValue = Random.Range(0, _messagesController.ExitTouchPhrasesCount);
        _currentText = _messagesController.ExitTouchPhrase(randomValue);

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

        if (_totalTouchesCount > _minTouchesForPlayButtonActivate - 1)
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
}
