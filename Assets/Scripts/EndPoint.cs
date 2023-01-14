using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private MessagesController _messagesController;
    [SerializeField] private GameObject _confetti;
    private bool _isEnd = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (_isEnd)
                return;

            _confetti.SetActive(true);
            _isEnd = true;
            SoundManager.Instance.StopAllMusics();
            SoundManager.Instance.PlaySound("Win1");
            SoundManager.Instance.PlaySound("Win2");
            _messagesController.HideMessageBox();
            _messagesController.ShowMessage(LocalizationManager.Instance.GetText("game_end"), true, true);
            Invoke(nameof(PlayMainMusic), 3f);
        }
    }

    private void PlayMainMusic()
    {
        SoundManager.Instance.PlayMusic("Main");
    }
}
