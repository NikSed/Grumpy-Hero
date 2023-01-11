using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDialogue : MonoBehaviour
{
    public MessagesController MessagesController;
    private string _currentText;

    private void Start()
    {
        _currentText = LocalizationManager.Instance.GetText("standart_controlls");
        MessagesController.ShowMessage(_currentText);
    }
}
