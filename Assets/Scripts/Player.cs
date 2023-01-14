using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] MessagesController _messagesController;
    [SerializeField] CharacterController2D _controller2D;
    [SerializeField] TileMapMovier _tileMapMovier;
    [SerializeField] private SpriteRenderer _image;

    private string _currentText;
    private Rigidbody2D _rigidbody2D;
    private int _deathCount = 0;
    private int _onTileJumsCount;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        SoundManager.Instance.PlayMusic("Main");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            transform.position = _spawnPoint.position;
            _rigidbody2D.velocity = Vector2.zero;

            _deathCount++;

            _controller2D.IsControllsInverse = !_controller2D.IsControllsInverse;

            if (_deathCount == 5)
            {
                _messagesController.ShowMessage(LocalizationManager.Instance.GetText("ok_i_help_you"), true);
                _image.color= Color.red;
                Invoke(nameof(TileMove), 1f);
                return;
            }

            if (_deathCount == 1)
            {
                _messagesController.ShowMessage(LocalizationManager.Instance.GetText("dont_ask_why_not_animations"), true);
                return;
            }

            if (_deathCount == 2)
            {
                _messagesController.ShowMessage(LocalizationManager.Instance.GetText("write_with_mistake"), true);
                return;
            }

            int randomValue = Random.Range(0, _messagesController.OffensivePhrasesCount);
            _currentText = _messagesController.OffensivePhrases(randomValue);

            Invoke(nameof(SayPhrase), 0.2f);
        }
        else if (collision.TryGetComponent(out TileMapMovier tileMapMovier))
        {
            if (_onTileJumsCount > 0 && _onTileJumsCount < 2)
                _controller2D.MoveSpeed /= 100f;

            if (_onTileJumsCount == 0)
            {
                _image.color = Color.white;
                _messagesController.HideMessageBox();
                _messagesController.ShowMessage(LocalizationManager.Instance.GetText("dont_jump_here"), true, true);
            }

            if (_onTileJumsCount == 2)
            {
                _image.color = Color.red;
                _messagesController.HideMessageBox();
                _messagesController.ShowMessage(LocalizationManager.Instance.GetText("listen_my_advice"), true, true);
            }

            if (_onTileJumsCount == 5)
            {
                _image.color = Color.white;
                _messagesController.HideMessageBox();
                _messagesController.ShowMessage(LocalizationManager.Instance.GetText("help_you_last"), true, true);
                _controller2D.MoveSpeed = 2f;
            }

            if (_onTileJumsCount > 10)
            {
                _controller2D.MoveSpeed = 2f;
            }

            _onTileJumsCount++;
        }
    }

    private void SayPhrase()
    {
        _messagesController.ShowMessage(_currentText, true);
    }

    private void TileMove()
    {
        _tileMapMovier.Move();
    }
}
