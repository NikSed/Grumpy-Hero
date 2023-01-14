using UnityEngine;

public class GameDialogue : MonoBehaviour
{
    public MessagesController MessagesController;
    private string _currentText;

    private void Start()
    {
        Invoke(nameof(ShowStartMessage), 0.6f);
    }

    private void ShowStartMessage()
    {
        _currentText = LocalizationManager.Instance.GetText("standart_controlls");
        MessagesController.ShowMessage(_currentText);
    }
}
