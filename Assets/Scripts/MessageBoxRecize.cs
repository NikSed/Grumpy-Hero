using TMPro;
using UnityEngine;

public class MessageBoxRecize : MonoBehaviour
{
    public SpriteRenderer MessageBox;
    public TextMeshPro Text;

    private void OnEnable()
    {
        Vector2 newSize = new Vector2(Text.text.Length / 10f + 0.5f, MessageBox.GetComponent<SpriteRenderer>().size.y);
        MessageBox.GetComponent<SpriteRenderer>().size = newSize;
    }
}
